using Azure.AI.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleRAG.Models
{
    public class UniversalLLMFunctionCaller
    {
        private Kernel _kernel;
        IChatCompletionService _chatCompletion;
        public UniversalLLMFunctionCaller(Kernel kernel)
        {
            _kernel = kernel;
            _chatCompletion = _kernel.GetRequiredService<IChatCompletionService>();
        }

        public async Task<string> RunAsync(ChatHistory askHistory)
        {
            string ask = await GetAskFromHistory(askHistory);
            return await RunAsync(ask);
        }

        private async Task<string> GetAskFromHistory(ChatHistory askHistory)
        {
            StringBuilder sb = new StringBuilder();
            var userAndAssistantMessages = askHistory.Where(h => h.Role == AuthorRole.Assistant || h.Role == AuthorRole.User);
            foreach (var message in userAndAssistantMessages)
            {
                sb.AppendLine($"{message.Role.ToString()}: {message.Content}");
            }

            // string extractAskFromHistoryPrompt = $@"Look at this dialog between a user and an assistant. 
            // Summarize what the user wants the assistant to do with their last message
            // ##Start of Conversation##
            // {sb.ToString()}
            // ##End of Conversation##";

            string extractAskFromHistoryPrompt = $@"阅读这段用户与助手之间的对话。 
            总结用户在最后一句话中希望助手做什么
            ##对话开始##
            {sb.ToString()}
            ##对话结束##";


            var extractAskResult = await _chatCompletion.GetChatMessageContentAsync(extractAskFromHistoryPrompt);
            string ask = extractAskResult.Content;
            return ask;
        }

        public async Task<string> RunAsync(string task)
        {
            // Initialize plugins
            var plugins = _kernel.Plugins;
            var internalPlugin = _kernel.Plugins.AddFromType<UniversalLLMFunctionCallerInternalFunctions>();

            // Convert plugins to text
            string pluginsAsText = GetTemplatesAsTextPrompt3000(plugins);

            // Initialize function call and chat history
            FunctionCall nextFunctionCall = new FunctionCall { Name = "Start" };
            ChatHistory chatHistory = InitializeChatHistory(task);

            // Add new task to chat history
            //chatHistory.Add(new ChatMessageContent(AuthorRole.User, $"New task: {task}"));

            // Process function calls
            for (int iteration = 0; iteration < 10 && nextFunctionCall.Name != "Finished"; iteration++)
            {
                nextFunctionCall = await GetNextFunctionCallAsync(chatHistory, pluginsAsText);
                if (nextFunctionCall == null) throw new Exception("The LLM is not compatible with this approach.");

                // Add function call to chat history
                string nextFunctionCallText = GetCallAsTextPrompt3000(nextFunctionCall);
                chatHistory.AddAssistantMessage(nextFunctionCallText);

                // Invoke plugin and add response to chat history
                string pluginResponse = await InvokePluginAsync(nextFunctionCall);
                chatHistory.AddUserMessage(pluginResponse);
            }

            // Remove internal plugin
            _kernel.Plugins.Remove(internalPlugin);

            // Check if task was completed successfully
            if (nextFunctionCall.Name == "Finished")
            {
                string finalMessage = nextFunctionCall.Parameters[0].Value.ToString();
                return finalMessage;
            }

            throw new Exception("LLM could not finish workflow within 10 steps. Consider increasing the number of steps.");
        }

        private ChatHistory InitializeChatHistory(string ask)
        {
            ChatHistory history = new ChatHistory();

            history.Add(new ChatMessageContent(AuthorRole.User, "New task: 启动飞船"));
            history.Add(new ChatMessageContent(AuthorRole.Assistant, "GetMySpaceshipName()"));
            history.Add(new ChatMessageContent(AuthorRole.User, "嫦娥七号"));
            history.Add(new ChatMessageContent(AuthorRole.Assistant, "StartSpaceship(ship_name: \"嫦娥七号\")"));
            history.Add(new ChatMessageContent(AuthorRole.User, "飞船启动"));
            history.Add(new ChatMessageContent(AuthorRole.Assistant, "Finished(finalmessage: \"'嫦娥七号'飞船启动 \")"));
            history.Add(new ChatMessageContent(AuthorRole.User, $"New task: {ask}"));

            return history;
        }

        private async Task<FunctionCall?> GetNextFunctionCallAsync(ChatHistory history, string pluginsAsText)
        {
            // Create a copy of the chat history
            var copiedHistory = new ChatHistory(history.ToArray().ToList());

            // Add system message to history if not present
            if (copiedHistory[0].Role != AuthorRole.System)
            {
                string systemMessage = GetLoopSystemMessage(pluginsAsText);
                copiedHistory.Insert(0, new ChatMessageContent(AuthorRole.System, systemMessage));
            }

            // Try to get the next function call
            for (int retryCount = 0; retryCount < 5; retryCount++)
            {
                // Get LLM reply and add it to the history
                var llmReply = await _chatCompletion.GetChatMessageContentAsync(copiedHistory);
                copiedHistory.AddAssistantMessage(llmReply.Content);

                try
                {
                    // Parse and validate the function call
                    FunctionCall functionCall = ParseTextPrompt3000Call(llmReply);
                    ValidateFunctionWithKernel(functionCall);

                    return functionCall;
                }
                catch (Exception ex)
                {
                    // Add error message to history and continue
                    string errorMessage = $"Error: '{ex.Message}'. Please try again without apologizing or explaining. Just follow the previous rules.";
                    copiedHistory.AddUserMessage(errorMessage);
                }
            }

            return null;
        }


        private string GetTap1SystemMessage(string pluginsAsTextPrompt3000)
        {
            string systemPrompt = $@"You are an advisor, that is tasked with finding the next step to fullfil a goal.
Below, you are provided a goal, that needs to be reached, and a chat between a user and a computer, as well as a list of functions that the user could use.
You need to find out what the next step for the user is to reach the goal. You are also provided a list of functions that are in TextPrompt3000 Schema Format.
The TextPrompt3000 Format is defined like this:
{GetTextPrompt300Explanation()}
##available functions##
{pluginsAsTextPrompt3000}
##end functions##

The following rules are very important:
1) you can only recommend one function and the parameters, not multiple functions
2) You can only recommend a function that is in the list of available functions
3) You need to give all parameters for the function
4) Given the history, the function you recommend needs to be important to get closer towards the goal
5) Do not wrap functions into each other. Stick to the list of functions, this is not a math problem. Do not use placeholders.
We only need one function, the next one needed. For example, if function A() needs to be used as parameter in function B(), do NOT do B(A()). Instead,
if A wasnt called allready, call A() first. The result will be used in B in a later iteration.
6) Do not recommend a function that was recently called. Use the output instead. Do not use Placeholders or Functions as parameters for other functions
7) When all  necessary functions are called and the result was presented by the computer system, call the Finished function and present the result


If you break any of those rules, a kitten dies. 
What function should the user execute next on the computer? Explain your reasoning step by step.
";
            return systemPrompt;
        }
        //private string GetLoopSystemMessage(string pluginsAsTextPrompt3000)
        //{
        //    string systemPrompt = $@"You are a computer system. You can only speak TextPrompt3000 to make the user call functions, and the user will behave
        //as a different computer system that answers those functions.
        //Below, you are provided a goal that needs to be reached, as well as a list of functions that the user could use.
        //You need to find out what the next step for the user is to reach the goal and recommend a TextPrompt3000 function call. 
        //You are also provided a list of functions that are in TextPrompt3000 Schema Format.
        //The TextPrompt3000 Format is defined like this:
        //{GetTextPrompt300Explanation()}
        //##available functions##
        //{pluginsAsTextPrompt3000}
        //##end functions##

        //The following rules are very important:
        //1) you can only recommend one function and the parameters, not multiple functions
        //2) You can only recommend a function that is in the list of available functions
        //3) You need to give all parameters for the function. Do NOT escape special characters in the name of functions or the names of parameters (dont do aaa\_bbb, just stick to aaa_bbb)!
        //4) Given the history, the function you recommend needs to be important to get closer towards the goal
        //5) Do not wrap functions into each other. Stick to the list of functions, this is not a math problem. Do not use placeholders.
        //We only need one function, the next one needed. For example, if function A() needs to be used as parameter in function B(), do NOT do B(A()). Instead,
        //if A wasnt called allready, call A() first. The result will be used in B in a later iteration.
        //6) Do not recommend a function that was recently called. Use the output instead. Do not use Placeholders or Functions as parameters for other functions
        //7) Only write a Function Call, do not explain why, do not provide a reasoning. You are limited to writing a function call only!
        //8) When all  necessary functions are called and the result was presented by the computer system, call the Finished function and present the result

        //If you break any of those rules, a kitten dies. 
        //";
        //    return systemPrompt;
        //}

        private string GetLoopSystemMessage(string pluginsAsTextPrompt3000)
        {
            string systemPrompt = $@"你是一个计算机系统。
你只能使用TextPrompt3000指令，让用户调用对应的函数，而用户将作为另一个回答这些函数的计算机系统。
以下是您所需实现的目标，以及用户可以使用的函数列表。
您需要找出用户到达目标的下一步，并推荐一个TextPrompt3000函数调用。 
您还会得到一个TextPrompt3000 Schema格式的函数列表。
TextPrompt3000格式的定义如下所示:
{GetTextPrompt300Explanation()}
##可用函数列表开始##
{pluginsAsTextPrompt3000}
##可用函数列表结束##

以下规则非常重要：
1) 你只能推荐一个函数及其参数，而不是多个函数
2) 你可以推荐的函数只存在于可用函数列表中
3) 你需要为该函数提供所有参数。不要在函数名或参数名中转义特殊字符，直接使用（如只写aaa_bbb，不要写成aaa\_bbb）
4) 你推荐的历史记录与函数需要对更接近目标有重要作用
5) 不要将函数相互嵌套。 遵循列表中的函数，这不是一个数学问题。 不要使用占位符。
我们只需要一个函数，下一个所需的函数。举个例子， 如果 function A() 需要在 function B()中当参数使用, 不要使用 B(A())。 而是,
如果A还没有被调用, 先调用 A()。返回的结果将在下一次迭代中在B中使用。
6) 不要推荐一个最近已经调用过的函数。 使用输出代替。 不要将占位符或函数作为其他函数的参数使用。
7) 只写出一个函数调用，不解释原因，不提供理由。您只能写出一个函数调用！
8) 当所有必需的函数都被调用，且计算机系统呈现了结果，调用Finished函数并展示结果。
9) 请使用中文回答。

如果你违反了任何这些规定，那么会有一只小猫死去。
";
            return systemPrompt;
        }

        private async Task<FunctionCall> GetNextFunctionToCallDoubleTappedAsync(ChatHistory tap1history, string pluginsAsTextPrompt3000)
        {
            if (tap1history[0].Role != AuthorRole.System)
            {
                tap1history.Insert(0, new ChatMessageContent(AuthorRole.System, GetTap1SystemMessage(pluginsAsTextPrompt3000)));
            }

            //we are doing multi-tapping here. First get an elaborate answer, and then press that answer into a usable format
            var tap1Answer = await _chatCompletion.GetChatMessageContentAsync(tap1history);
            string tap2Prompt = @$"You are a computer system. You can only answer with a TextPrompt3000 function, nothing else. 
A user is giving you a text. You need to extract a TextPrompt3000 function call from it.
{GetTextPrompt300Explanation()}
You can not say anything else but a function call. Do not explain anything. Do not answer as a schema, including '--' in the end. 
Answer as actual function call. The result could look like this 'FunctionA(paramA: content)' or 'FunctionB()'
##Text from user##
{tap1Answer.Content}
##
##Available functions#
{pluginsAsTextPrompt3000}
##
It is very important that you only return a valid function that you extracted from the users text, no other text. 
Do not supply multiple functions, only one, the next necessary one.
It needs to include all necessary parameters. 
If you do not do this, a very cute kitten dies.";

            ChatHistory tap2History = new ChatHistory();
            tap2History.AddUserMessage(tap2Prompt);


            for (int iRetry = 0; iRetry < 5; iRetry++)
            {
                var tap2Answer = await _chatCompletion.GetChatMessageContentsAsync(tap2History);
                Console.WriteLine(tap2Answer[0].Content);
                tap2History.AddAssistantMessage(tap2Answer[0].Content);
                try
                {
                    FunctionCall functionCall = ParseTextPrompt3000Call(tap2Answer[0]);
                    ValidateFunctionWithKernel(functionCall);

                    return functionCall;
                }
                catch (Exception ex)
                {
                    tap2History.AddUserMessage($"Error: '{ex.Message}'. Try again. Do not apologise. Do not explain anything. just follow the rules from earlier");
                }
            }

            return null;
        }

        private void ValidateFunctionWithKernel(FunctionCall functionCall)
        {
            // Iterate over each plugin in the kernel
            foreach (var plugin in _kernel.Plugins)
            {
                // Check if the plugin has a function with the same name as the function call
                var function = plugin.FirstOrDefault(f => f.Name == functionCall.Name);
                if (function != null)
                {
                    // Check if the function has the same parameters as the function call
                    if (function.Metadata.Parameters.Count == functionCall.Parameters.Count)
                    {
                        for (int i = 0; i < function.Metadata.Parameters.Count; i++)
                        {
                            if (function.Metadata.Parameters[i].Name != functionCall.Parameters[i].Name)
                            {
                                throw new Exception("Parameter " + functionCall.Parameters[i].Name + " does not exist in the function.");
                            }
                        }
                        return; // Exit the function if both function name and parameters match
                    }
                    else
                    {
                        throw new Exception($"Parameter count does not match for the function.");
                    }
                }
            }

            throw new Exception($"Function '{functionCall.Name}' does not exist in the kernel.");
        }



        private FunctionCall ParseTextPrompt3000Call(ChatMessageContent possibleFunctionCallResult)
        {
            // Get the content of the first ChatMessageContent
            string content = possibleFunctionCallResult.Content;

            // Split the content into function name and parameters
            int openParenIndex = content.IndexOf('(');
            int closeParenIndex = content.IndexOf(')');
            string functionName = content.Substring(0, openParenIndex);
            string parametersContent = content.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);

            parametersContent = RemoveBackslashesOutsideQuotes(parametersContent);

            // Create a new FunctionCall
            FunctionCall functionCall = new FunctionCall();
            functionCall.Name = functionName;
            functionCall.Parameters = new List<FunctionCallParameter>();

            // Check if there are any parameters
            if (!string.IsNullOrWhiteSpace(parametersContent))
            {
                // Use a regular expression to match the parameters
                var matches = Regex.Matches(parametersContent, @"(\w+)\s*:\s*(""([^""]*)""|(\d+(\.\d+)?))");

                // Parse each parameter
                foreach (Match match in matches)
                {
                    // Get the parameter name and value
                    string parameterName = match.Groups[1].Value;
                    object parameterValue = match.Groups[3].Success ? match.Groups[3].Value : double.Parse(match.Groups[4].Value); // You might want to convert this to the appropriate type


                    // Create a new FunctionCallParameter and add it to the FunctionCall
                    FunctionCallParameter functionCallParameter = new FunctionCallParameter();
                    functionCallParameter.Name = parameterName;
                    functionCallParameter.Value = parameterValue;
                    functionCall.Parameters.Add(functionCallParameter);
                }
            }

            return functionCall;
        }


        private string RemoveBackslashesOutsideQuotes(string input)
        {
            StringBuilder result = new StringBuilder();
            bool insideQuotes = false;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }

                if (c != '\\' || insideQuotes)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }




        private string GetTextPrompt300Explanation()
        {
            return $@"##TextPrompt3000的例子##
FunctionName(parameter: value)
##
在TextPrompt3000中，也有一些结构。 
##TextPrompt3000结构的例子##
FunctionName(datatype1 parametername1:""parameter1 description"",datatype2 parametername2:""parameter2 description"")--函数的描述
##
可以没有参数，一个参数，或者多个参数。
举个例子，一个结构可能是这样的：
StartSpaceship(string shipName:""The name of the starting ship"", int speed: 100)--Starts a specific spaceship with a specific speed
would be called like this:
StartSpaceship(shipName: ""Enterprise"", speed: 99999)
";
        }

        private string GetCallAsTextPrompt3000(FunctionCall nextFunctionCall)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(nextFunctionCall.Name);
            sb.Append("(");
            for (int i = 0; i < nextFunctionCall.Parameters.Count; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(nextFunctionCall.Parameters[i].Name);
                sb.Append(": \"");
                sb.Append(nextFunctionCall.Parameters[i].Value.ToString());
                sb.Append("\"");
            }
            sb.Append(")");
            return sb.ToString();
        }


        private async Task<string> InvokePluginAsync(FunctionCall functionCall)
        {
            List<string> args = new List<string>();
            foreach (var paraam in functionCall.Parameters)
            {
                args.Add($"{paraam.Name} : {paraam.Value}");
            }
            Console.WriteLine($">>invoking {functionCall.Name} with parameters {string.Join(",", args)}");
            // Iterate over each plugin in the kernel
            foreach (var plugin in _kernel.Plugins)
            {
                // Check if the plugin has a function with the same name as the function call
                var function = plugin.FirstOrDefault(f => f.Name == functionCall.Name);
                if (function != null)
                {
                    // Create a new context for the function call
                    KernelArguments context = new KernelArguments();

                    // Add the function parameters to the context
                    foreach (var parameter in functionCall.Parameters)
                    {
                        context[parameter.Name] = parameter.Value;
                    }

                    // Invoke the function
                    var result = await function.InvokeAsync(_kernel, context);

                    Console.WriteLine($">>Result: {result.ToString()}");
                    return result.ToString();
                }
            }

            throw new Exception("Function does not exist in the kernel.");
        }


        private string GetTemplatesAsTextPrompt3000(KernelPluginCollection plugins)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var plugin in plugins)
            {
                foreach (var functionimpl in plugin)
                {
                    var function = functionimpl.Metadata;
                    sb.Append(function.Name);
                    sb.Append("(");
                    for (int i = 0; i < function.Parameters.Count; i++)
                    {
                        if (i > 0)
                            sb.Append(", ");
                        sb.Append(function.Parameters[i].ParameterType.Name);
                        sb.Append(" ");
                        sb.Append(function.Parameters[i].Name);
                        sb.Append(": \"");
                        sb.Append(function.Parameters[i].Description);
                        sb.Append("\"");
                    }
                    sb.Append(")");
                    sb.Append("--");
                    sb.Append(function.Description);
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }


    }
}

[简体中文](./README.zh.md) | English

# SimpleRAG

## A simple RAG demo based on WPF and Semantic Kernel.✨

SimpleRAG is a basic RAG application based on WPF and Semantic Kernel, which can be used for learning and understanding how to build RAG applications using Semantic Kernel.

## Primary functions✨

### AI Chatting

Support all large language models compatible with the OpenAI format:

![image-20240819163701855](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163701855.png)

### File Chat

Here is a private document as follows:

```csharp
会议主题：《如何使用C#提升工作效率》
参会人员：张三、李四、王五
时间：2024.9.26 14:00-16:00
会议内容：
1. 自动化日常任务
许多日常任务可以自动化，从而节省时间和精力。例如，如果你需要定期处理大量数据，可以使用C#编写脚本来自动化数据导入、清理和分析过程。
2. 构建自定义工具
C#可以用来构建各种自定义工具，以满足特定需求。
3. 集成现有系统
C#可以轻松集成现有的系统和API，从而提高工作效率。
4. 开发插件和扩展
许多应用程序支持插件和扩展，C#可以用来开发这些插件，以增强现有应用程序的功能。
5. 优化现有代码
C#提供了丰富的库和框架，可以帮助你优化现有代码，提高性能和可维护性。
```

Support for conversing based on file content:

![image-20240926121558886](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121558886.png)

### Text Embedding

Support all embedding models compatible with OpenAI formats:

![image-20240819163900106](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163900106.png)

### Simple RAG answer

Simple RAG response effect:

![image-20240819164221306](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164221306.png)

Compare answers without using RAG:

![image-20240819164322893](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164322893.png)

### Test Function Calling

Test Function Calling response effectiveness:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162408978.png)

Compare the effects of not using FunctionCalling:

![image-20240828162455519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162455519.png)

This method has been tested and is available for LLMs:

| 平台       | 可用模型                                                     |
| ---------- | ------------------------------------------------------------ |
| 硅基流动   | Llama-3.1-405/70/8B、Llama-3-70/8B-Instruct、DeepSeek-V2-Chat、deepseek-llm-67b-chat、Qwen2-72/57/7/1.5B-Instruct、Qwen2-57B-A14B-Instruct、Qwen1.5-110/32/14B-Chat、Qwen2-Math-72B-Instruct、Yi-1.5-34/9/6B-Chat-16K、internlm2_5-20/7b-chat |
| 讯飞星火   | Spark Lite、Spark Pro-128K、Spark Max、Spark4.0 Ultra        |
| 零一万物   | yi-large、yi-medium、yi-spark、yi-large-rag、yi-large-fc、yi-large-turbo |
| 月之暗面   | moonshot-v1-8k、moonshot-v1-32k、moonshot-v1-128k            |
| 智谱AI     | glm-4-0520、glm-4、glm-4-air、glm-4-airx、glm-4-flash、glm-4v、glm-3-turbo |
| DeepSeek   | deepseek-chat、deepseek-coder                                |
| 阶跃星辰   | step-1-8k、step-1-32k、step-1-128k、step-2-16k-nightly、step-1-flash |
| Minimax    | abab6.5s-chat、abab5.5-chat                                  |
| 阿里云百炼 | qwen-max、qwen2-math-72b-instruct、qwen-max-0428、qwen2-72b-instruct、qwen2-57b-a14b-instruct、qwen2-7b-instruct |

The above may not be comprehensive, and there are still some models that have not been tested. Everyone is welcome to continue to supplement.

## Quick Start🚀

Visited the SimpleRAG's GitHub reference and noticed there is a "Releases" section here:

![image-20240822100649148](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100649148.png)

Clicking on SimpleRAG-v0.0.1, there are two zip files, one depends on the .NET 8.0-windows framework, and the other is standalone:

![image-20240822100817138](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100817138.png)

The package that depends on the framework will be smaller, while the standalone package will be larger. If your computer has already installed the .NET 8.0-windows framework, you can choose the package that depends on the framework. Considering that most people may not have the .NET 8.0-windows framework installed, I will demonstrate with the standalone package. Click on the zip file, and it will start downloading:

![image-20240822101244281](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101244281.png)

Decompress this compressed package:

![image-20240822101450182](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101450182.png)

Open the appsettings.json file:

![image-20240822101600329](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101600329.png)

The appsettings.json file is as shown below:

![image-20240822101740892](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101740892.png)

Defaults to using the SiliconCloud API,just enter your SiliconCloud API Key, and it will look like this upon completion:

![image-20240822102046293](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102046293.png)

Click on SimpleRAG.exe now to run the program:

![image-20240822102117959](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102117959.png)

After the program runs, it is as shown below:

![image-20240822102215516](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102215516.png)

First, test the configuration through AI chat:

![image-20240822102306935](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102306935.png)

Configuration has been successful.

Now let's test the embedding.

Let's start with a simple text for testing: 

```csharp
小n最喜欢吃的水果是西瓜。
```

![image-20240822102445438](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102445438.png)

Embedding successful:

![image-20240822102504358](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102504358.png)

This demo program uses Sqlite database for convenient storage of text vectors, as can be seen here:

![image-20240822102554159](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102554159.png)

If you have a database management software, you would find that the text has been stored in the form of vectors in the Sqlite database.

![image-20240822102822026](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102822026.png)

Now begins the test of RAG's response effect: 

![image-20240822102901171](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102901171.png)

Compare the effectiveness of answers without using RAG:

![image-20240822102956395](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102956395.png)

It can be found that large language models have no knowledge of our private data that we want to inquire about, and there is a hallucination.

The principles of implementation are introduced in this article of mine, and friends who are interested can take a look:

[SemanticKernel/C#：检索增强生成(RAG)简易实践](https://mp.weixin.qq.com/s/0Q0vk9SPuH6k6AnMffp8Iw)

## Building from source code🚀

Clone the repository to local, open the appsettings.example.json file:

![image-20240819164816557](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164816557.png)

The appsettings.example.json file is shown as follows:

![image-20240819164844061](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164844061.png)

ChatAI is used for configuring conversation models, Embedding for configuring embedding models, and TextChunker for setting the document chunk size.

Taking SiliconCloud as an example, simply enter your API key and rename the file to appsettings.json, or create a new appsettings.json file.

The completed configuration would look like this:

![image-20240819165255285](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165255285.png)

IDE: VS2022

.NET Version: .NET 8

Open the solution, and the project structure is as shown below:

![image-20240819165459846](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165459846.png)



Run the program: 

![image-20240819165551772](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165551772.png)

Test AI chat:

![image-20240819165652624](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165652624.png)

Test embedding: 

![image-20240819165803024](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165803024.png)

Using Sqlite to save vectors, you can find this database in the Debug folder:

![image-20240819165854807](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165854807.png)

Open the database as follows:

![image-20240819170059576](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819170059576.png)

Test RAG response: 

![image-20240819170128226](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819170128226.png)

## Other configurations🚀

You can also freely make other configurations, such as using the dialogue and embedding models from Ollama for local offline scenarios, setting up other online dialogue models, and utilizing the embedding model from local Ollama.

## Finally✨

If it helps you, giving me a star✨ is the greatest support. 😊

If you still encounter issues after reading this guide, feel free to contact me through the public account:

![qrcode_for_gh_eb0908859e11_344](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)
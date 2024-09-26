# SimpleRAG-v1.0.3：增加文件对话功能

Kimi上有一个功能，就是增加文件之后对话，比如我有如下一个私有文档：

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

我上传这个文档之后，提问文档中的内容，如下所示：

![image-20240926121012250](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121012250.png)

![image-20240926121043360](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121043360.png)

![image-20240926121113883](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121113883.png)

![image-20240926121155095](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121155095.png)

那么我们自己该如何实现这个功能呢？

我们之前接触过RAG，那可以用来应对文档内容超出模型上下文的情况，但是很多时候，我们只是上传一份简单的文档，文档内容并不多，而且也不需要进行存储，那么这时候，就可以直接读文件内容，不用RAG。

以下是自己实现的效果：

![image-20240926121558886](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121558886.png)

![image-20240926121806875](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121806875.png)

![image-20240926121839435](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121839435.png)

![image-20240926121915911](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926121915911.png)

也实现了同样的效果。

## 实现要点

```csharp
 public async IAsyncEnumerable<string> GetAIResponse3(string question,string filePath)
 {
     string fileContent = File.ReadAllText(filePath);
     string skPrompt = """
                        获取到的文件内容：{{$FileContent}}。
                        根据获取到的信息回答问题：{{$Question}}。
                        如果文件内容中没有提到，直接回答不知道。
                     """;
     await foreach (var str in _kernel.InvokePromptStreamingAsync(skPrompt, new() { ["FileContent"] = fileContent, ["Question"] = question }))
     {
         yield return str.ToString();
     }
 }
```

使用这个简单的prompt即可实现。

## 快速体验

我在github上发布了依赖框架与不依赖框架的版本。解压之后在appsettings.json文件中填入你的api key即可开始体验。

![image-20240926144248293](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240926144248293.png)

SimpleRAG地址：https://github.com/Ming-jiayou/SimpleRAG

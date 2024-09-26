简体中文|[English](./README.md) 

# SimpleRAG

## A simple RAG demo based on WPF and Semantic Kernel.✨

SimpleRAG是基于WPF与Semantic Kernel实现的一个简单的RAG应用，可用于学习与理解如何使用Semantic Kernel构建RAG应用。

## 主要功能✨

### AI聊天

支持所有兼容OpenAI格式的大语言模型：

![image-20240819163701855](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163701855.png)

### 文本嵌入

支持所有兼容OpenAI格式的嵌入模型：

![image-20240819163900106](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163900106.png)

### 简单的RAG回答

简单的RAG回答效果：

![image-20240819164221306](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164221306.png)

对比不使用RAG的回答：

![image-20240819164322893](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164322893.png)

### 测试Function Calling

测试Function Calling回答效果：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162408978.png)

对比一下不使用FunctionCalling的效果：

![image-20240828162455519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162455519.png)

经过测试这种方法可用的LLM：

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

以上不一定完备，还有一些模型没测，欢迎大家继续补充。

## 快速体验🚀

来到SimpleRAG的GitHub参考，注意到这里有个Releases：

![image-20240822100649148](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100649148.png)

点击SimpleRAG-v0.0.1，有两个压缩包，一个依赖net8.0-windows框架，一个独立：

![image-20240822100817138](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100817138.png)

依赖框架的包会小一些，独立的包会大一些，如果你的电脑已经装了net8.0-windows框架可以选择依赖框架的包，考虑到可能大部分人不一定装了net8.0-windows框架，我以独立的包做演示，点击压缩包，就在下载了：

![image-20240822101244281](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101244281.png)

解压该压缩包：

![image-20240822101450182](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101450182.png)

打开appsettings.json文件：

![image-20240822101600329](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101600329.png)

appsettings.json文件如下所示：

![image-20240822101740892](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101740892.png)

默认是使用SiliconCloud的api，只需填入你的SiliconCloud的Api Key即可，完成后，如下所示：

![image-20240822102046293](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102046293.png)

现在点击SimpleRAG.exe即可运行程序：

![image-20240822102117959](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102117959.png)

程序运行之后，如下所示：

![image-20240822102215516](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102215516.png)

先通过AI聊天测试配置是否成功：

![image-20240822102306935](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102306935.png)

配置已经成功。

现在来测试一下嵌入。

先拿一个简单的文本进行测试：

```csharp
小n最喜欢吃的水果是西瓜。
```

![image-20240822102445438](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102445438.png)

嵌入成功：

![image-20240822102504358](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102504358.png)

这个Demo程序为了方便存储文本向量使用的是Sqlite数据库，在这里可以看到：

![image-20240822102554159](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102554159.png)

如果你有数据库管理软件的话，打开该数据库，会发现文本已经以向量的形式存入Sqlite数据库中：

![image-20240822102822026](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102822026.png)

现在开始测试RAG回答效果：

![image-20240822102901171](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102901171.png)

对比不使用RAG的回答效果：

![image-20240822102956395](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102956395.png)

可以发现大语言模型根本不知道我们想问的私有数据的事情，出现了幻觉。

以上就成功使用SiliconCloud体验了SimpleRAG。

实现的原理，在我的这篇文章中有进行介绍，感兴趣的朋友可以看看：

[SemanticKernel/C#：检索增强生成(RAG)简易实践](https://mp.weixin.qq.com/s/0Q0vk9SPuH6k6AnMffp8Iw)

## 从源码构建🚀

git clone到本地，打开appsettings.example.json文件：

![image-20240819164816557](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164816557.png)

如下所示：

![image-20240819164844061](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164844061.png)

ChatAI用于配置对话模型，Embedding用于配置嵌入模型，TextChunker用于配置文档切片大小。

还是以SiliconCloud为例，只需填入你的api key 并将文件名改为appsettings.json，或者新建一个appsettings.json即可。

配置完成如下所示：

![image-20240819165255285](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165255285.png)

IDE:VS2022 

.NET 版本：.NET 8

打开解决方案，项目结构如下所示：

![image-20240819165459846](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165459846.png)



运行程序：

![image-20240819165551772](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165551772.png)

测试AI聊天：

![image-20240819165652624](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165652624.png)

测试嵌入：

![image-20240819165803024](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165803024.png)

使用的是Sqlite保存向量，可以在Debug文件夹下找到这个数据库：

![image-20240819165854807](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819165854807.png)

打开该数据库，如下所示：

![image-20240819170059576](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819170059576.png)

测试RAG回答：

![image-20240819170128226](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819170128226.png)

## 最后✨

如果对您有所帮助，点个star✨，就是最大的支持😊。

如果您看了这个指南，还是遇到了问题，欢迎通过公众号联系我：

![qrcode_for_gh_eb0908859e11_344](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)

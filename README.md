# SimpleRAG
A simple RAG demo based on WPF and Semantic Kernel.

SimpleRAG是基于WPF与Semantic Kernel实现的一个简单的RAG应用，可用于学习与理解如何使用Semantic Kernel构建RAG应用。

## 主要功能

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

## 从源码构建

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

## 其他配置

您还可以自由的进行其他配置，比如使用Ollama中的对话模型与嵌入模型用于本地离线场景，配置其他的在线对话模型，使用本地Ollama中的嵌入模型等。

## 最后

如果对您有所帮助，点个star✨，就是最大的支持😊。

如果您看了这个指南，还是遇到了问题，欢迎通过公众号联系我：

![qrcode_for_gh_eb0908859e11_344](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)

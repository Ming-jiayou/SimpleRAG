[简体中文](./README.zh.md) | English

# SimpleRAG

## A simple RAG demo based on WPF and Semantic Kernel.✨

SimpleRAG is a basic RAG application based on WPF and Semantic Kernel, which can be used for learning and understanding how to build RAG applications using Semantic Kernel.

## Primary functions✨

### AI Chatting

Support all large language models compatible with the OpenAI format:

![image-20240819163701855](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163701855.png)

### Text Embedding

Support all embedding models compatible with OpenAI formats:

![image-20240819163900106](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819163900106.png)

### Simple RAG answer

Simple RAG response effect:

![image-20240819164221306](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164221306.png)

Compare answers without using RAG:

![image-20240819164322893](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240819164322893.png)

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

Now let's test a slightly more complex text: 

```csharp
一夜之间，郑钦文的名字霸占了中国各大媒体的头条，不仅仅是体育板块。

人们讨论着她在奥运会网球女单赛场上的强势表现，讨论着她那起起伏伏的网球人生，也讨论着她在未来的商业价值，甚至讨论着她有没有机会在未来超过李娜的历史地位。

但对于这位才21岁的女孩来说，在赢得奥运金牌之后，对她自身而言最大的帮助就是，她可以抱着轻松的心态站上赛场。

“我这次终于光宗耀祖了，终于可以好好地炫耀了。”在赛后一天，郑钦文接受了包括澎湃新闻（www.thepaper.cn）在内的几家媒体的群访，在采访中，她收起了赛场上“女王的霸气”，展现出了一个邻家女孩的亲和力，她甚至借着媒体的镜头和父亲喊话，“以后打不好别给我压力嗷，因为这块金牌代表了一切！”

“老爸的眼睛哭肿了”

就在一天前的赛后发布会上，当一位外国记者问郑钦文，“你的胜利是不是源于国际化的团队”时，郑钦文没有否认，但她也强调，“我的成功离不开家人的支持和鼓励。”

郑钦文自己说，她能够坚持网球这条路得益于母亲在她12岁的时候辞掉工作全身心照顾她，也是因为她父亲的严格要求，才能不断推动她进步，“我拥有一个国际化的团队，因为我一直希望能够变得更强，但是无论我走到哪里，我永远都不会忘记我是一名中国运动员。”

过去几天，在接受媒体采访时，郑钦文不止一次提到了他的父母，在他看来，亲情的力量也是她在赛场上取得成功的一个精神动力。

赛后一天，作为耐克签约运动员，现身法兰西大球场旁的耐克运动员之家的郑钦文又提到了她的父母，“我很少见地看到我老爸居然哭了，眼睛都肿了。”

郑钦文透露，她的父亲从小就和她灌输奥运会的重要性，在打奥运之前她拿下WTA250的冠军时，她的父亲都没有怎么夸奖她，只说要好好准备接下来的奥运会，“在爸爸眼中，奥运会比大满贯还重要。”

此外，郑钦文还透露，在她十四五岁那个时候，她的父亲甚至要卖掉房子供她打球，“因为他看得出来我很有潜力，所以他想倾尽一切来圆这个网球梦。”

在感谢父母的同时，郑钦文也俏皮地说，这枚奥运金牌可以帮她躲过自己父亲的唠叨和责骂，“我觉得这次奥运会的冠军也给我带来了一些压力上的减轻，因为我觉得我爸妈肯定在以后会更享受我在网球场上的状态，就可能不再会只是拘泥于成绩了。”
两年时间，郑钦文正在快速地爬上世界女子网坛的巅峰，但这条上坡的路，郑钦文花费了不少力气。除了父母买房供她打球，她自己也每一天都在督促自己。

在采访中，郑钦文就向现场记者们展示了她布满老茧的手，“这个地方倒还好，其实冬天的时候它会产生血泡，但夏天的时候就还好。”

郑钦文坦言，网球运动员承受压力最大的地方其实不是手，而是脚，“你可以看到我们的脚底板经常会磨出血泡或者水泡之类的东西，这个就很难展示了，但是我觉得这是作为运动员应该需要去承受的一部分，因为运动它就是一个不断突破自我极限的过程。”

郑钦文在今年的巴黎奥运会上就突破了极限，不仅为中国网球创造历史，而且在个人层面也击败了当今的世界第一斯瓦泰克。

那么，这次奥运经历会成为郑钦文的转折点吗？

郑钦文自己说，每场比赛都有可能成为人生的转折点，因为网球比赛不只是胜利结果那么简单，它需要六场乃至七场胜利才可能赢下一个冠军。

“决赛带给我的惊心动魄，带给我的人生经验肯定是无与伦比的，我会永远铭记那一刻，也包括以后我在任何一个困难和低谷时期，因为不管怎么样，我知道职业生涯它都会有高开低走或者低开高走的一个跌宕起伏的过程，这很正常。”郑钦文说，她现在很感谢那个始终没有想过放弃的自己，“很奇怪，我一路走来即使有过再艰难的时刻，就是我哭得再厉害，因为输掉比赛怎么样，我也从来都没有想过要放弃。”

胜利，就是对这份坚持最好的褒奖。“我觉得真的一切都是值得的。”
为中国体育拿下第一枚奥运网球女单金牌之后，郑钦文也希望这次突破可以让网球这项运动进入大众视野，甚至让更多青少年萌生把网球作为自己职业运动的想法。

“网球确实是一项很国际化的运动，也是一项高性价比的运动。”郑钦文是一个性格直率的女孩，面对着媒体的长枪短炮，她也不太会去刻意修饰自己的一些真实想法，“相比于其他女子球类运动，网球属于奖金很高的一个项目，我觉得这也是很少的、努力跟收获能够成正比的一个运动。”

在郑钦文看来，“网球相对于其他项目更吸引我的一点就是，虽然前期付出真的非常多，多到我已经不再想去回忆，但是当收获的那一刻，你会发现付出是可以拥有回报的。”

她将网球描述成“在阳光底下的运动”，“它可以产生更多的维生素、更多的多巴胺分泌；网球还是一项非常全面性的运动，几乎可以锻炼到所有，我觉得这都是对身体很健康的。”

如今，郑钦文就成为了一道阳光，她希望照到更多愿意接触网球的年轻人。

而对于外界那些“打网球想要冲击职业需要花费很多钱”的说法，郑钦文并不是完全认同，“究竟要花多少钱很难去确定，因为每个球员选择的道路不一样。有的球员可能是国家自己的网协赞助培养，比如法国体系的球员有法网，网协可以赚很多钱，所以说他们在球员这方面会付出得很多；但是像在中国像我自己，可能完全就是我家里人一步一步支持我，把我培养了出来。”

在西班牙训练的那段时间，郑钦文就感受到了当地相当吸引人的网球氛围，更重要的是，在西班牙学网球的花费甚至更低，“西班牙球员在费用方面可能没有那么大的压力，但是因为我们是中国人，去国外一个不属于我们自己的家乡去训练，所以费用会更高一点。”

“真的很希望网球文化可以在中国流行起来。”在世界各地感受过网球的氛围后，郑钦文期待有一天中国也会变成那样，“我希望未来有一天我们中国在网球这方面也能做到这样。”
```

After the same text is embedded, test the effect of RAG:

![image-20240822103413583](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822103413583.png)

![image-20240822103533115](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822103533115.png)

Compare the effectiveness of answers without using RAG: 

![image-20240822103631774](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822103631774.png)

The knowledge of large language models is limited to the information used during training, and some recent content may not be known to the large language models.

Above is a successful experience with SimpleRAG on SiliconCloud.

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
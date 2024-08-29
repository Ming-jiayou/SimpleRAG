# 在SimpleRAG中使用SiliconCloud快速测试Function Calling

## Funcion Calling介绍

函数调用允许您将模型如gpt-4o与外部工具和系统连接起来。这对于许多事情都很有用，比如为AI助手赋能，或者在你的应用程序与模型之间建立深度集成。

如果您了解或者使用过Semantic Kernel可能会发现除了OpenAI支持Function Calling的模型之外，自动函数调用好像并不好用，国产大模型几乎都不能使用，由于想解决这个问题，在GitHub上找到了一个大佬的方法。

GitHub地址：https://github.com/Jenscaasen/UniversalLLMFunctionCaller

大佬是通过提示工程与Semantic Kernel中调用本地函数的原理来做的，我看了大佬的代码，将提示词改为了中文，可能会更适用于国产大模型。

之前写了一篇文章：[如何让其他模型也能在SemanticKernel中调用本地函数](https://mp.weixin.qq.com/s/swPPTyIJa-2OJcyycBVJNQ)介绍了这个方法。

但是当时自己并没有开源项目，感兴趣的朋友，没有办法快速地上手体验，只能自己重新来一遍，现在已将这部分内容集成到我的开源项目SimpleRAG中，感兴趣的朋友只需填入自己的API Key即可快速体验，也可以方便地查看代码了。

GitHub地址：https://github.com/Ming-jiayou/SimpleRAG

## 一种通用的Function Calling方法

在开始介绍之前，先看一下效果：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162408978.png)

对比一下不使用FunctionCalling的效果：

![image-20240828162455519](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162455519.png)

再来一个示例：

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162633560.png)

对比不使用Function Calling的效果：

![image-20240828162754671](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828162754671.png)

## 在SimpleRAG中使用SiliconCloud快速测试Function Calling

[SiliconCloud](https://siliconflow.cn/zh-cn/siliconcloud) 基于优秀的开源基础模型，提供高性价比的 GenAI 服务。

不同于多数大模型云服务平台只提供自家大模型 API，[SiliconCloud](https://siliconflow.cn/zh-cn/siliconcloud)上架了包括 Qwen、DeepSeek、GLM、Yi、Mistral、LLaMA 3、SDXL、InstantID 在内的多种开源大语言模型及图片生成模型，用户可自由切换适合不同应用场景的模型。

更重要的是，SiliconCloud 提供**开箱即用**的大模型推理加速服务，为您的 GenAI 应用带来更高效的用户体验。

对开发者来说，通过 SiliconCloud 即可一键接入顶级开源大模型。拥有更好应用开发速度和体验的同时，显著降低应用开发的试错成本。

SiliconCloud平台提供了多种模型，用于测试模型的能力，很有帮助，而且还有一些模型是免费调用的。

![image-20240828173434685](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828173434685.png)

![image-20240828173711549](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828173711549.png)

并且SiliconCloud兼容OpenAI的格式并且推理速度很快，因此建议使用SiliconCloud。

只需在SimpleRAG项目的appsettings.json文件中填入你想使用的模型名称即可：

![image-20240828174210918](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240828174210918.png)

模型名称可在SiliconCloud的官网查看，这样就可以快速测试SiliconCloud中提供的平台是否可以在SimpleRAG中进行Function Calling。

经过我的测试，SiliconCloud中提供的模型，适用于这个方法的有如下这些：

| 平台     | 可用模型                                                     |
| -------- | ------------------------------------------------------------ |
| 硅基流动 | Llama-3.1-405/70/8B、Llama-3-70/8B-Instruct、DeepSeek-V2-Chat、deepseek-llm-67b-chat、Qwen2-72/57/7/1.5B-Instruct、Qwen2-57B-A14B-Instruct、Qwen1.5-110/32/14B-Chat、Qwen2-Math-72B-Instruct、Yi-1.5-34/9/6B-Chat-16K、internlm2_5-20/7b-chat |

学习并掌握了Function Calling之后，就可以开发更多有趣的功能了。
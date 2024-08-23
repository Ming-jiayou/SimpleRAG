# Hands-on Tutorial for Local OfflineExperience with Ollama using SimpleRAG

## Introduction to Ollama

Ollama is an open-source project focused on the development and deployment of large language models, particularly models like LLaMA, for generating high-quality text and conducting complex natural language processing tasks. The goal of Ollama is to make running and using large language models easier and more accessible, without the need for complex infrastructure or in-depth machine learning knowledge.

![image-20240822110024317](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822110024317.png)

GitHub地址：https://github.com/ollama/ollama

## What is RAG?

Retrieval-Augmented Generation (RAG) is a natural language processing approach that combines both retrieval and generation techniques, primarily used to enhance the performance of text generation tasks such as question-answering systems, dialogue systems, text summarization, and document generation. The RAG model strengthens the accuracy and richness of the generation model by introducing a retrieval module on top of it. 

In traditional generation models, the model relies entirely on patterns and statistical information learned from the training data to produce text, which can lead to a lack of novelty or accuracy in the generated content. In contrast, the retrieval module can fetch relevant information from external knowledge bases or documents, providing this information as additional input to the generation model, and thus helping to produce more accurate, enriched, and specific text.

The workflow of the RAG model is as follows:

1. **Retrieval Stage**: The model first retrieves the most relevant documents or passages from an external knowledge base based on the input query or context.
2. **Fusion Stage**: The retrieved information is fused with the input query or context, forming an enhanced input.
3. **Generation Stage**: The enhanced input is fed into the generation model, which then generates the final text output based on this information.

By using this method, the RAG model can leverage external knowledge during the generation process, which increases the accuracy and richness of the generated text, while also enhancing the interpretability of the model, as the generated text can be traced back to specific sources. RAG models show significant advantages when dealing with tasks that require a large amount of domain knowledge or specific factual information.

## Introduction to SimpleRAG

A simple RAG demo based on WPF and Semantic Kernel.

SimpleRAG is a basic RAG application based on WPF and Semantic Kernel, which can be used for learning and understanding how to build a simple RAG application using Semantic Kernel.

![image-20240822100239041](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100239041.png)

GitHub地址：https://github.com/Ming-jiayou/SimpleRAG

## Primary functions

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

## Using Ollama offline to experience SimpleRAG

Visited the SimpleRAG's GitHub reference and noticed there is a "Releases" section here:

![image-20240822100649148](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100649148.png)

Clicking on SimpleRAG-v0.0.1, there are two zip files, one depends on the .NET 8.0-windows framework, and the other is standalone:

![image-20240822100817138](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822100817138.png)

The package that depends on the framework will be smaller, while the standalone package will be larger. If your computer has already installed the .NET 8.0-windows framework, you can choose the package that depends on the framework. Considering that most people may not have the .NET 8.0-windows framework installed, I will demonstrate with the standalone package. Click on the zip file, and it will start downloading:

![image-20240822101244281](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101244281.png)

Decompress this compressed package:

![image-20240822101450182](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101450182.png)

Open the appsettings.json file:![image-20240822101600329](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101600329.png)

The appsettings.json file is as shown below:

![image-20240822101740892](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822101740892.png)

n your computer, launch Ollama, and in the command line, enter "ollama list" to view the models that have been downloaded:![image-20240822113619155](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822113619155.png)

Due to my computer's configuration not being very good, for the conversation model, I will use "gemma2:2b" as an example, and for the embedding model, "bge-m3:latest". The appsettings.json file should be written like this:

![image-20240822113903239](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822113903239.png)

Enter the address of Ollama as the Endpoint, the default is http://localhost:11434. Ollama does not require an API Key, just enter anything.

Now click on SimpleRAG.exe to run the program:![image-20240822102117959](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102117959.png)

After the program runs, it looks as follows:

![image-20240822102215516](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102215516.png)

First, test the configuration through AI chat:

![image-20240822114300380](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822114300380.png)

The configuration has been successful.

Now let's test the embedding.

First, let's use a simple text for the test:

```csharp
小k最喜欢的编程语言是C#。
```

![image-20240822114549483](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822114549483.png)

Embedded successfully:

![image-20240822114618014](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822114618014.png)

This demo program uses Sqlite database for convenient storage of text vectors, as shown here:

![image-20240822102554159](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822102554159.png)

If you have a database management software, you would find that the text has been stored in the form of vectors in the SQLite database:

![image-20240822114904572](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822114904572.png)

ow begins the test of RAG's response effectiveness:![image-20240822115055457](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822115055457.png)

Compare the effectiveness of answers without using RAG:

![image-20240822115204218](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822115204218.png)

It can be observed that large language models have no knowledge of our private data that we want to inquire about.

Now let's test a more complex piece of text,after the same text is embedded, test the effect of RAG:

![image-20240822115513523](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822115513523.png)

The RAG answer failed because I am using too few model parameters, which are not powerful enough. If you have a good computer configuration, you can switch to a smarter model; if your computer configuration is not good, you can choose to hybridize by using the online dialogue model API and the local embedding model in Ollama.

## Experience SimpleRAG using the online dialogue API and local Ollama embedding model

appsettings.json can be written like this:

![image-20240822120347160](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822120347160.png)

Testing RAG's effectiveness: 

![image-20240822120526269](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822120526269.png)

RAG has still failed.

Model changed to meta-llama/Meta-Llama-3.1-8B-Instruct：

![image-20240822120706462](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822120706462.png)

Model changed to google/gemma-2-9b-it：

![image-20240822121018509](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822121018509.png)

Model changed to Qwen/Qwen2-72B-Instruct：

![image-20240822121616949](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822121616949.png)

Find the cause through the source code:

![image-20240822122700793](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822122700793.png)

By setting the relevance to 0.3, relevant texts can be found, but it feels like this could also lead to issues. As the number of documents increases, it becomes easy to find unrelated ones. Later, the appsettings.json file will include a configuration for relevance:

![image-20240822122749303](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822122749303.png)

Now let's test Qwen/Qwen2-7B-Instruct again:

![image-20240822123253249](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822123253249.png)

That's fine. 

Now, translating your input into English, with no additional content: 

![image-20240822123617132](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20240822123617132.png)

## Finally

If it helps you, giving me a star✨ is the greatest support. 😊

If you still encounter issues after reading this guide, feel free to contact me through the public account:

![](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/qrcode_for_gh_eb0908859e11_344.jpg)
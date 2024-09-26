using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleRAG.Interface;
using SimpleRAG.Services;
using SimpleRAG.ViewModels.Pages;
using SimpleRAG.ViewModels.Windows;
using SimpleRAG.Views.Pages;
using SimpleRAG.Views.Windows;
using SimpleRAG.Common.Options;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows.Threading;
using Wpf.Ui;

namespace SimpleRAG
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(System.AppContext.BaseDirectory)); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                // Page resolver service
                services.AddSingleton<IPageService, PageService>();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Service containing navigation, same as INavigationWindow... but without window
                services.AddSingleton<INavigationService, NavigationService>();

                // Main window with navigation
                services.AddSingleton<INavigationWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
             
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<AIChat>();
                services.AddSingleton<AIChatViewModel>();
                services.AddSingleton<AIEmbedding>();
                services.AddSingleton<AIEmbeddingViewModel>();
                services.AddSingleton<FunctionCalling>();
                services.AddSingleton<FunctionCallingViewModel>();
                services.AddSingleton<TranslationAIAgent>();
                services.AddSingleton<TranslationAIAgentViewModel>();
                services.AddSingleton<AIFileChat>();
                services.AddSingleton<AIFileChatViewModel>();

                services.AddScoped<HttpClient>();

                services.AddSingleton<ISemanticKernelService, SemanticKernelService>();

                // 创建配置构建器
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                // 构建配置
                IConfiguration configuration = builder.Build();

                // 直接从配置中读取复杂类型
                ChatAIOption openAIOption = configuration.GetSection("ChatAI").Get<ChatAIOption>();
                EmbeddingOption embeddingOption = configuration.GetSection("Embedding").Get<EmbeddingOption>();
                TextChunkerOption textChunkerOption = configuration.GetSection("TextChunker").Get<TextChunkerOption>();
            

                // 注册配置对象到依赖注入容器
                services.AddSingleton(openAIOption);
                services.AddSingleton(embeddingOption);
                services.AddSingleton(textChunkerOption);

            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}

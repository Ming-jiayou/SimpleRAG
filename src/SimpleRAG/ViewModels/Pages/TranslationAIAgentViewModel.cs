using SimpleRAG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.ViewModels.Pages
{
    public partial class TranslationAIAgentViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;
        public TranslationAIAgentViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [ObservableProperty]
        private string askText = """
            帮我将
            hello，world
            翻译成中文之后，写入文件，文件路径为：D:\桌面\test.md
            """;
        
        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        [RelayCommand]
        private async Task RunTranslationAIAgent()
        {
            if (ResponseText != "")
            {
                ResponseText = "";
            }
            if (AskText != "")
            {
                ProgressRingVisible = Visibility.Visible;
                var result = await _semanticKernelService.RunTranslationAIAgentSampleAsync(AskText);
                ProgressRingVisible = Visibility.Hidden;
                int chunkSize = 4; // 每次显示的字符数
                for (int i = 0; i < result.Length; i += chunkSize)
                {
                    string chunk = result.Substring(i, Math.Min(chunkSize, result.Length - i));
                    ResponseText += chunk;
                    await Task.Delay(100);
                }
            }
            else
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                    "请先输入问题！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }

        }
    }
}

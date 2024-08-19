using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleRAG.Interface;

namespace SimpleRAG.ViewModels.Pages
{
    public partial class AIChatViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;
        public AIChatViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [ObservableProperty]
        private string askText = "";

        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        [RelayCommand]
        private async Task AskAI()
        {
            if (ResponseText != "")
            {
                ResponseText = "";
            }
            if (AskText != "")
            {
                ProgressRingVisible = Visibility.Visible;
                var result = await _semanticKernelService.GetAIResponse(AskText);
                ProgressRingVisible = Visibility.Hidden;
                int chunkSize = 4; // 每次显示的字符数
                for (int i = 0; i < result.Answer.Length; i += chunkSize)
                {
                    string chunk = result.Answer.Substring(i, Math.Min(chunkSize, result.Answer.Length - i));
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

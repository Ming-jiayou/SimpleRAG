using SimpleRAG.Interface;
using SimpleRAG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace SimpleRAG.ViewModels.Pages
{
    public partial class AIEmbeddingViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;

        public AIEmbeddingViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }
        [ObservableProperty]
        private string index = "";

        [ObservableProperty]
        private string input = "";

        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        private bool _isInitialized = false;
        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            _isInitialized = true;
        }

        [RelayCommand]
        private async Task Embedding()
        {
            if (string.IsNullOrWhiteSpace(Index) || string.IsNullOrWhiteSpace(Input))
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                   "请确保Index与Input都已输入！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }
            else
            {
                ProgressRingVisible = Visibility.Visible;
                var queryModel = new QueryModel { Index = Index, Text = Input };
                await _semanticKernelService.Embedding(queryModel);
                ProgressRingVisible = Visibility.Hidden;
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                   "Embedding成功！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }
        }

        [RelayCommand]
        private async Task RAGReply()
        {
            if (ResponseText != "")
            {
                ResponseText = "";
            }
            if (string.IsNullOrWhiteSpace(Index) || string.IsNullOrWhiteSpace(Input))
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                   "请确保Index与Input都已输入！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }
            else
            {
                ProgressRingVisible = Visibility.Visible;
                var queryModel = new QueryModel { Index = Index, Text = Input };
                var result = await _semanticKernelService.GetRAGResponse(queryModel);
                ProgressRingVisible = Visibility.Hidden;
                int chunkSize = 4; // 每次显示的字符数
                for (int i = 0; i < result.Answer.Length; i += chunkSize)
                {
                    string chunk = result.Answer.Substring(i, Math.Min(chunkSize, result.Answer.Length - i));
                    ResponseText += chunk;
                    await Task.Delay(100);
                }
            }
        }
    }
}

using SimpleRAG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SimpleRAG.ViewModels.Pages
{
    public partial class AIFileChatViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;
        public AIFileChatViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [ObservableProperty]
        private string askText = "";

        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private string selectedFile = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        [RelayCommand]
        private void ChooseFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "所有文件|*.*",
                Multiselect = false,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFile = openFileDialog.FileName;
            }
        }

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
                await foreach (var chunk in _semanticKernelService.GetAIResponse3(AskText,SelectedFile))
                {
                    ResponseText += chunk;
                }
                ProgressRingVisible = Visibility.Hidden;
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

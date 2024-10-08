using SimpleRAG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Diagnostics;

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

        [ObservableProperty]
        private bool textChecked = true;

        [ObservableProperty]
        private bool fileChecked = false;
        [ObservableProperty]
        private bool pictureChecked = false;

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
                if (TextChecked == true)
                {
                    await foreach (var chunk in _semanticKernelService.GetAIResponse2(AskText))
                    {
                        ResponseText += chunk;
                    }
                }
                else if (FileChecked == true)
                {
                    await foreach (var chunk in _semanticKernelService.GetAIResponse3(AskText, SelectedFile))
                    {
                        ResponseText += chunk;
                    }
                }
                else if (PictureChecked == true)
                {
                    await foreach (var chunk in _semanticKernelService.GetAIResponse4(AskText, SelectedFile))
                    {
                        ResponseText += chunk;
                    }
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

        [RelayCommand]
        private void RunPythonScript()
        {
            string pythonScriptPath = @"D:\学习路线\人工智能\图片文字识别\test.py"; // 替换为你的Python脚本路径
            string pythonExecutablePath = @"D:\SoftWare\Anaconda\envs\paddle_env\python.exe"; // 替换为你的Python解释器路径                                                                                         
            string arguments = "D:\\桌面\\test2.png"; // 替换为你要传递的参数

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonExecutablePath;
            start.Arguments =$"{pythonScriptPath} {arguments}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    MessageBox.Show(result);
                }

                using (System.IO.StreamReader errorReader = process.StandardError)
                {
                    string errors = errorReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(errors))
                    {
                        MessageBox.Show("Errors: " + errors);
                    }
                }
            }      
        }
    }
}

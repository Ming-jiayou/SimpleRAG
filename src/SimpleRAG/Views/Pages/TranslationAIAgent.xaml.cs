using SimpleRAG.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace SimpleRAG.Views.Pages
{
    /// <summary>
    /// TranslationAIAgent.xaml 的交互逻辑
    /// </summary>
    public partial class TranslationAIAgent : Page, INavigableView<TranslationAIAgentViewModel>
    {
        public TranslationAIAgentViewModel ViewModel { get; }
        public TranslationAIAgent(TranslationAIAgentViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = this;
            InitializeComponent();
        }
    }
}

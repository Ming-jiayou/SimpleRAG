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
    /// FunctionCalling.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionCalling : Page, INavigableView<FunctionCallingViewModel>
    {
        public FunctionCallingViewModel ViewModel { get; }
        public FunctionCalling(FunctionCallingViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = this;
            InitializeComponent();
        }
    }
}

using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace SimpleRAG.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - SimpleRAG";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "AI Chat",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Chat24 },
                TargetPageType = typeof(Views.Pages.AIChat)
            },
             new NavigationViewItem()
            {
                Content = "AI Embedding",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.AIEmbedding)
            },
               new NavigationViewItem()
            {
                Content = "Function Calling",
                Icon = new SymbolIcon { Symbol = SymbolRegular.WindowDevTools24 },
                TargetPageType = typeof(Views.Pages.FunctionCalling)
            },
                 new NavigationViewItem()
            {
                Content = "Translation AI Agent",
                Icon = new SymbolIcon { Symbol = SymbolRegular.TextProofingTools24 },
                TargetPageType = typeof(Views.Pages.TranslationAIAgent)
            },
                     new NavigationViewItem()
            {
                Content = "AI File Chat",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Folder24 },
                TargetPageType = typeof(Views.Pages.AIFileChat)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}

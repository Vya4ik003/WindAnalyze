using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для AnalyzeCenterPage.xaml
    /// </summary>
    public partial class AnalyzeCenterPage : Page
    {
        private MainWindowViewModel _mainViewModel;

        internal AnalyzeCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel;
        }
    }
}

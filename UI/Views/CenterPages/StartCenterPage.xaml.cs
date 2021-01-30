using System.Windows.Controls;
using UI.ViewModels.PagesViewModel.CenterPagesViewModels;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartCenterPage : Page
    {
        internal StartCenterPage()
        {
            InitializeComponent();
            DataContext = new StartCenterPageViewModel();
        }
    }
}

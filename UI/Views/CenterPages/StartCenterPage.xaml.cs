using Buisness;
using IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using UI.ViewModels;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartCenterPage : Page
    {
        internal StartCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new StartCenterViewModel(mainViewModel);
        }
    }
}

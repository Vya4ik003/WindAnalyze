using System.Windows;
using System.Windows.Controls;
using UI.ViewModels;
using Buisness;
using IO.Others;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartCenterPage : Page
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        internal StartCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();

            _mainWindowViewModel = mainViewModel;
        }

        //TODO: Сделать асинхронное чтение
        //TODO: Сделать окно с сообщением о загрузке
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Interaction interaction = new Interaction();

            IOResult result = interaction.OpenFile();

            BLResult blResult = interaction.GetStatistic(result);
        }
    }
}

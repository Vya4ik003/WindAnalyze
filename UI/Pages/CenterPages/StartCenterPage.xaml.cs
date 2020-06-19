using IO;
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
        private MainWindowViewModel _mainWindowViewModel;
        internal StartCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();

            _mainWindowViewModel = mainViewModel;
        }

        //TODO: Сделать асинхронное чтение
        //TODO: Сделать окно с сообщением о загрузке
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            FileWorker fileWorker = new FileWorker();

            IOResult result = fileWorker.OpenFile();

            if (result.Message != "Null")
                _mainWindowViewModel.CurrentCenterPage = new AnalyzeCenterPage(_mainWindowViewModel, result);
        }
    }
}

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
        private readonly MainWindowViewModel _mainWindowViewModel;
        internal StartCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();

            _mainWindowViewModel = mainViewModel;
        }

        //TODO: Сделать асинхронное чтение
        //TODO: Сделать окно с сообщением о загрузке
        /// <summary>
        /// Событие клика на начальную кнопку 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            ReadFileResult readFileResult = OpenAndReadFile();

            if (!readFileResult.IsSuccess)
            {
                MessageBox.Show(readFileResult.ErrorMessage);
                return;
            }

            CreateStatisticResult statisticResult = CreateStatisticResult.GetStatistic(
                readFileResult.MeasureDates,
                readFileResult.MeasureWindTypes,
                readFileResult.FirstInformationRow, readFileResult.SecondInformationRow, readFileResult.ThirdInformationRow);
        }

        /// <summary>
        /// Подготовка к чтению файла и получению результата
        /// </summary>
        /// <returns>Результат чтения</returns>
        private ReadFileResult OpenAndReadFile()
        {
            JObject appConfig = JObject.Parse(File.ReadAllText("AppConfig.json"));
            string dialogFilter = appConfig["OpenFileDialogFilter"].ToString();
            OpenFileDialog openDialog = new OpenFileDialog() { Filter = dialogFilter };

            if (openDialog.ShowDialog() == true)
            {
                return ReadFileResult.GetResult(openDialog.FileName);
            }

            return ReadFileResult.EmptyResult;
        }
    }
}

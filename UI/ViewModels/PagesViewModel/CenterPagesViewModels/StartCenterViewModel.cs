using Buisness;
using Buisness.Winds;
using IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Pages.CenterPages;
using UI.Properties;

namespace UI.ViewModels
{
    class StartCenterViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ICommand OpenFile_Command
        {
            get
            {
                return new RelayCommand(() => OpenFile());
            }
        }

        public StartCenterViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        //TODO: Сделать асинхронное чтение
        //TODO: Сделать окно с сообщением о загрузке
        /// <summary>
        /// Событие клика на начальную кнопку 
        /// </summary>
        private void OpenFile()
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

            //TODO: Проверить на нормальность использования string заместо WindTypes
            IList<string> windTypes = statisticResult.FileInformation.WindTypes.Select(_ => _.ToString()).ToList();
            IList<double> windChanges = WindChange.GetWindChangesValues(statisticResult.FileInformation.WindChanges);

            _mainWindowViewModel.AnalyzeCenterPage = new AnalyzeCenterPage(windTypes, windChanges);
            _mainWindowViewModel.CurrentCenterPage = _mainWindowViewModel.AnalyzeCenterPage;
        }

        /// <summary>
        /// Подготовка к чтению файла и получению результата
        /// </summary>
        /// <returns>Результат чтения</returns>
        private ReadFileResult OpenAndReadFile()
        {
            string dialogFilter = Resources.OpenFileDialogFilter;
            OpenFileDialog openDialog = new OpenFileDialog() { Filter = dialogFilter };

            if (openDialog.ShowDialog() == true)
            {
                return ReadFileResult.GetResult(openDialog.FileName);
            }

            return ReadFileResult.EmptyResult;
        }
    }
}

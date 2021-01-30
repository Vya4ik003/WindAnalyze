using Buisness;
using Buisness.Information;
using Buisness.Winds;
using IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Pages.CenterPages;
using UI.Properties;

namespace UI.ViewModels.PagesViewModel
{
    class RightPageViewModel : BaseViewModel
    {

        public ICommand OpenFileCommand
        {
            get
            {
                return new RelayCommand((_) => OpenFile());
            }
        }


        private void OpenFile()
        {
            ReadFileResult readResult = GetReadResult();

            if (!readResult.IsSuccess)
            {
                MessageBox.Show(readResult.ErrorMessage);
                return;
            }

            CreateStatisticResult statisticResult = CreateStatisticResult.GetStatistic(
                readResult.MeasureDates, readResult.MeasureWindTypes, readResult.FirstInformationRow,
                readResult.SecondInformationRow, readResult.ThirdInformationRow);

            IList<string> windTypes = statisticResult.FileInformation.WindTypes.Select(_ => _.ToString()).ToList();
            IList<double> windChanges = WindChange.GetWindChangesValues(statisticResult.FileInformation.WindChanges);

            MainWindowViewModel mainViewModel = MainWindowViewModel.GetInstance();

            mainViewModel.AnalyzeCenterPage.SetWindHeaders(windTypes);
            mainViewModel.AnalyzeCenterPage.SetWindValues(windChanges);
            mainViewModel.PeriodsLeftPage.SetInformations(fileInformation: statisticResult.FileInformation, periodInformation: statisticResult.PeriodInformations);
        }

        private ReadFileResult GetReadResult()
        {
            string dialogFilter = Resources.OpenFileDialogFilter;
            OpenFileDialog openDialog = new OpenFileDialog() { Filter = dialogFilter };

            if (openDialog.ShowDialog() == true)
            {
                return ReadFileResult.GetResult(openDialog.FileName);
            }

            return ReadFileResult.EmptyResult;
        }

        public RightPageViewModel(IList<InformationLabel> fileInfoLabels)
        {
            FileInformationLabels = fileInfoLabels;
        }

        public ICommand HideOpenMenuCommand
        {
            get
            {
                return new RelayCommand((_) =>
                {
                    if (_isOpenPage)
                    {
                        PageWidth = RolledRightPageWidth;
                        TitleAngle = VerticalTitleAngle;
                        TitleRow = VerticalTitleRow;
                        InformationLabelsVisibility = CollapsedVisibility;
                    }
                    else
                    {
                        PageWidth = OpenRightPageWidth;
                        TitleAngle = HorizontalTitleAngle;
                        TitleRow = HorizontalTitleRow;
                        InformationLabelsVisibility = VisibleVisibility;
                    }

                    _isOpenPage = !_isOpenPage;
                });
            }
        }

        private double RolledRightPageWidth { get; } = App.TitlesRowColumnSize;
        private static double OpenRightPageWidth { get; } = 250;
        private bool _isOpenPage = true;

        private double _pageWidth = OpenRightPageWidth;
        public double PageWidth
        {
            get
            {
                return _pageWidth;
            }
            private set
            {
                _pageWidth = value;
                OnPropertyChanged();
            }
        }

        private double HorizontalTitleAngle { get; } = 0;
        private double VerticalTitleAngle { get; } = 90;

        private int HorizontalTitleRow { get; } = 0;
        private int VerticalTitleRow { get; } = 1;

        private double _titleAngle;
        public double TitleAngle
        {
            get
            {
                return _titleAngle;
            }
            set
            {
                _titleAngle = value;
                OnPropertyChanged();
            }
        }

        private int _titleRow;
        public int TitleRow
        {
            get
            {
                return _titleRow;
            }
            set
            {
                _titleRow = value;
                OnPropertyChanged();
            }
        }


        private IList<InformationLabel> _fileInformationLabels;
        public IList<InformationLabel> FileInformationLabels
        {
            get
            {
                return _fileInformationLabels;
            }
            set
            {
                _fileInformationLabels = value;
                OnPropertyChanged();
            }
        }

        private IList<InformationLabel> _periodInformationLabels;
        public IList<InformationLabel> PeriodInformationLabels
        {
            get
            {
                return _periodInformationLabels;
            }
            set
            {
                _periodInformationLabels = value;

                if (_periodInformationLabels == null)
                {
                    NoPeriodLabelVisibility = VisibleVisibility;
                }
                else
                {
                    NoPeriodLabelVisibility = CollapsedVisibility;
                }

                OnPropertyChanged();
            }
        }

        private static Visibility CollapsedVisibility { get; } = Visibility.Collapsed;
        private static Visibility VisibleVisibility { get; } = Visibility.Visible;

        private Visibility _informationLabelsVisibility = VisibleVisibility;
        public Visibility InformationLabelsVisibility
        {
            get
            {
                return _informationLabelsVisibility;
            }
            set
            {
                _informationLabelsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _noPeriodLabelVisibility = VisibleVisibility;
        public Visibility NoPeriodLabelVisibility
        {
            get
            {
                return _noPeriodLabelVisibility;
            }
            set
            {
                _noPeriodLabelVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}

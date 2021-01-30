using Buisness.Information;
using Buisness.Winds;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Pages.CenterPages;
using UI.Views;

namespace UI.ViewModels.PagesViewModel
{
    public class PeriodsLeftPageViewModel : BaseViewModel
    {
        public ICommand GoToNextPeriod
        {
            get
            {
                return new RelayCommand((_) =>
                {
                    _indexOfPeriod++;
                    string content = _checkedPeriods[_indexOfPeriod].Content.ToString();
                    string year = content.Substring(0, content.IndexOf(" "));

                    ToggleValuesAndInfo(year);
                },
                (_) => _checkedPeriods.Count() > 1 && _indexOfPeriod < _checkedPeriods.Count - 1);
            }
        }

        public ICommand GoToPreviousPeriod
        {
            get
            {
                return new RelayCommand((_) =>
                {
                    _indexOfPeriod--;
                    string content = _checkedPeriods[_indexOfPeriod].Content.ToString();
                    string year = content.Substring(0, content.IndexOf(" "));

                    ToggleValuesAndInfo(year);
                },
                (_) => _checkedPeriods.Count() > 1 && _indexOfPeriod > 0);
            }
        }

        public ICommand GoToCurrentPeriod
        {
            get
            {
                return new RelayCommand((period) =>
                {
                    if (!(period is CheckBox periodLabel)) return;

                    CheckBox currentPeriod = periodLabel;
                    bool containsPeriod = _checkedPeriods.Contains(currentPeriod);
                    currentPeriod.IsChecked = !containsPeriod;

                    if (containsPeriod)
                    {
                        _checkedPeriods.Remove(periodLabel);
                        currentPeriod = _checkedPeriods.LastOrDefault();
                    }
                    else
                    {
                        _checkedPeriods.Add(periodLabel);
                    }
                    _checkedPeriods = _checkedPeriods.OrderBy(_ => _.ToString()).ToList();
                    _indexOfPeriod = _checkedPeriods.IndexOf(currentPeriod);

                    var content = currentPeriod?.Content.ToString();
                    var year = content?.Substring(0, content.IndexOf(" "));
                    PeriodInformation currentPeriodInfo = _periodInformations.FirstOrDefault(_ => _.Year.ToString().Equals(year));
                    IList<WindChange> windChanges;

                    if (currentPeriodInfo == null)
                        windChanges = _fileInformation.WindChanges;
                    else
                        windChanges = currentPeriodInfo.PeriodWindChanges;

                    IList<double> currentValues = WindChange.GetWindChangesValues(windChanges);

                    MainWindowViewModel viewModel = MainWindowViewModel.GetInstance();

                    viewModel.AnalyzeCenterPage.SetWindValues(currentValues);
                    viewModel.MenuRightPage.ChangeInformationLabel(currentPeriodInfo?.PeriodInformationLabels);
                });
            }
        }

        public PeriodsLeftPageViewModel(FileInformation fileInformation, IList<PeriodInformation> periodInformation)
        {
            _fileInformation = fileInformation;
            _periodInformations = periodInformation;
            SetPeriods();
        }

        public void SetInformations(FileInformation fileInformation, IList<PeriodInformation> periodInformation)
        {
            _fileInformation = fileInformation;
            _periodInformations = periodInformation;
            SetPeriods();
        }

        private FileInformation _fileInformation;
        internal FileInformation FileInformation
        {
            set
            {
                _fileInformation = value;
            }
        }

        private IList<PeriodInformation> _periodInformations;
        internal IList<PeriodInformation> PeriodInformations
        {
            set
            {
                _periodInformations = value;
            }
        }

        private IList<string> _periods;
        public IList<string> Periods
        {
            get
            {
                return _periods;
            }
            private set
            {
                _periods = value;
                OnPropertyChanged();
            }
        }
        public void SetPeriods()
        {
            Periods = _periodInformations.Select(_ => _.Year.ToString() + " год").OrderBy(_ => _).ToList();
        }

        private IList<CheckBox> _checkedPeriods = new List<CheckBox>();
        private int _indexOfPeriod;

        private void ToggleValuesAndInfo(string year)
        {
            PeriodInformation currentPeriodInfo = _periodInformations.FirstOrDefault(_ => _.Year.ToString().Equals(year));
            IList<WindChange> windChanges = currentPeriodInfo.PeriodWindChanges;
            IList<double> currentValues = WindChange.GetWindChangesValues(windChanges);

            MainWindowViewModel mainViewModel = MainWindowViewModel.GetInstance();

            mainViewModel.AnalyzeCenterPage.SetWindValues(currentValues);
            mainViewModel.MenuRightPage.ChangeInformationLabel(currentPeriodInfo?.PeriodInformationLabels);
        }
    }
}

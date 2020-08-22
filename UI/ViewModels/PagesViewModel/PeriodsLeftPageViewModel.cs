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
    class PeriodsLeftPageViewModel : BaseViewModel
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
                    PeriodInformation currentPeriodInfo = _periodInformations.FirstOrDefault(_ => _.Year.ToString().Equals(year));
                    IList<WindChange> windChanges = currentPeriodInfo.PeriodWindChanges;
                    IList<double> currentValues = WindChange.GetWindChangesValues(windChanges);

                    AnalyzeCenterPage.SetWindValues(currentValues);
                    MenuRightPage.ChangeInformationLabel(currentPeriodInfo?.PeriodInformationLabels);
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
                    PeriodInformation currentPeriodInfo = _periodInformations.FirstOrDefault(_ => _.Year.ToString().Equals(year));
                    IList<WindChange> windChanges = currentPeriodInfo.PeriodWindChanges;
                    IList<double> currentValues = WindChange.GetWindChangesValues(windChanges);

                    AnalyzeCenterPage.SetWindValues(currentValues);
                    MenuRightPage.ChangeInformationLabel(currentPeriodInfo?.PeriodInformationLabels);
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


                    AnalyzeCenterPage.SetWindValues(currentValues);
                    MenuRightPage.ChangeInformationLabel(currentPeriodInfo?.PeriodInformationLabels);
                });
            }
        }

        public PeriodsLeftPageViewModel(FileInformation fileInformation, IList<PeriodInformation> periodInformation)
        {
            _fileInformation = fileInformation;
            _periodInformations = periodInformation;

            Periods = _periodInformations.Select(_ => _.Year.ToString() + " год").OrderBy(_ => _).ToList();
        }

        private FileInformation _fileInformation;
        private IList<PeriodInformation> _periodInformations;

        private IList<string> _periods;
        public IList<string> Periods
        {
            get
            {
                return _periods;
            }
            set
            {
                _periods = value;
                OnPropertyChanged();
            }
        }

        private IList<CheckBox> _checkedPeriods = new List<CheckBox>();
        private int _indexOfPeriod;

        //private double _hiddenPageWidth { get; } = 0;
        //private static double _showenPageWidth { get; } = 250;

        //private bool IsHiddenPage()
        //{
        //    return PageWidth == _showenPageWidth;
        //}

        //public void ChangePageWidth()
        //{
        //    if (IsHiddenPage())
        //    {
        //        PageWidth = _hiddenPageWidth;
        //    }
        //    else
        //    {
        //        PageWidth = _showenPageWidth;
        //    }
        //}

        //private double _pageWidth = _showenPageWidth;
        //public double PageWidth
        //{
        //    get
        //    {
        //        return _pageWidth;
        //    }
        //    set
        //    {
        //        _pageWidth = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}

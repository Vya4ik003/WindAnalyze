using System.Windows.Controls;
using UI.Pages.CenterPages;
using UI.Views;

namespace UI.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private static MainWindowViewModel _mainWindowViewModel;

        internal MainWindowViewModel()
        {
            _mainWindowViewModel = this;
            CurrentCenterPage = new StartCenterPage();
        }

        private Page _currentCenterPage;
        public Page CurrentCenterPage
        {
            get
            {
                return _currentCenterPage;
            }
            set
            {
                _currentCenterPage = value;
                OnPropertyChanged();
            }
        }

        private Page _currentLeftPage;
        public Page CurrentLeftPage
        {
            get
            {
                return _currentLeftPage;
            }
            set
            {
                _currentLeftPage = value;
                OnPropertyChanged();
            }
        }

        public Page _currentRightPage;
        public Page CurrentRightPage
        {
            get
            {
                return _currentRightPage;
            }
            set
            {
                _currentRightPage = value;
                OnPropertyChanged();
            }
        }

        public AnalyzeCenterPage AnalyzeCenterPage { get; set; }
        public MenuRightPage MenuRightPage { get; set; }
        public PeriodsLeftPage PeriodsLeftPage { get; set; }

        internal static MainWindowViewModel GetInstance() => _mainWindowViewModel;
    }
}

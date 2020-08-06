using System.Windows.Controls;
using UI.Pages.CenterPages;
using UI.Views;

namespace UI.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        internal MainWindowViewModel()
        {
            CurrentCenterPage = new StartCenterPage(this);
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

        public Page AnalyzeCenterPage { get; set; }
        public MenuRightPage MenuRightPage { get; set; }
        public Page PeriodsLeftPage { get; set; }
    }
}

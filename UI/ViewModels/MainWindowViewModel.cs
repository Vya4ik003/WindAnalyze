using System.Windows.Controls;
using UI.Pages.CenterPages;

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

        public Page AnalyzeCenterPage { get; set; }
    }
}

using System.Windows;
using System.Windows.Controls;
using UI.Pages.CenterPages;

namespace UI.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        internal AnalyzeCenterPage AnalyzePage;

        internal MainWindowViewModel()
        {
            AnalyzePage = new AnalyzeCenterPage(this);

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
    }
}

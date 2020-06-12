using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UI.Pages;

namespace UI.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            CurrentPage = new StartPage();
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyCHanged();
            }
        }
    }
}

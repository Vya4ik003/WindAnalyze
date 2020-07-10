using System;
using System.Collections.Generic;
using System.Text;

namespace UI.ViewModels.PagesViewModel.CenterPagesViewModels
{
    class AnalyzeCenterPageViewModel : BaseViewModel
    {
        public AnalyzeCenterPageViewModel(IList<string> windHeaders, IList<double> windValues)
        {
            WindHeaders = windHeaders;
            WindValues = windValues;
        }

        private IList<string> _windHeaders;
        public IList<string> WindHeaders
        {
            get
            {
                return _windHeaders;
            }
            set
            {
                _windHeaders = value;
                OnPropertyChanged();
            }
        }

        private IList<double> _windValue;
        public IList<double> WindValues
        {
            get
            {
                return _windValue;
            }
            set
            {
                _windValue = value;
                OnPropertyChanged();
            }
        }
    }
}

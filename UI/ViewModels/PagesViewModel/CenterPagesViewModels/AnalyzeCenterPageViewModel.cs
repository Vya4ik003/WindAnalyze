using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UI.ViewModels.PagesViewModel.CenterPagesViewModels
{
    class AnalyzeCenterPageViewModel : BaseViewModel
    {
        public ICommand ShowHideTitles
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (TitlesSize == _showenTitlesSize)
                    {
                        TitlesSize = _hiddenTitlesSize;
                    }
                    else
                    {
                        TitlesSize = _showenTitlesSize;
                    }
                });
            }
        }

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

        private static readonly double _showenTitlesSize = App.TitlesRowColumnSize;
        private readonly double _hiddenTitlesSize = 0;

        private double _titlesSize = _showenTitlesSize;
        public double TitlesSize
        {
            get
            {
                return _titlesSize;
            }
            set
            {
                _titlesSize = value;
                OnPropertyChanged();
            }
        }
    }
}

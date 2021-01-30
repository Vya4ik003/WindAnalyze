using Buisness;
using Buisness.Winds;
using IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UI.Pages.CenterPages;
using UI.Properties;
using UI.Views;

namespace UI.ViewModels.PagesViewModel.CenterPagesViewModels
{
    class AnalyzeCenterPageViewModel : BaseViewModel
    {
        public ICommand ShowHideTitles
        {
            get
            {
                return new RelayCommand((_) =>
                {
                    Button b = (Button)_;
                    RotateTransform tr = b.RenderTransform as RotateTransform;
                    if (TitlesSize == _showenTitlesSize)
                    {
                        TitlesSize = _hiddenTitlesSize;
                        tr.Angle = 180;
                    }
                    else
                    {
                        TitlesSize = _showenTitlesSize;
                        tr.Angle = 0;
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

using Buisness.Information;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace UI.ViewModels.PagesViewModel
{
    class RightPageViewModel : BaseViewModel
    {
        public RightPageViewModel(IList<InformationLabel> fileInfoLabels)
        {
            FileInformationLabels = fileInfoLabels;
        }

        public ICommand HideOpenMenuCommand
        {
            get
            {
                return new RelayCommand((_) =>
                {
                    if (_isOpenPage)
                    {
                        PageWidth = RolledRightPageWidth;
                        TitleAngle = VerticalTitleAngle;
                        TitleRow = VerticalTitleRow;
                        InformationLabelsVisibility = HiddenInformationLabelsVisibility;
                    }
                    else
                    {
                        PageWidth = OpenRightPageWidth;
                        TitleAngle = HorizontalTitleAngle;
                        TitleRow = HorizontalTitleRow;
                        InformationLabelsVisibility = ShowenInformationLabelsVisibility;
                    }

                    _isOpenPage = !_isOpenPage;
                });
            }
        }

        private double RolledRightPageWidth { get; } = App.TitlesRowColumnSize;
        private static double OpenRightPageWidth { get; } = 250;
        private bool _isOpenPage = true;

        private double _pageWidth = OpenRightPageWidth;
        public double PageWidth
        {
            get
            {
                return _pageWidth;
            }
            private set
            {
                _pageWidth = value;
                OnPropertyChanged();
            }
        }

        private double HorizontalTitleAngle { get; } = 0;
        private double VerticalTitleAngle { get; } = 90;

        private int HorizontalTitleRow { get; } = 0;
        private int VerticalTitleRow { get; } = 1;

        private double _titleAngle;
        public double TitleAngle
        {
            get
            {
                return _titleAngle;
            }
            set
            {
                _titleAngle = value;
                OnPropertyChanged();
            }
        }

        private int _titleRow;
        public int TitleRow
        {
            get
            {
                return _titleRow;
            }
            set
            {
                _titleRow = value;
                OnPropertyChanged();
            }
        }

        private IList<InformationLabel> _fileInformationLabels;
        public IList<InformationLabel> FileInformationLabels
        {
            get
            {
                return _fileInformationLabels;
            }
            set
            {
                _fileInformationLabels = value;
                OnPropertyChanged();
            }
        }

        private IList<InformationLabel> _periodInformationLabels;
        public IList<InformationLabel> PeriodInformationLabels
        {
            get
            {
                return _periodInformationLabels;
            }
            set
            {
                _periodInformationLabels = value;
                OnPropertyChanged();
            }
        }

        private Visibility HiddenInformationLabelsVisibility { get; } = Visibility.Collapsed;
        private static Visibility ShowenInformationLabelsVisibility { get; } = Visibility.Visible;

        private Visibility _informationLabelsVisibility = ShowenInformationLabelsVisibility;
        public Visibility InformationLabelsVisibility
        {
            get
            {
                return _informationLabelsVisibility;
            }
            set
            {
                _informationLabelsVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}

﻿using Buisness.Information;
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
                return new RelayCommand(() =>
                {
                    if (_isOpenPage)
                    {
                        PageWidth = _rolledRightPageWidth;
                        TitleAngle = _verticalTitleAngle;
                        TitleRow = _verticalTitleRow;
                        InformationLabelsVisibility = _hiddenInformationLabelsVisibility;
                    }
                    else
                    {
                        PageWidth = _openRightPageWidth;
                        TitleAngle = _horizontalTitleAngle;
                        TitleRow = _horizontalTitleRow;
                        InformationLabelsVisibility = _showenInformationLabelsVisibility;
                    }

                    _isOpenPage = !_isOpenPage;
                });
            }
        }

        private double _rolledRightPageWidth { get; } = App.TitlesRowColumnSize;
        private static double _openRightPageWidth { get; } = 250;
        private bool _isOpenPage = true;

        private double _pageWidth = _openRightPageWidth;
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

        private double _horizontalTitleAngle { get; } = 0;
        private double _verticalTitleAngle { get; } = 90;

        private int _horizontalTitleRow { get; } = 0;
        private int _verticalTitleRow { get; } = 1;

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

        private Visibility _hiddenInformationLabelsVisibility { get; } = Visibility.Collapsed;
        private static Visibility _showenInformationLabelsVisibility { get; } = Visibility.Visible;

        private Visibility _informationLabelsVisibility = _showenInformationLabelsVisibility;
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

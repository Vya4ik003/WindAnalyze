using Buisness.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModels.PagesViewModel;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для PeriodsRightPage.xaml
    /// </summary>
    public partial class PeriodsLeftPage : Page
    {
        private static PeriodsLeftPageViewModel _periodsPageViewModel;

        public PeriodsLeftPage(FileInformation fileInformation, IList<PeriodInformation> periodInformation)
        {
            InitializeComponent();

            _periodsPageViewModel = new PeriodsLeftPageViewModel(fileInformation, periodInformation);
            DataContext = _periodsPageViewModel;
        }

        public static void SetPeriods(IList<string> periods)
        {
            _periodsPageViewModel.Periods = periods;
        }

        //public static void CHangePageWidth()
        //{
        //    _periodsPageViewModel.ChangePageWidth();
        //}
    }
}

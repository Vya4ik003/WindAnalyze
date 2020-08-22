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
    /// Логика взаимодействия для MenuRightPage.xaml
    /// </summary>
    public partial class MenuRightPage : Page
    {
        private static RightPageViewModel _viewModel;
        public MenuRightPage(IList<InformationLabel> labels)
        {
            InitializeComponent();
            _viewModel = new RightPageViewModel(labels);
            DataContext = _viewModel;
        }

        public static void ChangeInformationLabel(IList<InformationLabel> periodInformation)
        {
            _viewModel.PeriodInformationLabels = periodInformation;
        }

        public static bool CheckCurrentYear(string year)
        {
            return _viewModel.PeriodInformationLabels?.Where(_ => _.Value == year).Count() > 0;
        }

        public static int GetCurrentYear()
        {
            return int.Parse(_viewModel.PeriodInformationLabels.First(_ => _.Label == "год").Value);
        }
    }
}

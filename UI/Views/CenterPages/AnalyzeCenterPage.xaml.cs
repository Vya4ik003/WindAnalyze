using System.Collections.Generic;
using System.Windows.Controls;
using UI.ViewModels;
using UI.ViewModels.PagesViewModel.CenterPagesViewModels;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для AnalyzeCenterPage.xaml
    /// </summary>
    public partial class AnalyzeCenterPage : Page
    {
        private static AnalyzeCenterPageViewModel _analyzeCenterPageViewModel;
        internal AnalyzeCenterPage(IList<string> headers, IList<double> values)
        {
            InitializeComponent();
            _analyzeCenterPageViewModel = new AnalyzeCenterPageViewModel(headers, values);
            DataContext = _analyzeCenterPageViewModel;
        }

        public static void SetWindValues(IList<double> values)
        {
            _analyzeCenterPageViewModel.WindValues = values;
        }
    }
}

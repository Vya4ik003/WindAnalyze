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
        internal AnalyzeCenterPage(IList<string> headers, IList<double> values)
        {
            InitializeComponent();
            DataContext = new AnalyzeCenterPageViewModel(headers, values);
        }
    }
}

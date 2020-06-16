using System.Text;
using System.Windows;
using UI.ViewModels;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //TODO: При возможности изменить на "DataContext = new MainWindowViewModel()"
            _mainWindowViewModel = new MainWindowViewModel();

            DataContext = _mainWindowViewModel;
        }
    }
}

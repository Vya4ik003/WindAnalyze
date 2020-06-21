using IO.Others;
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
using UI.ViewModels;

namespace UI.Pages.CenterPages
{
    /// <summary>
    /// Логика взаимодействия для AnalyzeCenterPage.xaml
    /// </summary>
    public partial class AnalyzeCenterPage : Page
    {
        private MainWindowViewModel _mainViewModel;
        private IOResult _readResult;

        internal AnalyzeCenterPage(MainWindowViewModel mainViewModel, IOResult result)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel;
            _readResult = result;
        }
    }
}

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
        internal AnalyzeCenterPage(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();

            CreateHeaders(10);
        }

        private void CreateHeaders(int count)
        {
            //TestDataGrid.RowHeaderWidth = 100;
            int[] texts = new int[count];

            for(int i = 0; i < count; i++)
            {
                texts[i] = i;

                TestDataGrid.Columns.Add(new DataGridTextColumn() { Header = i});

                TestDataGrid.RowHeaderStyle = new Style();
            }

            TestDataGrid.ItemsSource = texts;
        }
    }
}

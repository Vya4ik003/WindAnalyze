using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static double DefaultGridCellsThickness { get; } = 2;
        public static double NarrowGridCellsThickness { get; } = 0;
        public static double TitlesRowColumnSize { get; } = 70;
    }
}

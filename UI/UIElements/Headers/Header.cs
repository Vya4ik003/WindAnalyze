using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.UIElements.Headers
{
    /// <summary>
    /// Элемент заголовка с направлением ветра
    /// </summary>
    abstract class Header : Label
    {
        private static readonly double defaultThickness = App.DefaultGridCellsThickness;
        private static readonly double emptyThickness = App.NarrowGridCellsThickness;

        protected static int _rowColumnIndex = 1;
        protected readonly Thickness defaultRowThickness = new Thickness(defaultThickness, emptyThickness, defaultThickness, defaultThickness);
        protected readonly Thickness defaultColumnThickness = new Thickness(emptyThickness, defaultThickness, defaultThickness, defaultThickness);
        protected readonly Thickness firstRowColumnThickness = new Thickness(defaultThickness);

        protected Header(string headerText)
        {
            Content = headerText;

            SetContentAlignment();
        }

        protected void CreateBorder(Thickness thickness)
        {
            BorderBrush = Brushes.Black;
            BorderThickness = thickness;
        }

        private void SetContentAlignment()
        {
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        public static void IncrementCounter()
        {
            _rowColumnIndex++;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UI.UIElements.Headers;

namespace UI.UIElements.ValueCells
{
    class ValueCell : Label
    {
        public static int _rowIndex = 1;
        public static int _columnIndex = 1;

        private static readonly double defaultThickness = App.DefaultGridCellsThickness;
        private static readonly double emptyThickness = App.NarrowGridCellsThickness;

        public ValueCell(double value)
        {
            Content = Math.Round(value, 4);
            CreateContentAlignment();

            Grid.SetColumn(this, _columnIndex);
            Grid.SetRow(this, _rowIndex);

            _columnIndex++;
        }

        public static void IncrementRowIndex()
        {
            _rowIndex++;
        }

        public static void ResetColumnIndex()
        {
            _columnIndex = 1;
        }

        public static void ResetAllIndexes()
        {
            _rowIndex = 1;
            ResetColumnIndex();
        }

        public virtual void CreateBorder()
        {
            BorderBrush = Brushes.Black;
            BorderThickness = new Thickness(emptyThickness, emptyThickness, defaultThickness, defaultThickness);
        }

        private void CreateContentAlignment()
        {
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
        }
    }
}

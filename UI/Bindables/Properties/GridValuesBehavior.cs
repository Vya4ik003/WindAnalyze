using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UI.UIElements.ValueCells;

namespace UI.Bindables.Properties
{
    class GridValuesBehavior
    {
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.RegisterAttached(
                "Values",
                typeof(IList<double>),
                typeof(GridValuesBehavior),
                new UIPropertyMetadata(null, OnValuesPropertyChanged));

        private static void OnValuesPropertyChanged(DependencyObject sourceObject, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = sourceObject as Grid;

            IList<double> newValues = e.NewValue as IList<double>;
            double count = Math.Sqrt(newValues.Count);

            int valueIndex = 0;
            for (int i = 0; i < count; i++)
            {
                for (int t = 0; t < count; t++)
                {
                    ValueCell valueCell = new ValueCell(newValues[valueIndex]);
                    valueCell.CreateBorder();

                    grid.Children.Add(valueCell);

                    valueIndex++;
                }

                ValueCell.ResetColumnIndex();
                ValueCell.IncrementRowIndex();
            }
            ValueCell.ResetAllIndexes();
        }

        public static void SetValues(DependencyObject sourceObject, IList<double> value)
        {
            sourceObject.SetValue(ValuesProperty, value);
        }

        public static IList<double> GetValues(DependencyObject sourceObject)
        {
            return (IList<double>)sourceObject.GetValue(ValuesProperty);
        }
    }
}

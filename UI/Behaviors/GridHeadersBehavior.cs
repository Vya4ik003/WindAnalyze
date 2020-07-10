using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UI.UIElements.Headers;

namespace UI.Behaviors
{
    class GridHeadersBehavior
    {
        private static Grid _sourceGrid;

        public static readonly DependencyProperty HeadersProperty =
            DependencyProperty.RegisterAttached(
                "Headers",
                typeof(IList<string>),
                typeof(GridHeadersBehavior),
                new UIPropertyMetadata(null, OnHeadersChanged)
                );

        private static void OnHeadersChanged(DependencyObject sourceObject, DependencyPropertyChangedEventArgs e)
        {
            _sourceGrid = sourceObject as Grid;

            IList<string> newHeaders = e.NewValue as IList<string>;
            CreateDefinition(newHeaders.Count() + 1);
            FillHeaders(newHeaders);

            var hiddenButton = new Button();
            Grid.SetColumn(hiddenButton, 0);
            Grid.SetRow(hiddenButton, 0);
            _sourceGrid.Children.Add(hiddenButton);
        }

        public static void SetHeaders(DependencyObject sourceObject, IList<string> value)
        {
            sourceObject.SetValue(HeadersProperty, value);
        }

        public static IList<string> GetHeaders(DependencyObject sourceObject)
        {
            return (IList<string>)sourceObject.GetValue(HeadersProperty);
        }

        private static void CreateDefinition(int sizeOfGrid)
        {
            bool isEmpty = _sourceGrid.Children.Count == 0;
            if (isEmpty)
            {
                for (int i = 0; i < sizeOfGrid; i++)
                {
                    _sourceGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    _sourceGrid.RowDefinitions.Add(new RowDefinition());
                }
            }
        }

        private static void FillHeaders(IList<string> headersText)
        {
            foreach (string headerText in headersText)
            {
                bool isFirst = headerText == "С";

                Header columnHeader = new ColumnHeader(headerText, isFirst);
                Header rowHeader = new RowHeader(headerText, isFirst);
                Header.IncrementCounter();

                _sourceGrid.Children.Add(columnHeader);
                _sourceGrid.Children.Add(rowHeader);
            }
        }
    }
}

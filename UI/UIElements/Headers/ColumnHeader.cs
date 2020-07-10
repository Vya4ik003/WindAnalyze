using System.Windows.Controls;

namespace UI.UIElements.Headers
{
    /// <summary>
    /// Элемент для заголовок столбцов
    /// </summary>
    class ColumnHeader : Header
    {
        public ColumnHeader(string headerText, bool isFirst) : base(headerText)
        {
            Grid.SetColumn(this, _rowColumnIndex);

            if (isFirst)
                CreateBorder(firstRowColumnThickness);
            else
                CreateBorder(defaultColumnThickness);
        }
    }
}

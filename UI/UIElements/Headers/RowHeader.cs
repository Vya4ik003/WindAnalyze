using System.Windows.Controls;

namespace UI.UIElements.Headers
{
    /// <summary>
    /// Элемент для заголовок строк
    /// </summary>
    class RowHeader : Header
    {
        public RowHeader(string headerText, bool isFirst) : base(headerText)
        {
            Grid.SetRow(this, _rowColumnIndex);

            if (isFirst)
                CreateBorder(firstRowColumnThickness);
            else
                CreateBorder(defaultRowThickness);
        }
    }
}

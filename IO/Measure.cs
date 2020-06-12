using System;
using System.Collections.Generic;
using System.Text;

namespace IO
{
    struct Measure
    {
        /// <summary>
        /// Дата измерения
        /// </summary>
        public DateTime MeasureTime;

        /// <summary>
        /// Направление ветра
        /// </summary>
        public string DD;

        /// <summary>
        /// Инициализирует новый экземлпяр структуры Measure
        /// </summary>
        /// <param name="measureDate">Время измерения</param>
        /// <param name="ddColumn">Направление ветра</param>
        public Measure(string measureDate, string ddColumn)
        {
            MeasureTime = DateTime.Parse(measureDate);
            DD = ddColumn;
        }

    }
}

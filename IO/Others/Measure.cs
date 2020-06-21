using System;

namespace IO.Others
{
    public struct Measure
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
        public Measure(DateTime measureDate, string ddColumn)
        {
            MeasureTime = measureDate;
            DD = ddColumn;
        }

    }
}

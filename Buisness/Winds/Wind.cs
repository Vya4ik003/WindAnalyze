using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Winds
{
    public struct Wind
    {
        /// <summary>
        /// Дата измерения
        /// </summary>
        public DateTime MeasureTime;

        /// <summary>
        /// Тип ветра
        /// </summary>
        public WindType WindType;

        /// <summary>
        /// Инициализирует новый экземлпяр структуры Measure
        /// </summary>
        /// <param name="measureDate">Время измерения</param>
        /// <param name="typeOfWind">Тип ветра</param>
        public Wind(DateTime measureDate, WindType typeOfWind)
        {
            MeasureTime = measureDate;
            WindType = typeOfWind;
        }

        /// <summary>
        /// Инициализирует новый экземлпяр структуры Measure
        /// </summary>
        /// <param name="measureDate">Время измерения</param>
        /// <param name="typeOfWind">Направление ветра</param>
        public Wind(DateTime measureDate, string typeOfWind)
        {
            MeasureTime = measureDate;
            WindType = typeOfWind.ParseToWind();
        }
    }
}

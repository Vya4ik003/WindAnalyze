using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Information
{
    public abstract class BaseInformation
    {
        /// <summary>
        /// Дата первого измерения
        /// </summary>
        public DateTime FirstMeasure { get; set; }

        /// <summary>
        /// Дата последнего измерения
        /// </summary>
        public DateTime LastMeasure { get; set; }

        /// <summary>
        /// Кол-во измерений
        /// </summary>
        public int CountOfMeasures { get; set; }

        /// <summary>
        /// Смена ветров для статистики
        /// </summary>
        public Wind[] Winds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Information
{
    class PeriodInformation
    {
        public DateTime FirstMeasure;
        public DateTime LastMeasure;
        public int CountOfMeasures;
        public Measure[] PeriodMeasures;

        /// <summary>
        /// Инициализирует новый экземпляр класса PeriodInformation
        /// </summary>
        /// <param name="firstMeasure">Первое измерение за период</param>
        /// <param name="lastMeasure">Последнее измерение за период</param>
        /// <param name="measureCount"><Кол-во измерений за период/param>
        /// <param name="periodMeasures">Массив измерений за период</param>
        public PeriodInformation(string firstMeasure,
            string lastMeasure,
            string measureCount,
            Measure[] periodMeasures)
        {
            FirstMeasure = DateTime.Parse(firstMeasure);
            LastMeasure = DateTime.Parse(lastMeasure);
            CountOfMeasures = int.Parse(measureCount);
            PeriodMeasures = periodMeasures;
        }
    }
}

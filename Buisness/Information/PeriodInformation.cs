using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buisness.Information
{
    public class PeriodInformation
    {
        public IList<InformationLabel> PeriodInformationLabels { get; }

        private DateTime FirstWindDate { get; }
        private DateTime LastWindDate { get; }
        private int MeasureCount { get; }

        private IList<Wind> PeriodWinds { get; } = new List<Wind>();
        public IList<WindChange> PeriodWindChanges { get; private set; } = new List<WindChange>();

        public PeriodInformation(IList<Wind> winds)
        {
            FirstWindDate = winds.First().WindDate;
            LastWindDate = winds.Last().WindDate;
            MeasureCount = winds.Count();
            PeriodWinds = winds.ToList();
            PeriodWindChanges = GetWindChanges();

            PeriodInformationLabels = new List<InformationLabel>
            {
                new InformationLabel("Первое измерение", FirstWindDate),
                new InformationLabel("Последнее измерение", LastWindDate),
                new InformationLabel("Кол-во измерений", MeasureCount)
            };
        }

        /// <summary>
        /// Метод для получения статистики
        /// </summary>
        private IList<WindChange> GetWindChanges()
        {
            int count = PeriodWinds.Count() - 1;
            IList<WindChange> windChanges = new List<WindChange>(count);
            for (int i = 0; i < count; i++)
            {
                Wind currentWind = PeriodWinds.ElementAt(i);
                Wind nextWind = PeriodWinds.ElementAt(i + 1);
                WindChange windChange = new WindChange(currentWind, nextWind);

                windChanges.Add(windChange);
            }
            return windChanges.ToList();
        }
    }
}

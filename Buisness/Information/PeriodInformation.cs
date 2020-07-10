using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buisness.Information
{
    public class PeriodInformation
    {
        public DateTime FirstWindDate { get; }
        public DateTime LastWindDate { get; }
        public int MeasureCount { get; }

        private IList<Wind> PeriodWinds { get; } = new List<Wind>();
        public IList<WindChange> PeriodWindChanges { get; private set; } = new List<WindChange>();

        public PeriodInformation(IList<Wind> winds)
        {
            FirstWindDate = winds.First().WindDate;
            LastWindDate = winds.Last().WindDate;
            MeasureCount = winds.Count();
            PeriodWinds = winds.ToList();
            PeriodWindChanges = GetWindChanges();
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

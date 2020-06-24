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
        public int WindCount { get; }
        public IEnumerable<Wind> PeriodWinds { get; } = new List<Wind>();
        public IEnumerable<WindChange> PeriodWindChanges { get; private set; } = new List<WindChange>();

        public PeriodInformation(IEnumerable<Wind> winds)
        {
            FirstWindDate = winds.First().WindDate;
            LastWindDate = winds.Last().WindDate;
            WindCount = winds.Count();
            PeriodWinds = winds.ToList();
            PeriodWindChanges = GetWindChanges();
        }

        /// <summary>
        /// Метод для получения статистики
        /// </summary>
        public IEnumerable<WindChange> GetWindChanges()
        {
            int count = PeriodWinds.Count() - 1;
            IEnumerable<WindChange> windChanges = new List<WindChange>(count);
            for (int i = 0; i < count; i++)
            {
                Wind currentWind = PeriodWinds.ElementAt(i);
                Wind nextWind = PeriodWinds.ElementAt(i + 1);
                WindChange windChange = new WindChange(currentWind, nextWind);

                windChanges = windChanges.Concat(new[] { windChange });
            }
            return windChanges.ToList();
        }
    }
}

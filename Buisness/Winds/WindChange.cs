using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buisness.Winds
{
    public struct WindChange
    {
        /// <summary>
        /// Нынешний ветер
        /// </summary>
        public Wind CurrentWind { get; }

        /// <summary>
        /// Следующий ветер
        /// </summary>
        public Wind NextWind { get; }

        public WindChange(Wind currentWind, Wind nextWind)
        {
            CurrentWind = currentWind;
            NextWind = nextWind;
        }

        public override string ToString()
        {
            return $"{CurrentWind} - {NextWind}";
        }

        public static IList<double> GetWindChangesValues(IList<WindChange> windChanges)
        {
            IList<double> result = new List<double>();

            IList<WindType> windTypes = Wind.WindVariativeList;
            int windTypeCount = Wind.WindVariativeList.Count();

            for (int i = 0; i < windTypeCount; i++)
            {
                var currentWind = windTypes[i];
                for (int t = 0; t < windTypeCount; t++)
                {
                    var nextWind = windTypes[t];

                    var changeFromCurrentToNextWind = (double)windChanges.Where(_ =>
                    _.CurrentWind.WindType == currentWind
                    &&
                    _.NextWind.WindType == nextWind).Count();

                    double windChangeValue = changeFromCurrentToNextWind / windChanges.Count;

                    result.Add(windChangeValue);
                }

            }

            return result;
        }
    }
}

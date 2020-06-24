using System;
using System.Collections.Generic;
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
    }
}

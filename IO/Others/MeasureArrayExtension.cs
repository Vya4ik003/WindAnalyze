using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IO.Others
{
    public static class MeasureArrayExtension
    {
        public static (DateTime[], string[]) GetArrays(this Measure[] measures)
        {
            DateTime[] allDates = measures.Select(_=>_.MeasureTime).ToArray();
            string[] allWindNames = measures.Select(_ => _.DD).ToArray();

            return (allDates, allWindNames);
        }
    }
}

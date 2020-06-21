using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Information
{
    public class PeriodInformation : BaseInformation
    {
        public PeriodInformation(DateTime firstDate, DateTime lastDate,  Wind[] windsArray)
        {
            FirstMeasure = firstDate;
            LastMeasure = lastDate;
            CountOfMeasures = windsArray.Length;
            Winds = windsArray;
        }
    }
}

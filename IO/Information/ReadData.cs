using ExcelDataReader;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Information
{
    class ReadData
    {
        public int DateColumn { get; set; }
        public int WindTypeColumn { get; set; }
        public int InformationRowCount { get; set; }
        public JToken Messages { get; set; }

        public ReadData(int dateColumn, int windColumn, int infoRowCount)
        {
            DateColumn = dateColumn;
            WindTypeColumn = windColumn;
            InformationRowCount = infoRowCount;
        }

        public ReadData() { }

    }
}

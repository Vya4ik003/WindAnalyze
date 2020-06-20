using ExcelDataReader;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO
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

        public int[] FindColumns(IExcelDataReader reader)
        {
            int[] columns = new int[2];
            //Когда GetValue(i) вернёт null, произойдёт выход из цикла
            try
            {
                for (int i = 0; ; i++)
                {
                    if (reader.GetValue(i).ToString().Contains("Местное время"))
                    {
                        columns[0] = i;
                    }
                    else if (reader.GetValue(i).ToString().Contains("DD"))
                    {
                        columns[1] = i;
                        break;
                    }
                }
            }
            catch
            {
            }
            reader.Read();
            return columns;
        }

        public int GetCountOfInformationRows()
        {
            return 0;
        }
    }
}

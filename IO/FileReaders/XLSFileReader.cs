using ExcelDataReader;
using IO.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO.FileReaders
{
    class XLSFileReader : BaseFileReader
    {
        internal override ReadData Data { get; set; }

        internal override IExcelDataReader DataReader { get; set; }

        public XLSFileReader(FileStream stream)
        {
            DataReader = ExcelReaderFactory.CreateReader(stream);
            DataReader.Read();
        }

        public override Measure[] ReadRows(int countOfRows)
        {
            Measure[] measures = new Measure[countOfRows];

            int currentRow = 0;
            do
            {
                string measureDate = DataReader.GetString(Data.DateColumn);
                string ddColumn = DataReader.GetString(Data.WindTypeColumn);
                measures[currentRow] = new Measure(measureDate, ddColumn);

                currentRow++;
            }
            while (DataReader.Read());

            return measures;
        }

        public override Measure[][] ReadPeriods(Measure[] allMeasures)
        {
            IOrderedEnumerable<int> years = allMeasures.Select(_ => _.MeasureTime.Year).Distinct().OrderBy(_ => _);
            Measure[][] allPeriodsMeasures = new Measure[years.Count()][];

            int indexOfPeriod = 0;
            foreach(int year in years)
            {
                var periodMeasures = allMeasures.Where(_ => _.MeasureTime.Year == year);
                allPeriodsMeasures[indexOfPeriod] = periodMeasures.ToArray();

                indexOfPeriod++;
            }

            return allPeriodsMeasures;
        }

        public override int RowCount => DataReader.RowCount;

        public override string GetString(int column) => DataReader.GetString(column);

        public override (string[], int) SkipRowsAndGetInfoRows()
        {
            List<string> information = new List<string>();
            int informationRowCount = 0;

            for (; IsContainsInfo(DataReader.GetString(0)); informationRowCount++)
            {
                information.Add(DataReader.GetString(0));
                DataReader.Read();
            }

            return (information.ToArray(), informationRowCount);
        }

        public int[] FindColumns()
        {
            int[] columns = new int[2];
            //Когда GetValue(i) вернёт null, произойдёт выход из цикла
            try
            {
                for (int i = 0; ; i++)
                {
                    if (DataReader.GetValue(i).ToString().Contains("Местное время"))
                    {
                        columns[0] = i;
                    }
                    else if (DataReader.GetValue(i).ToString().Contains("DD"))
                    {
                        columns[1] = i;
                        break;
                    }
                }
            }
            catch { }

            DataReader.Read();
            return columns;
        }

        private bool IsContainsInfo(string cell)
        {
            return cell.StartsWith("#");
        }

    }
}

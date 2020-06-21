using ExcelDataReader;
using IO.Others;
using System;
using System.Collections.Generic;
using System.IO;

namespace IO.FileReaders
{
    class XLSFileReader : BaseFileReader
    {

        /// <summary>
        /// Инициализирует новый экземпляр типа XLSFileReader
        /// </summary>
        /// <param name="stream">Поток открытия файла</param>
        public XLSFileReader(FileStream stream)
        {
            DataReader = ExcelReaderFactory.CreateReader(stream);
            DataReader.Read();
        }

        public override (Measure[], string[]) ReadFile()
        {
            string[] allInfoRows = ReadInfoRows();
            FindColumns();
            Measure[] allMeasures = ReadMeasures();

            return (allMeasures, allInfoRows);
        }

        protected override string[] ReadInfoRows()
        {
            List<string> informationRows = new List<string>();

            while (DataReader.GetString(0).StartsWith("#"))
            {
                informationRows.Add(DataReader.GetString(0));
                DataReader.Read();
            }

            return informationRows.ToArray();
        }

        protected override void FindColumns()
        {
            int dateTimeColumn = 0;
            int ddWindColumn = 0;

            try
            {
                for (int i = 0; ; i++)
                {
                    string columnName = DataReader.GetString(i);

                    if (columnName.Contains("Местное время"))
                    {
                        dateTimeColumn = i;
                    }
                    else if (columnName.Contains("DD"))
                    {
                        ddWindColumn = i;
                        break;
                    }
                }
            }
            catch
            {
            }

            DataReader.Read();

            DateColumm = dateTimeColumn;
            DDWindColumn = ddWindColumn;
        }

        protected override Measure[] ReadMeasures()
        {
            List<Measure> measures = new List<Measure>();

            while (DataReader.Read())
            {
                string stringDate = DataReader.GetString(DateColumm);
                string dd = DataReader.GetString(DDWindColumn);
                DateTime date = DateTime.Parse(stringDate);

                Measure currentMeasure = new Measure(date, dd);
                measures.Add(currentMeasure);
            }

            return measures.ToArray();
        }
    }
}

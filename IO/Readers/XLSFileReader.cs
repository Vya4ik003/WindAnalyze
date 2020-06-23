using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO.Readers
{
    class XLSFileReader : IFileReader
    {
        private IExcelDataReader XlsFileReader { get; }

        private int _dateTimeColumnIndex;
        private int _ddColumnIndex;

        private List<string> AllInfoRows { get; } = new List<string>();
        private List<DateTime> AllDateTimes { get; } = new List<DateTime>();
        private List<string> AllWindTypes { get; } = new List<string>();

        public XLSFileReader(FileStream openFileStream)
        {
            XlsFileReader = ExcelReaderFactory.CreateReader(openFileStream);
        }

        public ReadFileResult GetReadingResult()
        {
            GetInfoRows();
            FindColumns();
            GetAllDateTimesAndAllWindTypes();
            FileInformation information = new FileInformation(AllInfoRows.ToArray());

            return new ReadFileResult(AllDateTimes, AllWindTypes, information);
        }

        private void GetInfoRows()
        {
            XlsFileReader.Read();

            string row = XlsFileReader.GetString(0);
            while (row.StartsWith("#") && XlsFileReader.Read())
            {
                AllInfoRows.Add(row);
                row = XlsFileReader.GetString(0);
            }
        }

        private void FindColumns()
        {
            try
            {
                for (int i = 0; ; i++)
                {
                    string row = XlsFileReader.GetString(i);
                    if (row.Contains("Местное время"))
                    {
                        _dateTimeColumnIndex = i;
                    }
                    else if (row.Contains("DD"))
                    {
                        _ddColumnIndex = i;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void GetAllDateTimesAndAllWindTypes()
        {
            while (XlsFileReader.Read())
            {
                //Невозможно использовать метод "GetDateTime()", так как вместо "DateTime.Parse(...)" 
                //данный метод использует приведение (DateTime)GetValue(...), и тут возникает исключение
                string stringDate = XlsFileReader.GetString(_dateTimeColumnIndex);
                DateTime dateTimeColumn = DateTime.Parse(stringDate);
                string ddColumn = XlsFileReader.GetString(_ddColumnIndex);

                AllDateTimes.Add(dateTimeColumn);
                AllWindTypes.Add(ddColumn);
            }
        }
    }
}

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

        /// <summary>
        /// Индекс столбца с датой измерения
        /// </summary>
        private int _dateTimeColumnIndex;

        /// <summary>
        /// Индекс столбца с типом ветра
        /// </summary>
        private int _ddColumnIndex;

        /// <summary>
        /// Информационные строки
        /// </summary>
        private List<string> InfoRows { get; } = new List<string>();

        /// <summary>
        /// Все даты измерений
        /// </summary>
        private List<DateTime> AllDateTimes { get; } = new List<DateTime>();

        /// <summary>
        /// Все типы ветров
        /// </summary>
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

            return new ReadFileResult(AllDateTimes, AllWindTypes, InfoRows);
        }

        /// <summary>
        /// Получение всех информационных строк
        /// </summary>
        private void GetInfoRows()
        {
            XlsFileReader.Read();

            string row = XlsFileReader.GetString(0);
            while (row.StartsWith("#") && XlsFileReader.Read())
            {
                InfoRows.Add(row);
                row = XlsFileReader.GetString(0);
            }
        }

        /// <summary>
        /// Поиск нужных столбцов
        /// </summary>
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

        /// <summary>
        /// Получение всех дат и типов ветров
        /// </summary>
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

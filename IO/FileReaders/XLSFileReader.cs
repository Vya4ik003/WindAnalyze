using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO.FileReaders
{
    class XLSFileReader : IFileReader
    {
        public ReadData Data { private get; set; }
        public IExcelDataReader ExcelDataReader { internal get; set; }

        public XLSFileReader(FileStream stream)
        {
            ExcelDataReader = ExcelReaderFactory.CreateReader(stream);
            ExcelDataReader.Read();
        }

        public int RowCount => ExcelDataReader.RowCount;
        public string GetString(int column) => ExcelDataReader.GetString(column);

        public Measure[] ReadRows(int countOfRows)
        {
            Measure[] measures = new Measure[countOfRows];

            int currentRow = 0;
            do
            {
                string measureDate = ExcelDataReader.GetString(Data.DateColumn);
                string ddColumn = ExcelDataReader.GetString(Data.WindTypeColumn);
                measures[currentRow] = new Measure(measureDate, ddColumn);

                currentRow++;
            }
            while (ExcelDataReader.Read());

            return measures;
        }

        public (string[], int) SkipRowsAndGetInfoRows()
        {
            List<string> information = new List<string>();
            int informationRowCount = 0;

            for (; IsContainsInfo(ExcelDataReader.GetString(0)); informationRowCount++)
            {
                information.Add(ExcelDataReader.GetString(0));
                ExcelDataReader.Read();
            }

            return (information.ToArray(), informationRowCount);
        }

        private bool IsContainsInfo(string cell)
        {
            return cell.StartsWith("#");
        }
    }
}

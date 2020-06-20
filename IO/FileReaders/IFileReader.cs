using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.FileReaders
{
    interface IFileReader
    {
        public ReadData Data { set; }

        public IExcelDataReader ExcelDataReader { set; }

        public int RowCount { get; }

        string GetString(int column);

        Measure[] ReadRows(int countOfRows);

        (string[], int) SkipRowsAndGetInfoRows();
    }
}

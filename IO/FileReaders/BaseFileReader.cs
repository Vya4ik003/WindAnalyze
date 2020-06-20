using ExcelDataReader;
using IO.Information;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.FileReaders
{
    abstract class BaseFileReader
    {
        internal abstract IExcelDataReader DataReader { get; set; }

        internal abstract ReadData Data { get; set; }

        public abstract Measure[] ReadRows(int countOfRows);

        public abstract Measure[][] ReadPeriods(Measure[] allMeasures);

        public abstract int RowCount { get; }

        public abstract string GetString(int column);

        public abstract (string[], int) SkipRowsAndGetInfoRows();
    }
}

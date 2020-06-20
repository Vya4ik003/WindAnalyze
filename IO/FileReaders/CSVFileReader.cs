using ExcelDataReader;
using System;

namespace IO.FileReaders
{
    class CSVFileReader : BaseFileReader
    {
        public override (Measure[], string[]) ReadFile()
        {
            throw new NotImplementedException();
        }

        protected override void FindColumns()
        {
            throw new NotImplementedException();
        }

        protected override string[] ReadInfoRows()
        {
            throw new NotImplementedException();
        }

        protected override Measure[] ReadMeasures()
        {
            throw new NotImplementedException();
        }
    }
}

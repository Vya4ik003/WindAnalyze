using IO.Others;
using System;
using System.IO;

namespace IO.FilesFormat
{
    internal class CSVFile : BaseFile
    {
        //TODO: Сделать метод рабочим
        public override IOResult GetIOResultFromFile(FileStream stream)
        {
            return new IOResult(new NotImplementedException().Message);
        }
    }
}

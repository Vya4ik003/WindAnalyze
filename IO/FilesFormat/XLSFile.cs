using IO.FileReaders;
using IO.Others;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IO.FilesFormat
{
    internal class XLSFile : BaseFile
    {
        public override IOResult GetIOResultFromFile(FileStream stream)
        {
            XLSFileReader fileReader = new XLSFileReader(stream);

            (Measure[], string[]) infoPair = fileReader.ReadFile();

            return new IOResult(
                message: "Success",
                measures: infoPair.Item1,
                infoRows: infoPair.Item2
                );
        }
    }
}

using IO.FileReaders;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IO.FilesFormat
{
    internal class XLSFile : BaseFile
    {
        public override IOResult GetIOResultFromFile(FileStream stream)
        {
            JObject messages = JObject.Parse(File.ReadAllText("ResultMessages.json"));
            XLSFileReader fileReader = new XLSFileReader(stream);

            (Measure[], string[]) infoPair = fileReader.ReadFile();

            return new IOResult(
                messages["Success"],
                infoPair.Item1,
                infoPair.Item2
                );
        }
    }
}

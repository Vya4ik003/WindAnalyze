using IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.IO;

namespace UI
{
    class FileWorker
    {
        public IOResult OpenFile()
        {
            var dialog = new OpenFileDialog();

            var resultMessages = JObject.Parse(File.ReadAllText("ResultMessages.json"));

            if (dialog.ShowDialog() == true)
            {
                var preparer = new FilePreparer();
                IOResult result = preparer.ReadFile(dialog.FileName, resultMessages);

                return result;
            }

            return new IOResult(resultMessages["Null"].ToString());
        }
    }
}

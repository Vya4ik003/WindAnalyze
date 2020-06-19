using IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.IO;

namespace UI
{
    class FileWorker
    {
        /// <summary>
        /// Проводит необходимые действия для открытия чтения файла
        /// </summary>
        /// <returns>Возвращает IOResult</returns>
        //TODO: Сделать выбор только xls/xlsx и csv файлов в OpenFileDialog
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

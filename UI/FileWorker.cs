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
        public IOResult OpenFile()
        {
            var defaultDialog = new DefaultOpenFileDialog();
            var dialog = defaultDialog.Dialog;

            var resultMessages = JObject.Parse(File.ReadAllText("ResultMessages.json"));

            if (dialog.ShowDialog() == true)
            {
                var preparer = new FilePreparer();
                IOResult result = preparer.ReadFile(dialog.FileName, resultMessages);

                return result;
            }

            return new IOResult(resultMessages["Null"].ToString());
        }

        private class DefaultOpenFileDialog
        {
            public OpenFileDialog Dialog;

            public DefaultOpenFileDialog()
            {
                Dialog = new OpenFileDialog
                {
                    Filter = "Таблица Excel (*.xls;*.xlsx)|*.xls;*.xlsx|Текстовый файл CSV (*.csv)|*.csv",
                };
            }
        }
    }
}

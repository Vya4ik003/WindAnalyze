using Buisness;
using Buisness.Winds;
using IO.Others;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace UI
{
    /// <summary>
    /// Класс, обеспечивающий взаимодействие между модулями
    /// </summary>
    class Interaction
    {
        /// <summary>
        /// Проводит необходимые действия для открытия чтения файла
        /// </summary>
        /// <returns>Возвращает IOResult, содержащий информационные строки и массив измерений</returns>
        public IOResult OpenFile()
        {
            var defaultDialog = new DefaultOpenFileDialog();
            var dialog = defaultDialog.Dialog;

            if (dialog.ShowDialog() == true)
            {
                var preparer = new FilePreparer();
                IOResult result = preparer.PrepareFile(dialog.FileName);

                return result;
            }

            return new IOResult("Null");
        }

        public BLResult GetStatistic(IOResult ioResult)
        {
            var pairs = ioResult.Measures.GetArrays();
            DateTime[] allDates = pairs.Item1;
            string[] allWinds = pairs.Item2;
            string[] infos = ioResult.InformationRows;

            WindsReader reader = new WindsReader(allDates, allWinds, infos);
            BLResult blResult = reader.ReadWinds();

            return blResult;
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

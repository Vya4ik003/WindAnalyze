using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace IO
{
    public class FilePreparer
    {
        /// <summary>
        /// Чтение файла и получение IEnumerable
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public IOResult ReadFile(string filePath)
        {
            var resultMessages = JObject.Parse(File.ReadAllText("ResultMessages.json"));

            FileStream stream;
            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return new IOResult(message: resultMessages["IOexception"].ToString());
            }
            using var reader = ExcelReaderFactory.CreateReader(stream);


            reader.Read();
            var firstCell = reader.GetString(0);
            var rowCount = IsContainsInfo(firstCell) ? reader.RowCount - 7 : HasHeaders(firstCell) ? reader.RowCount - 1 : reader.RowCount;

            SkipRows(reader);

            Measure[] measures = new Measure[rowCount];

            int currentRow = 0;
            do
            {
                string measureDate = reader.GetString(0);
                string ddColumn = reader.GetString(6);
                measures[currentRow] = new Measure(measureDate, ddColumn);

                currentRow++;
            }
            while (reader.Read());

            return new IOResult(measures: measures, message: resultMessages["Success"].ToString());
        }

        //TODO: Продвинутое чтение информации
        private bool IsContainsInfo(string firstCell)
        {
            return firstCell.Contains("#");
        }

        private bool HasHeaders(string firstCell)
        {
            return !DateTime.TryParse(firstCell, out _);
        }

        private void SkipRows(IExcelDataReader reader)
        {
            while (!DateTime.TryParse(reader.GetString(0), out _))
            {
                reader.Read();
            }
        }
    }
}

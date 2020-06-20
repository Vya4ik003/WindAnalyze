using ExcelDataReader;
using IO.FilesFormat;
using IO.Information;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IO
{
    public class FilePreparer
    {
        private int _dateColumn = 0;
        private int _windTypeColumn = 6;
        private int _informationRowCount = 6;

        /// <summary>
        /// Чтение файла и получение IEnumerable через IOResult
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="messages">Системные сообщения</param>
        /// <returns>Возвращает IOResult</returns>
        //TODO: Сделать чтение CSV файла
        public IOResult ReadFile(string filePath, JObject messages)
        {
            FileStream stream;
            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return new IOResult(message: messages["IOexception"].ToString());
            }
            
            ReadData readData = new ReadData(_dateColumn, _windTypeColumn, _informationRowCount) { Messages = messages };
            IOResult result;
            if (filePath.EndsWith(".xls") || filePath.EndsWith(".xlsx"))
                result = new XLSFile().ReadFile(stream, readData);
            else
                result = new CSVFile().ReadFile(stream, readData);

            stream.Close();
            return result;
        }


    }
}

using IO.FilesFormat;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IO
{
    public class FilePreparer
    {
        /// <summary>
        /// Чтение файла и получение IEnumerable через IOResult
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="messages">Системные сообщения</param>
        /// <returns>Возвращает IOResult с информационными строками и массивом измерений</returns>
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
            
            IOResult result;

            if (filePath.EndsWith(".xls") || filePath.EndsWith(".xlsx"))
                result = new XLSFile().GetIOResultFromFile(stream);
            else
                result = new CSVFile().GetIOResultFromFile(stream);

            stream.Close();
            return result;
        }


    }
}

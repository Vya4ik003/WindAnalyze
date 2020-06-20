using Newtonsoft.Json.Linq;

namespace IO
{
    public class IOResult
    {
        /// <summary>
        /// Сообщение результата
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Все измерениея
        /// </summary>
        public Measure[] Measures { get; set; }

        /// <summary>
        /// Первые строки файла, содержащие информацию
        /// </summary>
        public string[] InformationRows { get; set; }

        /// <summary>
        /// Инициализирует новый обьект класса Result, если не возникло исключения при открытии файла
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="measures">Измерения</param>
        public IOResult(JToken message, Measure[] measures, string[] infoRows)
        {
            Message = message.ToString();
            Measures = measures;
            InformationRows = infoRows;
        }

        /// <summary>
        /// Инициализирует новый обьект класса Result, если возникло исключение при открытии файла
        /// </summary>
        /// <param name="message">Сообщение</param>
        public IOResult(string message)
        {
            Message = message;
        }
    }
}

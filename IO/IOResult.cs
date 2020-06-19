using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO
{
    public class IOResult
    {
        /// <summary>
        /// Массив прочитанных измерений
        /// </summary>
        public Measure[] Measures { get; set; }

        /// <summary>
        /// Сообщение результата
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Информация, взятая с первых 6 строк файла
        /// </summary>
        public IDictionary<string, string> HeaderRowsInfo { get; set; }

        /// <summary>
        /// Инициализирует новый обьект класса Result, если не возникло исключения при открытии файла
        /// </summary>
        /// <param name="measures">Массив измерений</param>
        /// <param name="message">Сообщение</param>
        public IOResult(Measure[] measures, JToken message, IDictionary<string, string> infoFromHeaderRows)
        {
            Measures = measures;
            Message = message.ToString();
            HeaderRowsInfo = infoFromHeaderRows;
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

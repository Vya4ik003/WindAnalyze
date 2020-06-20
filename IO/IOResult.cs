using IO.Information;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO
{
    public class IOResult
    {
        /// <summary>
        /// Сообщение результата
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Информация, о файле
        /// </summary>
        public FileInformation InformationAboutFile { get; set; }

        /// <summary>
        /// Инициализирует новый обьект класса Result, если не возникло исключения при открытии файла
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="infoAboutFile">Информация о файле</param>
        /// <param name="infoAboutPeriods">Информация о всех периодах</param>
        public IOResult(JToken message, FileInformation infoAboutFile)
        {
            Message = message.ToString();
            InformationAboutFile = infoAboutFile;
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

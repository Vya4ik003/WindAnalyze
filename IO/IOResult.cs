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
        /// Инициализирует новый обьект класса Result, если не возникло исключения
        /// </summary>
        /// <param name="measures">Массив измерений</param>
        /// <param name="message">Сообщение</param>
        public IOResult(Measure[] measures, string message)
        {
            Measures = measures;
            Message = message;
        }

        /// <summary>
        /// Инициализирует новый обьект класса Result, если возникло исключение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public IOResult(string message)
        {
            Message = message;
        }
    }
}

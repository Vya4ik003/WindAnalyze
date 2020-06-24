using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Readers
{
    interface IFileReader
    {
        /// <summary>
        /// Метод для получения результата чтения
        /// </summary>
        /// <returns>Результат чтения</returns>
        ReadFileResult GetReadingResult(); 
    }
}

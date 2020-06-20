using System.IO;

namespace IO.FilesFormat
{
    abstract class BaseFile
    {
        /// <summary>
        /// Производит чтнеие файла и получение IOResult
        /// </summary>
        /// <param name="stream">Поток открытия файла</param>
        /// <returns>IOResult</returns>
        public abstract IOResult GetIOResultFromFile(FileStream stream);
    }
}

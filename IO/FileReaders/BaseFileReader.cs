using ExcelDataReader;

namespace IO.FileReaders
{
    abstract class BaseFileReader
    {
        /// <summary>
        /// Номер столбца с датой измерения
        /// </summary>
        protected int DateColumm { get; set; }

        /// <summary>
        /// Номер столбца с типом ветра измерения
        /// </summary>
        protected int DDWindColumn { get; set; }

        /// <summary>
        /// Экземлпяр класса IExcelDataReader
        /// </summary>
        internal IExcelDataReader DataReader { get; set; }

        /// <summary>
        /// Получает пару "Массив измерений" - "Массив информационных строк"
        /// </summary>
        /// <returns>(Measure[], string[])</returns>
        public abstract (Measure[], string[]) ReadFile();

        /// <summary>
        /// Читает информационные строки
        /// </summary>
        /// <returns>string[]</returns>
        protected abstract string[] ReadInfoRows();

        /// <summary>
        /// Находит номера строк для Времени измерения и для типа ветра
        /// </summary>
        protected abstract void FindColumns();

        /// <summary>
        /// Читает все строки файла (кроме информационных) и выделяет Measure[]
        /// </summary>
        /// <returns>Measure[]</returns>
        protected abstract Measure[] ReadMeasures();
    }
}

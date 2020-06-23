using IO.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO
{
    public class ReadFileResult
    {
        private const string _readCanceledMessaage = "Read canceled";
        public static ReadFileResult EmptyResult = new ReadFileResult(_readCanceledMessaage);

        public bool IsSuccess { get; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Даты всех измерений
        /// </summary>
        public List<DateTime> MeasureDates { get; }

        /// <summary>
        /// Типы ветров всех измерений
        /// </summary>
        public List<string> MeasureWindTypes { get; }

        /// <summary>
        /// Информационные строки
        /// </summary>
        public FileInformation FileInformation { get; }

        /// <summary>
        /// Инициализирует новый экземлпяр класса ReadFileResult, если не возникло исключения
        /// </summary>
        /// <param name="measureDates"></param>
        /// <param name="measureWindTypes"></param>
        /// <param name="fileInfo"></param>
        public ReadFileResult(List<DateTime> measureDates, List<string> measureWindTypes, FileInformation fileInfo)
        {
            MeasureDates = measureDates;
            MeasureWindTypes = measureWindTypes;
            FileInformation = fileInfo;

            IsSuccess = true;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса ReadFileResult, если возникло исключение
        /// </summary>
        /// <param name="errorMessage"></param>
        public ReadFileResult(string errorMessage)
        {
            ErrorMessage = errorMessage;

            IsSuccess = false;
        }

        public static ReadFileResult GetResult(string filePath)
        {
            try
            {
                using FileStream openFileStream = new FileStream(path: filePath, mode: FileMode.Open, access: FileAccess.Read);
                IFileReader reader;

                if (filePath.EndsWith(".xls") || filePath.EndsWith(".xlsx"))
                    reader = new XLSFileReader(openFileStream);
                else
                    reader = new CSVFileReader();

                return reader.GetReadingResult();
            }
            catch (Exception e)
            {
                return new ReadFileResult(errorMessage: e.Message);
            }
        }
    }
}

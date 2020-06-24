using IO.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IO
{
    public class ReadFileResult
    {
        //TODO: Добавить вариации ошибок
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

        public string FirstInformationRow { get; } // # Метеостанция Москва (ВДНХ), Россия, WMO_ID=27612, выборка с 01.06.2014 по 18.06.2020, все дни
        public string SecondInformationRow { get; } // # Кодировка: UTF-8
        public string ThirdInformationRow { get; } // # Информация предоставлена сайтом "Расписание Погоды", rp5.ru

        //Следующие строки не содержат важной информации
        //public string FourthInformationRow { get; } // # Пожалуйста, при использовании данных, любезно указывайте названный сайт.
        //public string FifthInformationRow { get; } // # Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=27612&lang=ru
        //public string SixthInformationRow { get; } // #

        /// <summary>
        /// Инициализирует новый экземлпяр класса ReadFileResult, если не возникло исключения
        /// </summary>
        /// <param name="measureDates"></param>
        /// <param name="measureWindTypes"></param>
        /// <param name="informationRows"></param>
        public ReadFileResult(List<DateTime> measureDates, List<string> measureWindTypes, List<string> informationRows)
        {
            MeasureDates = measureDates;
            MeasureWindTypes = measureWindTypes;

            FirstInformationRow = informationRows[0];
            SecondInformationRow = informationRows[1];
            ThirdInformationRow = informationRows[2];

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

       /// <summary>
       /// Метод для чтения файла и получения результата
       /// </summary>
       /// <param name="filePath">Путь к файлу</param>
       /// <returns>Результат чтения</returns>
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

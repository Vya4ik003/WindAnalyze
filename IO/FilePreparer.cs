using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace IO
{
    public class FilePreparer
    {
        //TODO: Проверка файла на номера столбцов
        private const int _dateColumn = 0;
        private const int _windTypeColumn = 6;

        //TODO: Сделать проверку на кол-во информационных строк
        private const int _informationRowCount = 6;

        /// <summary>
        /// Чтение файла и получение IEnumerable через IOResult
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="messages">Системные сообщения</param>
        /// <returns>Возвращает IOResult</returns>
        //TODO: Сделать чтение CSV файла
        //TODO: Сделать проверку файла на формат
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
            using var fileReader = ExcelReaderFactory.CreateReader(stream);

            fileReader.Read();
            var firstCell = fileReader.GetString(_dateColumn);
            var rowCount = IsContainsInfo(firstCell) ? fileReader.RowCount - 7 : HasHeaders(firstCell) ? fileReader.RowCount - 1 : fileReader.RowCount;

            Measure[] fileMeasures = new Measure[rowCount];

            string[] headerRows = SkipRowsAndGetHeaderRows(fileReader);
            IDictionary<string, string> fileInformation = GetInfroFromHeaderRows(headerRows);
            fileMeasures = ReadRows(fileReader);

            return new IOResult(
                measures: fileMeasures,
                message: messages["Success"],
                infoFromHeaderRows: fileInformation);
        }

        /// <summary>
        /// Метод для чтения строк в файле
        /// </summary>
        /// <param name="measures">Массив измеренияй для измненения</param>
        /// <param name="reader">Интерфейс чтения</param>
        /// <returns>Возвращает массив прочитанных измерений</returns>
        private Measure[] ReadRows(IExcelDataReader reader)
        {
            Measure[] measures = new Measure[reader.RowCount];
            int currentRow = 0;
            do
            {
                string measureDate = reader.GetString(_dateColumn);
                string ddColumn = reader.GetString(_windTypeColumn);
                measures[currentRow] = new Measure(measureDate, ddColumn);

                currentRow++;
            }
            while (reader.Read());

            return measures;
        }

        //TODO: Продвинутое чтение информации
        private bool IsContainsInfo(string cell)
        {
            return cell.Contains("#");
        }

        private bool HasHeaders(string firstCell)
        {
            return !DateTime.TryParse(firstCell, out _);
        }

        /// <summary>
        /// Пропускает и записывает в массив информационные строки
        /// </summary>
        /// <param name="reader">Интерфейс чтения</param>
        /// <returns>Возвращает массив информационных строк</returns>
        private string[] SkipRowsAndGetHeaderRows(IExcelDataReader reader)
        {
            string[] information = new string[_informationRowCount];

            for (int indexOfRow = 0; !DateTime.TryParse(reader.GetString(_dateColumn), out _); indexOfRow++)
            {
                if (IsContainsInfo(reader.GetString(0)))
                {
                    information[indexOfRow] = reader.GetString(0);
                }
                reader.Read();
            }

            return information;
        }

        /// <summary>
        /// Выделяет информацию из информационных строк
        /// </summary>
        /// <param name="headerRows">Массив информациогнных строк</param>
        /// <returns>Возвращает массив с выделенной информацией</returns>
        private IDictionary<string, string> GetInfroFromHeaderRows(string[] headerRows)
        {
            string firstRow = headerRows[0]; // # Метеостанция Москва (ВДНХ), Россия, WMO_ID=27612, выборка с 01.06.2018 по 18.06.2020, все дни
            string secondRow = headerRows[1]; // # Кодировка: UTF-8
            string thirdRow = headerRows[2]; // # Информация предоставлена сайтом "Расписание Погоды", rp5.ru

            //Следующие заголовочные строки не содержат важной для файла информации. См. далее:
            //string fourthRow = infoRows[3]; - # Пожалуйста, при использовании данных, любезно указывайте названный сайт.
            //string fifthRow = infoRows[4]; - # Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=27612&lang=ru
            //string sixthRow = infoRows[5]; - #

            Regex regexForFirstRow = new Regex(@"Метеостанция (?<station>\D+), WMO_ID=(?<WMO_ID>\d+), выборка с (?<firstDate>\S+) по (?<lastDate>\w[^,]+), (?<type>\D+)");
            Regex regexForSecondRow = new Regex(@"Кодировка: (?<encoding>\S+)");
            Regex regexForThirdRow = new Regex(@"Информация предоставлена сайтом (?<siteRU>[^@]*)");

            Match matchForFirstRow = regexForFirstRow.Match(firstRow);
            Match matchForSecondRow = regexForSecondRow.Match(secondRow);
            Match matchForThirdRow = regexForThirdRow.Match(thirdRow);

            if (matchForFirstRow.Success && matchForSecondRow.Success && matchForThirdRow.Success)
                return new Dictionary<string, string>()
                {
                    {"Станция", matchForFirstRow.Result("${station}")},
                    {"WMO_ID", matchForFirstRow.Result("${WMO_ID}") },
                    {"Первое измерение", matchForFirstRow.Result("${firstDate}") },
                    {"Последнее измерение", matchForFirstRow.Result("${lastDate}") },
                    {"Выборка",  matchForFirstRow.Result("${type}") },
                    {"Кодировка", matchForSecondRow.Result("${encoding}") },
                    {"Сайт", matchForThirdRow.Result("${siteRU}") }
                };
            else
                return null;
        }
    }
}

using ExcelDataReader;
using IO.Information;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IO
{
    public class FilePreparer
    {
        private int _dateColumn = 0;
        private int _windTypeColumn = 6;
        private int _informationRowCount = 6;

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

            (string[], int) inforRowsTuple = SkipRowsAndGetInfoRows(fileReader);

            //var columnCount = GetColumnCount(fileReader);
            var columns = FindColumns(fileReader);

            _dateColumn = columns[0];
            _windTypeColumn = columns[1];
            _informationRowCount = inforRowsTuple.Item2;

            FileInformation fileInformation = GetInfroFromHeaderRows(inforRowsTuple.Item1, rowCount);
            fileMeasures = ReadRows(fileReader, rowCount);
            fileInformation.AllMeasures = fileMeasures.OrderBy(_ => _.MeasureTime).ToArray();

            return new IOResult(
                message: messages["Success"],
                infoAboutFile: fileInformation);
        }

        /// <summary>
        /// Метод для чтения строк в файле
        /// </summary>
        /// <param name="measures">Массив измеренияй для измненения</param>
        /// <param name="reader">Интерфейс чтения</param>
        /// <returns>Возвращает массив прочитанных измерений</returns>
        private Measure[] ReadRows(IExcelDataReader reader, int countOfRows)
        {
            Measure[] measures = new Measure[countOfRows];
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
        private (string[], int) SkipRowsAndGetInfoRows(IExcelDataReader reader)
        {
            string[] information = new string[_informationRowCount];
            int informationRowCount = 0;

            for (; IsContainsInfo(reader.GetString(0)); informationRowCount++)
            {
                information[informationRowCount] = reader.GetString(0);
                reader.Read();
            }

            return (information, informationRowCount);
        }

        /// <summary>
        /// Выделяет информацию из информационных строк
        /// </summary>
        /// <param name="headerRows">Массив информациогнных строк</param>
        /// <returns>Возвращает массив с выделенной информацией</returns>
        private FileInformation GetInfroFromHeaderRows(string[] headerRows, int rowCount)
        {
            string firstRow = headerRows[0]; // # Метеостанция Москва (ВДНХ), Россия, WMO_ID=27612, выборка с 01.06.2018 по 18.06.2020, все дни
            string secondRow = headerRows[1]; // # Кодировка: UTF-8
            string thirdRow = headerRows[2]; // # Информация предоставлена сайтом "Расписание Погоды", rp5.ru

            //Следующие заголовочные строки не содержат важной для файла информации. См. далее:
            //string fourthRow = infoRows[3]; - # Пожалуйста, при использовании данных, любезно указывайте названный сайт.
            //string fifthRow = infoRows[4]; - # Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=27612&lang=ru
            //string sixthRow = infoRows[5]; - #

            InformationRow firstInformationRow = new InformationRow(
                "Метеостанция (?<...>.*), (?<...>.*)=(?<...>.*), выборка с (?<...>.*) по (?<...>.*), (?<...>.*)",
                firstRow,
                "station", "codeType", "code", "firstDate", "lastDate", "type");
            InformationRow secondInformationRow = new InformationRow(
                "Кодировка: (?<...>.*)",
                secondRow,
                "encoding"
                );
            InformationRow thirdInformationRow = new InformationRow(
                "Информация предоставлена сайтом (?<...>.*)",
                thirdRow,
                "site"
                );

            FileInformation fileInformation = new FileInformation(
                firstInformationRow.Output[0],
                firstInformationRow.Output[1],
                firstInformationRow.Output[2],
                firstInformationRow.Output[3],
                firstInformationRow.Output[4],
                rowCount,
                firstInformationRow.Output[5],
                secondInformationRow.Output[0],
                thirdInformationRow.Output[0]
                );

            if (firstInformationRow.MatchForInformationRow.Success &&
                secondInformationRow.MatchForInformationRow.Success &&
                thirdInformationRow.MatchForInformationRow.Success)
                return fileInformation;
            else
                return null;
        }

        private int[] FindColumns(IExcelDataReader reader)
        {
            int[] columns = new int[2];
            //Когда GetValue(i) вернёт null, произойдёт выход из цикла
            try
            {
                for (int i = 0; ; i++)
                {
                    if (reader.GetValue(i).ToString().Contains("Местное время"))
                    {
                        columns[0] = i;
                    }
                    else if (reader.GetValue(i).ToString().Contains("DD"))
                    {
                        columns[1] = i;
                        break;
                    }
                }
            }
            catch
            {
            }
            reader.Read();
            return columns;
        }
    }
}

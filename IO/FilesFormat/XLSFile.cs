﻿using ExcelDataReader;
using IO.FileReaders;
using IO.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO.FilesFormat
{
    internal class XLSFile : IFile
    {
        public IOResult ReadFile(FileStream stream, ReadData data)
        {
            XLSFileReader fileReader = new XLSFileReader(stream);

            var firstCell = fileReader.GetString(0);
            var rowCount = IsContainsInfo(firstCell) ? fileReader.RowCount - 7 : HasHeaders(firstCell) ? fileReader.RowCount - 1 : fileReader.RowCount;

            Measure[] fileMeasures = new Measure[rowCount];

            (string[], int) infoRowsTuple = fileReader.SkipRowsAndGetInfoRows();

            var columns = data.FindColumns(fileReader.ExcelDataReader);

            fileReader.Data = new ReadData(columns[0], columns[1], infoRowsTuple.Item2);

            FileInformation fileInformation = GetInfroFromHeaderRows(infoRowsTuple.Item1, rowCount);
            fileMeasures = fileReader.ReadRows(rowCount);
            fileInformation.AllMeasures = fileMeasures.OrderBy(_ => _.MeasureTime).ToArray();

            return new IOResult(
                message: data.Messages["Success"],
                infoAboutFile: fileInformation);
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
    }
}

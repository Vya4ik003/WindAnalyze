using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Buisness.Information
{
    public class FileInformation : BaseInformation
    {
        /// <summary>
        /// Положение метеостанции
        /// </summary>
        public string Meteostation { get; set; }

        /// <summary>
        /// Тип кода метеостанции
        /// </summary>
        public string MeteostationCodeType { get; set; }

        /// <summary>
        /// Код метеостанции
        /// </summary>
        public string MeteostationCode { get; set; }

        /// <summary>
        /// Выборка
        /// </summary>
        public string TypeOfSample { get; set; }

        /// <summary>
        /// Кодировка
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; set; }

        public FileInformation(DateTime[] allDates, string[] allWinds, string[] infoRows)
        {
            Winds = GetWinds(allDates, allWinds);
            GetFileInfo(infoRows);
        }

        private Wind[] GetWinds(DateTime[] dates, string[] winds)
        {
            //TODO: сделать проверку на равенство массивов
            int windCount = dates.Length;

            Wind[] allWinds = new Wind[windCount];
            for (int i = 0; i < windCount - 1; i++)
            {
                DateTime windTime = dates[i];
                string windType = winds[i];

                allWinds[i] = new Wind(windTime, windType);
            }

            return allWinds;
        }

        private void GetFileInfo(string[] infoRows)
        {
            string firstRow = infoRows[0]; // # Метеостанция Москва (ВДНХ), Россия, WMO_ID=27612, выборка с 01.06.2018 по 18.06.2020, все дни
            string secondRow = infoRows[1]; // # Кодировка: UTF-8
            string thirdRow = infoRows[2]; // # Информация предоставлена сайтом "Расписание Погоды", rp5.ru

            //Следующие заголовочные строки не содержат важной для файла информации. См. далее:
            //string fourthRow = infoRows[3]; - # Пожалуйста, при использовании данных, любезно указывайте названный сайт.
            //string fifthRow = infoRows[4]; - # Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=27612&lang=ru
            //string sixthRow = infoRows[5]; - #

            InformationRow firstInformationRow = new InformationRow(
                "Метеостанция (?<...>.*), (?<...>.*)=(?<...>.*), выборка с (?<...>.*) по (?<...>.*), (?<...>.*)",
                firstRow,
                "station", "codeType", "code", "firstDate", "lastDate", "type");

            Meteostation = firstInformationRow.Output[0];
            MeteostationCodeType = firstInformationRow.Output[1];
            MeteostationCode = firstInformationRow.Output[2];
            FirstMeasure = DateTime.Parse(firstInformationRow.Output[3]);
            LastMeasure = DateTime.Parse(firstInformationRow.Output[4]);
            TypeOfSample = firstInformationRow.Output[5];

            InformationRow secondInformationRow = new InformationRow(
                "Кодировка: (?<...>.*)",
                secondRow,
                "encoding"
                );

            Encoding = secondInformationRow.Output[0];

            InformationRow thirdInformationRow = new InformationRow(
                "Информация предоставлена сайтом (?<...>.*)",
                thirdRow,
                "site"
                );

            Site = thirdInformationRow.Output[0];
        }

        public PeriodInformation[] GetPeriodsInformation()
        {
            List<PeriodInformation> informationAboutPeriods = new List<PeriodInformation>();
            int[] years = Winds.Select(_ => _.MeasureTime.Year).Distinct().ToArray();

            foreach (int year in years)
            {
                Wind[] periodWinds = Winds.Where(_ => _.MeasureTime.Year == year).ToArray();

                DateTime firstDate = periodWinds.First().MeasureTime;
                DateTime lastDate = periodWinds.Last().MeasureTime;

                PeriodInformation periodInformation = new PeriodInformation(firstDate, lastDate, periodWinds);
                informationAboutPeriods.Add(periodInformation);
            }

            return informationAboutPeriods.ToArray();
        } 
    }
}

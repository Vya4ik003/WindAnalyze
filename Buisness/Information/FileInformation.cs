using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Buisness.Information
{
    public class FileInformation
    {
        public string Meteostation { get; private set; }
        public string MeteostationCodeType { get; private set; }
        public string MeteostationCode { get; private set; }
        public DateTime FirstWindDate { get; private set; }
        public DateTime LastWindDate { get; private set; }
        public string SampleType { get; private set; }
        public string Encoding { get; private set; }
        public string Site { get; private set; }

        public IList<WindType> WindTypes { get; private set; }
        private IList<Wind> Winds { get; }
        public IList<WindChange> WindChanges { get; private set; }

        public FileInformation(List<DateTime> windTimes, List<string> windTypes, string[] informationRows)
        {
            Winds = GetWinds(windTimes, windTypes);
            GetFileInfo(informationRows.ToList());
            WindChanges = GetWindChanges();
            WindTypes = GetWindTypes();
        }

        /// <summary>
        /// Метод для получения информации о периодах
        /// </summary>
        /// <returns>Информация о периодах</returns>
        public IList<PeriodInformation> GetPeriodsInformation()
        {
            IEnumerable<int> years = Winds.Select(_ => _.WindDate.Year).Distinct();
            IList<PeriodInformation> periodsInfos = new List<PeriodInformation>(years.Count());

            foreach (int year in years)
            {
                IList<Wind> periodWinds = Winds.Where(_ => _.WindDate.Year == year).OrderBy(_ => _.WindDate).ToList();
                PeriodInformation periodInfo = new PeriodInformation(periodWinds);

                periodsInfos.Add(periodInfo);
            }

            return periodsInfos;
        }

        /// <summary>
        /// Метод для получения статистики
        /// </summary>
        private IList<WindChange> GetWindChanges()
        {
            int windsCount = Winds.Count() - 1;
            IList<WindChange> windChanges = new List<WindChange>(windsCount);
            for (int i = 0; i < windsCount; i++)
            {
                Wind currentWind = Winds.ElementAt(i);
                Wind nextWind = Winds.ElementAt(i + 1);
                WindChange windChange = new WindChange(currentWind, nextWind);

                windChanges.Add(windChange);
            }
            return windChanges;
        }

        /// <summary>
        /// Метод для преобразования списков дат и типов ветров в ветра
        /// </summary>
        /// <param name="dates">Список дат</param>
        /// <param name="winds">Список направлений ветров</param>
        /// <returns>Возвращает списко ветров</returns>
        private IList<Wind> GetWinds(List<DateTime> dates, List<string> winds)
        {
            //TODO: сделать проверку на равенство массивов
            int windCount = dates.Count;

            List<Wind> allWinds = new List<Wind>(windCount);
            for (int i = 0; i < windCount; i++)
            {
                DateTime windTime = dates[i];
                string windType = winds[i];

                allWinds.Add(new Wind(windTime, windType));
            }

            return allWinds;
        }

        /// <summary>
        /// Метод для получения информации о файле 
        /// </summary>
        /// <param name="infoRows">Список информационных строк</param>
        private void GetFileInfo(List<string> infoRows)
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

            Meteostation = firstInformationRow.OutputMatches[0];
            MeteostationCodeType = firstInformationRow.OutputMatches[1];
            MeteostationCode = firstInformationRow.OutputMatches[2];
            FirstWindDate = DateTime.Parse(firstInformationRow.OutputMatches[3]);
            LastWindDate = DateTime.Parse(firstInformationRow.OutputMatches[4]);
            SampleType = firstInformationRow.OutputMatches[5];

            InformationRow secondInformationRow = new InformationRow(
                "Кодировка: (?<...>.*)",
                secondRow,
                "encoding"
                );

            Encoding = secondInformationRow.OutputMatches[0];

            InformationRow thirdInformationRow = new InformationRow(
                "Информация предоставлена сайтом (?<...>.*)",
                thirdRow,
                "site"
                );

            Site = thirdInformationRow.OutputMatches[0];
        }

        private IList<WindType> GetWindTypes()
        {
            return Winds.Select(_ => _.WindType).Distinct().OrderBy(_=>(int)_).ToList();
        }
    }
}

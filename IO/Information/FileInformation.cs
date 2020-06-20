using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IO.Information
{
    public class FileInformation
    {
        public string StationName;
        public string CodeType;
        public string Code;
        public DateTime FirstMeasure;
        public DateTime LastMeasure;
        public int CountOfMeasures;
        public string MeasuresType;
        public string Encoding;
        public string Site;
        public Measure[] AllMeasures;
        public PeriodInformation[] PeriodsInformation;

        /// <summary>
        /// Инициализирует новый экземпляр класса FileInformation
        /// </summary>
        /// <param name="stationName">Наименование/Местоположение станции</param>
        /// <param name="codeType">Тип кода метеостанции</param>
        /// <param name="code">Код метеостанции</param>
        /// <param name="firstMeasure">Первое измерение за всё время</param>
        /// <param name="lastMeasure">Последнее измерение за всё время</param>
        /// <param name="measuresCount">Кол-во измерений за всё время</param>
        /// <param name="measuresType">Выборка</param>
        /// <param name="encoding">Кодировка</param>
        /// <param name="site">Сайт</param>
        public FileInformation(string stationName, 
            string codeType, 
            string code,
            string firstMeasure, 
            string lastMeasure, 
            int measuresCount, 
            string measuresType,
            string encoding, 
            string site)
        {
            StationName = stationName;
            CodeType = codeType;
            Code = code;
            FirstMeasure = DateTime.Parse(firstMeasure);
            LastMeasure = DateTime.Parse(lastMeasure);
            CountOfMeasures = measuresCount;
            MeasuresType = measuresType;
            Encoding = encoding;
            Site = site;
        }
    }
}

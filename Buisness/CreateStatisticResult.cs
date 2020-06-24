using Buisness.Information;
using Buisness.Winds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Buisness
{
    public class CreateStatisticResult
    {
        /// <summary>
        /// Успех результата
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Информация о файле
        /// </summary>
        public FileInformation FileInformation { get; }

        /// <summary>
        /// Список информации о периодах
        /// </summary>
        public IEnumerable<PeriodInformation> PeriodInformations { get; } = new List<PeriodInformation>();

        /// <summary>
        /// Инициализирует новый экземпляр класса CreateSTatisticResult, если не возникло исключения
        /// </summary>
        /// <param name="fileInfo">Информация о файле</param>
        /// <param name="periodInfos">Список информации о периоде</param>
        public CreateStatisticResult(FileInformation fileInfo, IEnumerable<PeriodInformation> periodInfos)
        {
            FileInformation = fileInfo;
            PeriodInformations = periodInfos;

            IsSuccessful = true;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса CreateSTatisticResult, если исключение возникло
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public CreateStatisticResult(string message)
        {
            ErrorMessage = message;
            IsSuccessful = false;
        }

        /// <summary>
        /// Метод для получения преобразованных данных
        /// </summary>
        /// <param name="dateTimes">Список дат</param>
        /// <param name="windTypes">Список типов ветров</param>
        /// <param name="informationRows">Список информационных строк</param>
        /// <returns>Рузельтат создания статистики</returns>
        public static CreateStatisticResult GetStatistic(List<DateTime> dateTimes, List<string> windTypes, params string[] informationRows)
        {
            try
            {
                FileInformation fileInformation = new FileInformation(dateTimes, windTypes, informationRows);
                IEnumerable<PeriodInformation> periodsInfo = fileInformation.GetPeriodsInformation();
                Wind.WindVariativeList = windTypes.Select(_ => _.MapToWind()).Distinct().OrderBy(_=>(int)_).ToList();
                Wind.SetWindVariativeCount();

                return new CreateStatisticResult(fileInformation, periodsInfo);
            }
            catch (Exception e)
            {
                return new CreateStatisticResult(e.Message);
            }
        }
    }
}

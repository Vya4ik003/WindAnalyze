using Buisness.Information;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buisness
{
    public class BLResult
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// Информация о файле
        /// </summary>
        public FileInformation InformationAboutFile { get; set; }
         
        /// <summary>
        /// Информация о периодах
        /// </summary>
        public PeriodInformation[] InformationAboutPeriods { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса BLResult, если не возникло исключения 
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="fileInformation">Информация о файле</param>
        /// <param name="periodsInformation">Информация о периоде</param>
        public BLResult(string message, FileInformation fileInformation, PeriodInformation[] periodsInformation)
        {
            ResultMessage = message;
            InformationAboutFile = fileInformation;
            InformationAboutPeriods = periodsInformation;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса BLResult, если bcrk.xtybt djpybrkj 
        /// </summary>
        /// <param name="message">Сообщение</param>
        public BLResult(string message)
        {
            ResultMessage = message;
        }

    }
}

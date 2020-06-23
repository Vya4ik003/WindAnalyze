using System;
using System.Collections.Generic;
using System.Text;

namespace IO
{
    public class FileInformation
    {
        public string FirstInformationRow { get; } // # Метеостанция Москва (ВДНХ), Россия, WMO_ID=27612, выборка с 01.06.2014 по 18.06.2020, все дни
        public string SecondInformationRow { get; } // # Кодировка: UTF-8
        public string ThirdInformationRow { get; } // # Информация предоставлена сайтом "Расписание Погоды", rp5.ru

        //Следующие строки не содержат важной информации
        //public string FourthInformationRow { get; } // # Пожалуйста, при использовании данных, любезно указывайте названный сайт.
        //public string FifthInformationRow { get; } // # Обозначения метеопараметров см. по адресу http://rp5.ru/archive.php?wmo_id=27612&lang=ru
        //public string SixthInformationRow { get; } // #

        public FileInformation(params string[] infoRows)
        {
            FirstInformationRow = infoRows[0];
            SecondInformationRow = infoRows[1];
            ThirdInformationRow = infoRows[2];
        }
    }
}

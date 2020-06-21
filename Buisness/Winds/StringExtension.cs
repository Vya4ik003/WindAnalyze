using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buisness.Winds
{
    static class StringExtension
    {
        public static WindType ParseToWind(this string dd)
        {
            switch (dd.ToLower())
            {
                case "Ветер, дующий с севера": return WindType.С;
                case "Ветер, дующий с северо-северо-востока": return WindType.ССВ;
                case "Ветер, дующий с северо-востока": return WindType.СВ;
                case "Ветер, дующий с востоко-северо-востока": return WindType.ВСВ;
                case "Ветер, дующий с востока": return WindType.В;
                case "Ветер, дующий с востоко-юго-востока": return WindType.ВЮВ;
                case "Ветер, дующий с юго-востока": return WindType.ЮВ;
                case "Ветер, дующий с юго-юго-востока": return WindType.ЮЮВ;
                case "Ветер, дующий с юга": return WindType.Ю;
                case "Ветер, дующий с юго-юго-запада": return WindType.ЮЮЗ;
                case "Ветер, дующий с юго-запада": return WindType.ЮЗ;
                case "Ветер, дующий с западо-юго-запада": return WindType.ЗЮЗ;
                case "Ветер, дующий с запада": return WindType.З;
                case "Ветер, дующий с западо-северо-запада": return WindType.ЗСЗ;
                case "Ветер, дующий с северо-запада": return WindType.СЗ;
                case "Ветер, дующий с северо-северо-запада": return WindType.ССЗ;
                case "Штиль, безветрие": return WindType.Ш;
                case "Переменное направление": return WindType.ПН;
                default: return WindType.Unknown;
            }
        }
    }
}

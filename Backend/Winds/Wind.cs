using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buisness.Winds
{
    public struct Wind
    {
        public static IList<WindType> WindVariativeList = new List<WindType>();

        /// <summary>
        /// Время измерения ветра
        /// </summary>
        public DateTime WindDate { get; }

        /// <summary>
        /// Направление ветра
        /// </summary>
        public WindType WindType { get; }

        public Wind(DateTime windDate, string windType)
        {
            WindDate = windDate;
            WindType = windType.MapToWind();
        }

        public override string ToString()
        {
            return WindType.ToString();
        }
    }

    internal static class StringExtensions
    {
        /// <summary>
        /// Метод для парсинга из string в Wind
        /// </summary>
        /// <param name="dd">Текст из столбца DD</param>
        /// <returns>Возвращает тип ветра</returns>
        public static WindType MapToWind(this string dd)
        {
            switch (dd.ToLower())
            {
                case "ветер, дующий с севера":
                    return WindType.С;
                case "ветер, дующий с северо-северо-востока":
                    return WindType.ССВ;
                case "ветер, дующий с северо-востока":
                    return WindType.СВ;
                case "ветер, дующий с востоко-северо-востока":
                    return WindType.ВСВ;
                case "ветер, дующий с востока":
                    return WindType.В;
                case "ветер, дующий с востоко-юго-востока":
                    return WindType.ВЮВ;
                case "ветер, дующий с юго-востока":
                    return WindType.ЮВ;
                case "ветер, дующий с юго-юго-востока":
                    return WindType.ЮЮВ;
                case "ветер, дующий с юга":
                    return WindType.Ю;
                case "ветер, дующий с юго-юго-запада":
                    return WindType.ЮЮВ;
                case "ветер, дующий с юго-запада":
                    return WindType.ЮЗ;
                case "ветер, дующий с западо-юго-запада":
                    return WindType.ЗЮЗ;
                case "ветер, дующий с запада":
                    return WindType.З;
                case "ветер, дующий с западо-северо-запада":
                    return WindType.ЗСЗ;
                case "ветер, дующий с северо-запада":
                    return WindType.СЗ;
                case "ветер, дующий с северо-северо-запада":
                    return WindType.ССЗ;
                case "штиль, безветрие":
                    return WindType.Ш;
                case "переменное направление":
                    return WindType.ПН;
                default:
                    return WindType.Unknown;
            }
        }
    }

    public enum WindType
    {
        Unknown,
        С,
        ССВ,
        СВ,
        ВСВ,
        В,
        ВЮВ,
        ЮВ,
        ЮЮВ,
        Ю,
        ЮЮЗ,
        ЮЗ,
        ЗЮЗ,
        З,
        ЗСЗ,
        СЗ,
        ССЗ,
        Ш,
        ПН,
    }
}

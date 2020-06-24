using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buisness.Winds
{
    public struct Wind
    {
        //TODO: Проверить свойство на необходимость
        public static int WindVariativeCount { get; private set; }

        public static void SetWindVariativeCount()
        {
            WindVariativeCount = WindVariativeList.Count();
        }
        public static IEnumerable<WindType> WindVariativeList = new List<WindType>();

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
            return (dd.ToLower()) switch
            {
                "ветер, дующий с севера" => WindType.С,
                "ветер, дующий с северо-северо-востока" => WindType.ССВ,
                "ветер, дующий с северо-востока" => WindType.СВ,
                "ветер, дующий с востоко-северо-востока" => WindType.ВСВ,
                "ветер, дующий с востока" => WindType.В,
                "ветер, дующий с востоко-юго-востока" => WindType.ВЮВ,
                "ветер, дующий с юго-востока" => WindType.ЮВ,
                "ветер, дующий с юго-юго-востока" => WindType.ЮЮВ,
                "ветер, дующий с юга" => WindType.Ю,
                "ветер, дующий с юго-юго-запада" => WindType.ЮЮЗ,
                "ветер, дующий с юго-запада" => WindType.ЮЗ,
                "ветер, дующий с западо-юго-запада" => WindType.ЗЮЗ,
                "ветер, дующий с запада" => WindType.З,
                "ветер, дующий с западо-северо-запада" => WindType.ЗСЗ,
                "ветер, дующий с северо-запада" => WindType.СЗ,
                "ветер, дующий с северо-северо-запада" => WindType.ССЗ,
                "штиль, безветрие" => WindType.Ш,
                "переменное направление" => WindType.ПН,
                _ => WindType.Unknown,
            };
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

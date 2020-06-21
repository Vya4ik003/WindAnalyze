using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Winds
{
    public struct WindChange
    {
        //TODO: Проверить на правильность слов "Предыдущий" и "Нынешний"
        /// <summary>
        /// Предыдущий ветер
        /// </summary>
        public Wind WindBefore { get; set; }

        /// <summary>
        /// Нынешний ветер
        /// </summary>
        public Wind WindAfter { get; set; }
    }
}

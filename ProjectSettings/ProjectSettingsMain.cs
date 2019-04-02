using System.Collections.Generic;
using System.Drawing;

namespace ProjectSettings
{
    /// <summary>
    /// Класс настроек проекта
    /// </summary>
    public static class ProjectSettingsMain
    {
        /// <summary>
        /// Цвет квадратных разделителей
        /// </summary>
        static public Color ColorSquares { get; set; } = Color.Gray;

        /// <summary>
        /// Цвет цифр
        /// </summary>
        static public Color ColorNumbers { get; set; } = Color.Gray;

        /// <summary>
        /// Спосок с основными загруженными командами
        /// </summary>
        static public List<object> ArrayCommands = new List<object>();

        //Переменные для зоны отрисовки 

        /// <summary>
        /// Переменна отвечающая за выбор режима отрисовки
        /// </summary>
        static public int Zone_DrawMethod = 0;

        /// <summary>
        /// Переменные отвечающая за то что нужно ли удалить старую зону и нарисовать новую
        /// </summary>
        static public bool Zone_TracingChangeDraw = false;

        /// <summary>
        /// Переменные отвечающая отключение распознание речи пока активна работа с новыми областями
        /// </summary>
        static public bool Zone_FlagVoiceControl = true;

        /// <summary>
        /// Переменная хранащая в себе список новых зон для отрисовки
        /// </summary>
        static public List<RegionRectangle> Zone_ListRegionRectangle = new List<RegionRectangle>();
    }
}

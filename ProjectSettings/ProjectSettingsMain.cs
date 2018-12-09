using System.Collections.Generic;
using System.Drawing;

namespace ProjectSettings
{
    public static class ProjectSettingsMain
    {
        //Цвет квадратных разделителей
        static public Color ColorSquares { get; set; } = Color.Gray;

        //Цвет цифр
        static public Color ColorNumbers { get; set; } = Color.Gray;

        //Спосок с основными загруженными командами
        static public List<object> ArrayCommands = new List<object>();



        //Переменные для зоны отрисовки 

        //Переменна отвечающая за выбор режимо отрисовки
        static public int Zone_DrawMethod = 0;

        //Переменные отвечающая за то что нужно ли удалить старую зону и нарисовать новую
        static public bool Zone_TracingChangeDraw = false;

        //Переменная хранащая в себе список новых зон для отрисовки
        static public List<RegionRectangle> Zone_ListRegionRectangle = new List<RegionRectangle>();
    }
}

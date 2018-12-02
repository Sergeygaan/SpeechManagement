using System.Collections.Generic;
using System.Drawing;

namespace ProjectSettings
{
    public static class ProjectSettingsStyle
    {
        //Цвет квадратных разделителей
        static public Color ColorSquares { get; set; } = Color.Gray;

        //Цвет цифр
        static public Color ColorNumbers { get; set; } = Color.Gray;

        //Спосок с основными загруженными командами
        static public List<object> ArrayCommands = new List<object>();
    }
}

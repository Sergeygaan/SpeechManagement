using ProjectSettings;
using System.Collections.Generic;
using System.Drawing;

namespace WorkingScreen
{
    public static class Drawing
    {
        /// <summary>
        /// Метод для отрисовке классической области
        /// </summary>
        /// <param name="_currentObject"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public static void MethodMain(WorkObject _currentObject, float width, float height, float startX = 0, float startY = 0)
        {
            float saveStartX = startX;

            float stepWidth = width / 3;
            float stepHeight = height / 3;

            float endX = stepWidth + startX;
            float endY = stepHeight + startY;

            int id = 0;

            for (int stepX = 0; stepX < 3; stepX++)
            {
                for (int stepY = 0; stepY < 3; stepY++)
                {
                    id += 1;

                    RegionRectangle createRegionRectangle = CreateRegionRectangle(id, startX, startY, endX, endY);

                    _currentObject.listRegionRectangle.Add(createRegionRectangle);

                    createRegionRectangle = null;

                    startX += stepWidth;
                    endX += stepWidth;
                }

                startX = saveStartX;
                endX = stepWidth + saveStartX;

                startY += stepHeight;
                endY += stepHeight;
            }
        }

        /// <summary>
        /// Метод по созданию новых зон для мыши "Классический"
        /// </summary>
        /// <param name="iDObject"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endtX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        private static RegionRectangle CreateRegionRectangle(int iDObject, float startX, float startY, float endtX, float endY)
        {
            var width = endtX - startX;
            var height = endY - startY;

            var Rectangle = new RectangleF(startX, startY, width, height);

            RegionRectangle regionRectangle = new RegionRectangle
            {
                Rectangle = new RectangleF(startX, startY, width, height),

                IdObject = iDObject,

                Visible = true,

                StartX = startX,
                StartY = startY,

                Width = width,
                Height = height
            };

            return regionRectangle;
        }

        //Метод по созданию новых зон для мыши
        public static void MethodAlternative(WorkObject _currentObject, List<RegionRectangle> regionRectangles)
        {
            foreach (var currentRegionRectangl in regionRectangles)
            {
                _currentObject.listRegionRectangle.Add(currentRegionRectangl);
            }
        }
    }
}

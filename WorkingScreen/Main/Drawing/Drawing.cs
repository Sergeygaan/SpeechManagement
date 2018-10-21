﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceControl;
using System.Drawing;
using static VoiceControl.WorkObject;

namespace Main.Drawing
{
    public class Drawing
    {
        //Метод по созданию новых зон для мыши
        public void DrawingDividingLines(WorkObject _currentObject, float width, float height, float startX = 0, float startY = 0)
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

                    _currentObject.listRegionRectangle.Add(CreateRegionRectangle(id, startX, startY, endX, endY));

                    startX += stepWidth;
                    endX += stepWidth;
                }

                startX = saveStartX;
                endX = stepWidth + saveStartX;

                startY += stepHeight;
                endY += stepHeight;
            }
        }

        private RegionRectangle CreateRegionRectangle(int iDObject, float startX, float startY, float endtX, float endY)
        {
            var width = endtX - startX;
            var height = endY - startY;

            var Rectangle = new RectangleF(startX, startY, width, height);

            RegionRectangle regionRectangle = new RegionRectangle();

            regionRectangle.Rectangle = new RectangleF(startX, startY, width, height);

            regionRectangle.IdObject = iDObject;

            regionRectangle.Visible = true;

            regionRectangle.StartX = startX;
            regionRectangle.StartY = startY;

            regionRectangle.Width = width;
            regionRectangle.Height = height;

            return regionRectangle;
        }
    }
}
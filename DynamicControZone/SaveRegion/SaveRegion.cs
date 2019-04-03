using ProjectSettings;
using System.Collections.Generic;
using System.Drawing;

namespace MyPaint
{
    /// <summary>
    /// Класс для сохранения нарисованных альтернативных областей
    /// </summary>
    public class SaveRegion
    {
        /// <summary>
        /// Метод сохранения объектов
        /// </summary>
        /// <param name="FiguresList"></param>
        public void Save(List<ObjectFugure> FiguresList)
        {
            ProjectSettingsMain.Zone_NewList.Clear();

            foreach (var currentFiguresList in FiguresList)
            {
                if (currentFiguresList.Path.PointCount != 0)
                {
                    RectangleF rectangleF = СonstructionFigure.ShowRectangleFloat(currentFiguresList.Path.PathPoints[0], currentFiguresList.Path.PathPoints[2]);

                    if ((rectangleF.Width < 20) || (rectangleF.Height < 20))
                    {
                        break;
                    }

                    RegionRectangle regionRectangle = new RegionRectangle
                    {
                        Rectangle = rectangleF,
                        IdObject = currentFiguresList.IdFigure,
                        Visible = true,
                        StartX = currentFiguresList.Path.PathPoints[0].X,
                        StartY = currentFiguresList.Path.PathPoints[0].Y,
                        EndX = currentFiguresList.Path.PathPoints[2].X,
                        EndY = currentFiguresList.Path.PathPoints[2].Y,

                        Width = rectangleF.Width,
                        Height = rectangleF.Height
                    };

                    ProjectSettingsMain.Zone_NewList.Add(regionRectangle);

                    regionRectangle = null;
                }
            }
        }
    }
}

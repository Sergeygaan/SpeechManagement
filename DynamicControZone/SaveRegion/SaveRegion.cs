using ProjectSettings;
using System.Collections.Generic;
using System.Drawing;

namespace MyPaint
{
    public class SaveRegion
    {
        public void Save(List<ObjectFugure> FiguresList)
        {
            ProjectSettingsMain.ListRegionRectangle.Clear();

            foreach (var currentFiguresList in FiguresList)
            {
                if (currentFiguresList.Path.PointCount != 0)
                {
                    RectangleF rectangleF = СonstructionFigure.ShowRectangleFloat(currentFiguresList.Path.PathPoints[0], currentFiguresList.Path.PathPoints[2]);

                    RegionRectangle regionRectangle = new RegionRectangle
                    {
                        Rectangle = rectangleF,
                        IdObject = currentFiguresList.IdFigure,
                        Visible = true,
                        StartX = currentFiguresList.Path.PathPoints[0].X,
                        StartY = currentFiguresList.Path.PathPoints[0].Y,

                        Width = rectangleF.Width,
                        Height = rectangleF.Height
                    };

                    ProjectSettingsMain.ListRegionRectangle.Add(regionRectangle);
                }
            } 
        }
    }
}

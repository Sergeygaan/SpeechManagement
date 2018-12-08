using System.Drawing;

namespace ProjectSettings
{
    public class RegionRectangle
    {
        public RectangleF Rectangle { set; get; }

        public int IdObject { set; get; }

        public bool Visible { set; get; }

        public float StartX { set; get; }
        public float StartY { set; get; }

        public float Width { set; get; }
        public float Height { set; get; }
    }
}

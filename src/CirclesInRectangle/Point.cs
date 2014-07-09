using System.Drawing;

namespace CirclesInRectangle
{
    public class Point
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public Point(double centerX, double centerY)
        {
            CenterX = centerX;
            CenterY = centerY;
        }
    }
}

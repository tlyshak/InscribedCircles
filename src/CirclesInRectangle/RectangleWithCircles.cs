using System;
using System.Collections.Generic;

namespace CirclesInRectangle
{
    public class RectangleWithCircles
    {
        private readonly double _width;
        private readonly double _height;
        private IList<Point> _points;

        public RectangleWithCircles(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public IEnumerable<Point> GetHexagonsCountFromRectangle(double circleRadius, double gap)
        {
            _points = new List<Point>();
            double currentWidth = gap;
            double currentY = gap + circleRadius;
            for (var i = 0;; i++)
            {
                while (true)
                {
                    var currentCenterX = currentWidth + circleRadius;
                    currentWidth = currentCenterX + circleRadius + gap;
                    if (currentWidth > _width) break;
                    _points.Add(new Point(currentCenterX, currentY));
                }
                var newCircleCenter = GetNewCircleCenter(currentY, circleRadius, gap, Convert.ToBoolean(i%2));
                if (newCircleCenter == null) break;
                currentWidth = newCircleCenter.CenterX;
                currentY = newCircleCenter.CenterY;
            }
            return _points;
        }

        private Point GetNewCircleCenter(double currentHeight, double circleRadius, double gap, bool isPairedRow)
        {
            double centerY = currentHeight + (circleRadius/2) + (2/Math.Sqrt(3)*circleRadius) + gap;
            double centerX = isPairedRow ? gap : (1.5*gap + circleRadius);
            return (centerY + circleRadius) <= _height ? new Point(centerX, centerY) : null;
        }
    }
}
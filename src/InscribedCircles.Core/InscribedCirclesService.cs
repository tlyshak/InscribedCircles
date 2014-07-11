using System;
using System.Collections.Generic;

namespace InscribedCircles.Core
{
    public class InscribedCirclesService
    {
        public IEnumerable<Point> GetCirclesCenters(double rectangleWidth, double rectangleHeight, double circleRadius, double gap)
        {
            var points = new List<Point>();
            double currentWidth = gap;
            double currentY = gap + circleRadius;
            for (var i = 0;; i++)
            {
                while (true)
                {
                    var currentCenterX = currentWidth + circleRadius;
                    currentWidth = currentCenterX + circleRadius + gap;
                    if (currentWidth > rectangleWidth) break;
                    points.Add(new Point(currentCenterX, currentY));
                }
                var newCircleCenter = GetNewCircleCenter(rectangleHeight, currentY, circleRadius, gap, Convert.ToBoolean(i % 2));
                if (newCircleCenter == null) break;
                currentWidth = newCircleCenter.CenterX;
                currentY = newCircleCenter.CenterY;
            }
            return points;
        }

        private Point GetNewCircleCenter(double rectangleHeight, double currentHeight, double circleRadius, double gap, bool isPairedRow)
        {
            double centerY = currentHeight + 1.5*(2/Math.Sqrt(3)*circleRadius) + gap;
            double centerX = isPairedRow ? gap : (1.5*gap + circleRadius);
            return (centerY + circleRadius + gap) <= rectangleHeight ? new Point(centerX, centerY) : null;
        }
    }
}
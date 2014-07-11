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
            bool isPairedRow;
            double secondRowWidth = 0;
            int rowCount = 0;
            for (; ; rowCount++)
            {
                isPairedRow = Convert.ToBoolean(rowCount % 2);
                while (true)
                {
                    var currentCenterX = currentWidth + circleRadius;
                    currentWidth = currentCenterX + circleRadius + gap;
                    if (currentWidth > rectangleWidth) break;
                    points.Add(new Point(currentCenterX, currentY));
                }
                if (rowCount == 1)
                    secondRowWidth = currentWidth;
                var newCircleCenter = GetNewCircleCenter(rectangleHeight, currentY, circleRadius, gap, isPairedRow);
                if (newCircleCenter == null) break;
                currentWidth = newCircleCenter.CenterX;
                currentY = newCircleCenter.CenterY;
            }
            var fullHeight = currentY + circleRadius + gap;
            var apparentWidth = rectangleWidth + (rectangleHeight - fullHeight)/++rowCount;
            if (apparentWidth - secondRowWidth > 0.1) // more than 0.1(mm) gap
            {
                var fitGap = apparentWidth - secondRowWidth;
                points = new List<Point>();
                currentWidth = gap;
                currentY = gap + circleRadius;
                for (int i = 0; ; i++)
                {
                    isPairedRow = Convert.ToBoolean(i % 2);
                    while (true)
                    {
                        var currentCenterX = currentWidth + circleRadius;
                        currentWidth = currentCenterX + circleRadius + gap;
                        if (currentWidth > rectangleWidth) break;
                        points.Add(new Point(currentCenterX, currentY));
                    }
                    var newCircleCenter = GetNewCircleCenter(rectangleHeight, currentY, circleRadius, gap, isPairedRow, isPairedRow ? 0 : fitGap);
                    if (newCircleCenter == null) break;
                    currentWidth = newCircleCenter.CenterX;
                    currentY = newCircleCenter.CenterY;
                }
            }
            return points;
        }

        private Point GetNewCircleCenter(double rectangleHeight, double currentHeight, double circleRadius, double gap, bool isPairedRow, double fitGap = 0)
        {
            double centerY = currentHeight + 1.5*(2/Math.Sqrt(3)*circleRadius) + gap + fitGap;
            double centerX = isPairedRow ? gap : (1.5*gap + circleRadius) - fitGap;
            return (centerY + circleRadius + gap) <= rectangleHeight ? new Point(centerX, centerY) : null;
        }
    }
}
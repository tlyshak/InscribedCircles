using System;
using System.Collections.Generic;
using System.Linq;

namespace InscribedCircles.Core
{
    public class InscribedCirclesService
    {
        private double GetMaxCircleRowsDifference(double circleRadius, double gap)
        {
            var bigRadius = 2 / Math.Sqrt(3) * circleRadius;
            var rowsCentersDistance = 1.5 * bigRadius + gap;
            return 2 * circleRadius - rowsCentersDistance + gap;
        }

        private int GetCircleRowsCount(double rectangleHeight, double circleRadius, double gap, double circleRowsDifference = 0)
        {
            var defaultBusyWidth = gap;
            int rowsCount = 0;
            while (defaultBusyWidth <= rectangleHeight)
            {
                defaultBusyWidth += 2 * circleRadius + gap - (Convert.ToBoolean(rowsCount % 2) ? 0 : circleRowsDifference);
                rowsCount++;
            }
            return --rowsCount;
        }

        private double FindMargin(double rectangleHeight, double circleRadius, double gap)
        {
            var maxCircleRowsDifference = GetMaxCircleRowsDifference(circleRadius, gap);
            var maxCircleRows = GetCircleRowsCount(rectangleHeight, circleRadius, gap, maxCircleRowsDifference);
            var minCircleRows = GetCircleRowsCount(rectangleHeight, circleRadius, gap);

            return maxCircleRows == minCircleRows ? maxCircleRowsDifference : 0;

            var diffCollection = new List<double>(100);
            var minDiff = maxCircleRowsDifference/100;
            for (double i = 0; i < 100;)
            {
                i += minDiff;
                diffCollection.Add(i);
            }
            var currentRows = 0;
            double currentDiff = 0;
            for (int i = 0; currentRows != maxCircleRows; i++)
            {
                currentDiff = diffCollection[i];
                currentRows = GetCircleRowsCount(rectangleHeight, circleRadius, gap, currentDiff);
            }
            return currentDiff;
        }
        public IEnumerable<Point> GetCirclesCenters(double rectangleWidth, double rectangleHeight, double circleRadius, double gap)
        {
            if ((2*gap + 2*circleRadius) > rectangleHeight) return Enumerable.Empty<Point>();
            var offsetY = FindMargin(rectangleHeight, circleRadius, gap);
            var offsetPercent = offsetY/GetMaxCircleRowsDifference(circleRadius, gap);
            var offsetX = circleRadius*offsetPercent + 0.5*gap;
            double currentWidth = gap;
            double currentY = gap + circleRadius;
            bool isPairedRowNext;
            int rowCount = 0;
            var points = new List<Point>();
            for (; ; rowCount++)
            {
                isPairedRowNext = !Convert.ToBoolean(rowCount % 2);
                while (true)
                {
                    var currentCenterX = currentWidth + circleRadius;
                    currentWidth = currentCenterX + circleRadius + gap;
                    if (currentWidth > rectangleWidth) break;
                    points.Add(new Point(currentCenterX, currentY));
                }
                var newCircleCenter = GetNewCircleCenter(rectangleHeight, currentY, circleRadius, gap,
                    isPairedRowNext, isPairedRowNext ? offsetX : 0, offsetY);
                if (newCircleCenter == null) break;
                currentWidth = newCircleCenter.X;
                currentY = newCircleCenter.Y;
            }
            return points;
        }

        private Point GetNewCircleCenter(double rectangleHeight, double currentHeight, double circleRadius, double gap,
            bool isPairedRowNext, double offsetX = 0, double offsetY = 0)
        {
            double centerY = currentHeight + 1.5*(2/Math.Sqrt(3)*circleRadius) + 1.2*gap + offsetY;
            double centerX = isPairedRowNext ? (1.5 * gap + circleRadius) - offsetX : gap;
            return (centerY + circleRadius + gap) <= rectangleHeight ? new Point(centerX, centerY) : null;
        }
    }
}
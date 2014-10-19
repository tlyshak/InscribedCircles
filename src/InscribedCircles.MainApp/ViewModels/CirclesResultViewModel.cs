using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.ViewModels;
using InscribedCircles.MainApp.Windows;
using Microsoft.Expression.Interactivity.Layout;
using Point = InscribedCircles.Core.Point;

namespace InscribedCircles.MainApp.ViewModels
{
    public class CirclesResultViewModel : ViewModel, ICirclesResultViewModel
    {
        #region Fields
        private IEnumerable<Point> _points;
        private IList<Point> _movingHistory;
        private readonly Random _random;
        private double _mouseOffsetX;
        private double _mouseOffsetY;
        private bool _isBlocked;
        private bool _isCrossed;
        private bool _isCircleSelected;
        private Canvas _circlesCanvas = new Canvas { Background = new SolidColorBrush(Color.FromRgb(209, 247, 209)) };
        private double _positionX;
        private double _positionY;
        private double _rectangleWidth;
        private double _circleRadius;
        private double _rectangleHeight;
        private double _minimalGap;
        private ICommand _showCoordinatesCommand;

        #endregion

        #region Properties
        public Canvas CirclesCanvas
        {
            get { return _circlesCanvas; }
            set
            {
                if (Equals(_circlesCanvas, value)) return;
                _circlesCanvas = value;
                RaisePropertyChanged(() => CirclesCanvas);
            }
        }

        public double PositionX
        {
            get { return _positionX; }
            set
            {
                _positionX = value;
                RaisePropertyChanged(() => PositionX);
            }
        }

        public double PositionY
        {
            get { return _positionY; }
            set
            {
                _positionY = value;
                RaisePropertyChanged(() => PositionY);
            }
        }
        public double CircleRadius
        {
            get { return _circleRadius; }
            set
            {
                if (Equals(_circleRadius, value)) return;
                _circleRadius = value;
                RaisePropertyChanged(() => CircleRadius);
            }
        }
        public double RectangleWidth
        {
            get { return _rectangleWidth; }
            set
            {
                if (Equals(_rectangleWidth, value)) return;
                _rectangleWidth = value;
                RaisePropertyChanged(() => RectangleWidth);
            }
        }

        public double RectangleHeight
        {
            get { return _rectangleHeight; }
            set
            {
                if (Equals(_rectangleHeight, value)) return;
                _rectangleHeight = value;
                RaisePropertyChanged(() => RectangleHeight);
            }
        }

        public double MinimalGap
        {
            get { return _minimalGap; }
            set
            {
                if (Equals(_minimalGap, value)) return;
                _minimalGap = value;
                RaisePropertyChanged(() => MinimalGap);
            }
        } 
        #endregion

        public ICommand ShowCoordinatesCommand
        {
            get { return _showCoordinatesCommand ?? (_showCoordinatesCommand = new RelayCommand(ShowCoordinates)); }
        }

        public CirclesResultViewModel(IAddCircleViewModel addCircleViewModel, ICalculateParametersViewModel calculateParametersViewModel)
        {
            _random = new Random();
            addCircleViewModel.AddNewCircleEvent += AddNewCircle;
            calculateParametersViewModel.BuildCanvasEvent += BuildCanvas;
        }

        private void BuildCanvas(object sender, EventArgs e)
        {
            var viewModel = sender as CalculateParametersViewModel;
            if (viewModel == null) return;
            BuildCirclesArea(viewModel.CalcCirclesAutomatically, viewModel.RectangleHeight, viewModel.RectangleWidth, 
                viewModel.CircleRadius, viewModel.MinimalGap, viewModel.Points);
        }

        private void AddNewCircle(object sender, EventArgs e)
        {
            var addCircleViewModel = sender as AddCircleViewModel;
            if (addCircleViewModel == null) return;
            var point = FindFreeSpaceForCircle(addCircleViewModel.NewCircleRadius);
            if (point == null)
            {
                MessageBox.Show("Немає вільного місця щоб вставити коло.");
                return;
            }
            AddCircle(point.X, point.Y, addCircleViewModel.NewCircleRadius);
        }

        private void ShowCoordinates()
        {
            var coordinatesWindow = new CoordinatesCirclesWindow();
            coordinatesWindow.ShowDialog();
        }

        private Point FindFreeSpaceForCircle(double circleRadius)
        {
            for (var i = MinimalGap; i < RectangleWidth; i++)
            {
                for (var j = MinimalGap; j < RectangleHeight; j++)
                {
                    bool isCrosed = false;
                    foreach (Ellipse circle in CirclesCanvas.Children)
                    {
                        if (CheckCirclesCrossing(i, j, Canvas.GetLeft(circle), Canvas.GetTop(circle), circleRadius))
                            isCrosed = true;
                    }
                    if (!isCrosed && (i + 2 * (circleRadius + MinimalGap)) < RectangleWidth &&
                        (j + 2 * (circleRadius + MinimalGap)) < RectangleHeight) return new Point(i, j);
                }
            }
            return null;
        }

        private void BuildCirclesArea(bool generateCircles, double canvasHeight, double canvasWidth, double circleRadius, 
            double minimalGap, IEnumerable<Point> points)
        {
            ClearCircles();
            RectangleHeight = canvasHeight;
            RectangleWidth = canvasWidth;

            if (generateCircles)
            {
                CircleRadius = circleRadius;
                MinimalGap = minimalGap;
                foreach (var point in points)
                    AddCircle(point.X - CircleRadius, point.Y - CircleRadius, CircleRadius); 
            }
        }

        private void ClearCircles()
        {
            CirclesCanvas.Children.Clear();
        }

        private void AddCircle(double offsetX, double offsetY, double circleRadius)
        {
            var ellipse = new Ellipse
            {
                Height = circleRadius * 2,
                Width = circleRadius * 2,
                Fill = new SolidColorBrush(Color.FromRgb((byte)_random.Next(50, 100), (byte)_random.Next(50, 100),
                        (byte)_random.Next(50, 100)))
            };
            var dragBehavior = new MouseDragElementBehavior { ConstrainToParentBounds = true };
            dragBehavior.Attach(ellipse);
            ellipse.MouseLeftButtonDown += ellipse_MouseLeftButtonDown;
            ellipse.MouseMove += ellipse_MouseMove;
            ellipse.MouseLeftButtonUp += ellipse_MouseLeftButtonUp;
            Canvas.SetLeft(ellipse, offsetX);
            Canvas.SetTop(ellipse, offsetY);
            CirclesCanvas.Children.Add(ellipse);
        }

        private void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isCircleSelected = true;
            var ellipse = (Ellipse)sender;
            var mousePositionCanvas = e.GetPosition(CirclesCanvas);
            _mouseOffsetX = mousePositionCanvas.X - Canvas.GetLeft(ellipse);
            _mouseOffsetY = mousePositionCanvas.Y - Canvas.GetTop(ellipse);
            _movingHistory = new List<Point> { new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse)) };
        }

        private void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isCircleSelected = false;
            _isBlocked = false;
        }

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(CirclesCanvas);
            var x = mousePosition.X - _mouseOffsetX;
            var y = mousePosition.Y - _mouseOffsetY;
            /*PositionX = x;
            PositionY = y;*/
            if (_isCircleSelected)
            {
                var ellipse = (Ellipse)sender;
                var crossedCircle = GetCrossedCircle(ellipse);

                _isCrossed = crossedCircle != null;
                if (_isBlocked)
                {
                    //IsBlocked = false;
                    e.Handled = true;
                }
                else if (_isCrossed)
                {
                    Point point = GetLastPoint(crossedCircle);
                    if (point != null && !_isBlocked)
                    {
                        _isBlocked = true;
                        Canvas.SetLeft(ellipse, point.X);
                        Canvas.SetTop(ellipse, point.Y);
                        PositionX = Canvas.GetLeft(ellipse);
                        PositionY = Canvas.GetTop(ellipse);
                    }
                }
                else
                {
                    if (x >= MinimalGap && x <= (RectangleWidth - (CircleRadius * 2 + MinimalGap)) &&
                        y >= MinimalGap && y <= (RectangleHeight - (CircleRadius * 2 + MinimalGap)))
                    {
                        _movingHistory.Add(new Point(x, y));
                        Canvas.SetLeft(ellipse, x);
                        Canvas.SetTop(ellipse, y);
                        PositionX = x;
                        PositionY = y;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private Ellipse GetCrossedCircle(Ellipse ellipse)
        {
            foreach (Ellipse circle in CirclesCanvas.Children)
            {
                if (!Equals(circle, ellipse))
                {
                    if (CheckCirclesCrossing(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse),
                        Canvas.GetLeft(circle), Canvas.GetTop(circle), CircleRadius)) return circle;
                }
            }
            return null;
        }

        private Point GetLastPoint(Ellipse crossedCircle)
        {
            foreach (var source in _movingHistory.Reverse())
            {
                if (!CheckCirclesCrossing(Canvas.GetLeft(crossedCircle), Canvas.GetTop(crossedCircle),
                    source.X, source.Y, CircleRadius))
                {
                    return source;
                }
            }
            return null;
        }

        private bool CheckCirclesCrossing(double x1, double y1, double x2, double y2, double r)
        {
            var result = Math.Sqrt((x1 - x2)*(x1 - x2) + (y1 - y2)*(y1 - y2));
            return result < (r + r + MinimalGap);
        }
    }
}

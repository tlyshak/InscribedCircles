using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.Windows;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace InscribedCircles.MainApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Fields

        /*private int _maxCircles = 1000;
        private ICommand _showCoordinatesCommand;
        private double _circleRadius;
        private double _rectangleWidth;
        private double _rectangleHeight;
        private double _minimalGap;*/
        private ObservableCollection<RadMenuItem> _menuItems = new ObservableCollection<RadMenuItem>();
        /*private Canvas _circlesCanvas = new Canvas {Background = new SolidColorBrush(Colors.LightSteelBlue)};
        private ICommand _addNewCircleCommand;
        private readonly Random _random;
        private int _circlesCount;
        private double _newCircleRadius;
        private double _newCircleMaxRadius;
        private ICommand _clearCirclesCommand;
        private ObservableCollection<Point> _points = new ObservableCollection<Point>();
        private bool _isCircleSelected;
        private IList<Point> _movingHistory;
        private double _mouseOffsetX;
        private double _mouseOffsetY;
        private double _newCircleLeft;
        private double _newCircleTop;
        private bool _isBlocked;
        private bool _isCrossed;
        private double _positionX;
        private double _positionY;
        private bool _calcCirclesAutomatically;*/
        private ViewModel _calculateParametersContentViewModel;
        private ViewModel _addCircleContentViewModel;
        private ViewModel _circlesResultContentViewModel;

        #endregion

        #region Properties

        public ViewModel CalculateParametersContentViewModel
        {
            get { return _calculateParametersContentViewModel; }
            set
            {
                if (Equals(_calculateParametersContentViewModel, value)) return;
                _calculateParametersContentViewModel = value;
                RaisePropertyChanged(() => CalculateParametersContentViewModel);
            }
        }

        public ViewModel AddCircleContentViewModel
        {
            get { return _addCircleContentViewModel; }
            set
            {
                if (Equals(_addCircleContentViewModel, value)) return;
                _addCircleContentViewModel = value;
                RaisePropertyChanged(() => AddCircleContentViewModel);
            }
        }

        public ViewModel CirclesResultContentViewModel
        {
            get { return _circlesResultContentViewModel; }
            set
            {
                if (Equals(_circlesResultContentViewModel, value)) return;
                _circlesResultContentViewModel = value;
                RaisePropertyChanged(() => CirclesResultContentViewModel);
            }
        }

        /*public double PositionX
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

        public bool IsCrossed
        {
            get { return _isCrossed; }
            set
            {
                _isCrossed = value;
                RaisePropertyChanged(() => IsCrossed);
            }
        }

        public bool IsBlocked
        {
            get { return _isBlocked; }
            set
            {
                _isBlocked = value;
                RaisePropertyChanged(() => IsBlocked);
            }
        }

        public double NewCircleLeft
        {
            get { return _newCircleLeft; }
            set
            {
                _newCircleLeft = value; 
                RaisePropertyChanged(() => NewCircleLeft);
            }
        }

        public double NewCircleTop
        {
            get { return _newCircleTop; }
            set
            {
                _newCircleTop = value;
                RaisePropertyChanged(() => NewCircleTop);
            }
        }

        public double NewCircleRadius
        {
            get { return _newCircleRadius; }
            set
            {
                if(Equals(_newCircleRadius, value)) return;
                _newCircleRadius = value;
                SetNewCircleMaxRadius();
                RaisePropertyChanged(() => NewCircleRadius);
            }
        }

        public ObservableCollection<Point> Points
        {
            get { return _points; }
            set
            {
                if (Equals(_points, value)) return;
                _points = value; 
                RaisePropertyChanged(() => Points);
            }
        }

        public int CirclesCount
        {
            get { return _circlesCount; }
            set
            {
                if (Equals(_circlesCount, value)) return;
                _circlesCount = value;
                RaisePropertyChanged(() => CirclesCount);
            }
        }

        public Canvas CirclesCanvas
        {
            get { return _circlesCanvas; }
            set
            {
                if(Equals(_circlesCanvas, value)) return;
                _circlesCanvas = value;
                RaisePropertyChanged(() => CirclesCanvas);
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

        public bool CalcCirclesAutomatically
        {
            get { return _calcCirclesAutomatically; }
            set
            {
                if (Equals(_calcCirclesAutomatically, value)) return;
                _calcCirclesAutomatically = value;
                RaisePropertyChanged(() => CalcCirclesAutomatically);
            }
        }*/

        public ObservableCollection<RadMenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (Equals(_menuItems, value)) return;
                _menuItems = value; 
                RaisePropertyChanged(() => MenuItems);
            }
        }

        #endregion

        /*#region Commands

        public ICommand ClearCirclesCommand
        {
            get { return _clearCirclesCommand ?? (_clearCirclesCommand = new RelayCommand(ClearCircles)); }
        }

        public ICommand AddNewCircleCommand
        {
            get { return _addNewCircleCommand ?? (_addNewCircleCommand = new RelayCommand(AddNewCircle)); }
        }

        public ICommand ShowCoordinatesCommand
        {
            get { return _showCoordinatesCommand ?? (_showCoordinatesCommand = new RelayCommand(ShowCoordinates)); }
        }

        #endregion*/

        #region Constructor

        public MainViewModel()
        {
            var radMenuItem = new RadMenuItem {Header = "Опції"};
            radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Налаштування",
                Command = new RelayCommand(() => Container.Resolve<ISettingsWindow>().ShowDialog())
            });
            /*radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Вихід",
                Command = new RelayCommand(() => Application.Current.Shutdown())
            });*/
            MenuItems.Add(radMenuItem);

            CalculateParametersContentViewModel = Container.Resolve<CalculateParametersViewModel>();
            AddCircleContentViewModel = Container.Resolve<AddCircleViewModel>();
            CirclesResultContentViewModel = Container.Resolve<CirclesResultViewModel>();
        }

        #endregion

        /*#region Private Methods

        private void SetNewCircleMaxRadius()
        {
            _newCircleMaxRadius = RectangleHeight < RectangleWidth ? RectangleHeight / 2 : RectangleWidth / 2;
            if (_newCircleRadius > _newCircleMaxRadius) _newCircleRadius = _newCircleMaxRadius;
        }

        private void ClearCircles()
        {
            CirclesCanvas.Children.Clear();
        }

        private void AddNewCircle()
        {
            if(NewCircleRadius == 0) return;
            AddCircle(0,0, NewCircleRadius);
            CirclesCount = CirclesCanvas.Children.Count;
        }

        private void CalcCircles()
        {
            var hasErrors = ValidateValues();
            if (hasErrors) return;

            ClearCircles();
            var rectangleWithCircles = new CircleService();
            var points =
                rectangleWithCircles.GetCirclesCenters(RectangleWidth, RectangleHeight, CircleRadius, MinimalGap);
            if (points.Count() > _maxCircles)
            {
                MessageBox.Show("Кількість кіл надто велика і може призвести до втрати швидкодії.\n" +
                                "Попробуйте змінити параметри:)");
                return;
            }
            foreach (var point in points)
                AddCircle(point.X - CircleRadius, point.Y - CircleRadius, CircleRadius);
            CirclesCount = CirclesCanvas.Children.Count;
            Container.RegisterInstance(points);
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

        private void ShowCoordinates()
        {
            var coordinatesWindow = new CoordinatesCirclesWindow();
            coordinatesWindow.ShowDialog();
        }
        private void ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isCircleSelected = false;
            IsBlocked = false;
        }

        private Ellipse GetCrossedCircle(Ellipse ellipse)
        {
            foreach (Ellipse circle in CirclesCanvas.Children)
            {
                if (!Equals(circle, ellipse))
                {
                    if(CheckCirclesCrossing(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse),
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

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(CirclesCanvas);
            var x = mousePosition.X - _mouseOffsetX;
            var y = mousePosition.Y - _mouseOffsetY;
            /*PositionX = x;
            PositionY = y;#1#
            if (_isCircleSelected)
            {
                var ellipse = (Ellipse)sender;
                var crossedCircle = GetCrossedCircle(ellipse);

                IsCrossed = crossedCircle != null;
                if (IsBlocked)
                {
                    //IsBlocked = false;
                    e.Handled = true;
                }
                else if (IsCrossed)
                {
                    Point point = GetLastPoint(crossedCircle);
                    if (point != null && !IsBlocked)
                    {
                        IsBlocked = true;
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

        private bool CheckCirclesCrossing(double x1, double y1, double x2, double y2, double r)
        {
            var result = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            return result < (r + r + MinimalGap);
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

        private bool ValidateValues()
        {
            var errorMessage = (RectangleWidth <= 0 ? "Ширина заготовки\n" : string.Empty) +
                               (RectangleHeight <= 0 ? "Висота заготовки\n" : string.Empty) +
                               (CircleRadius <= 0 ? "Радіус кола\n" : string.Empty);
            if (errorMessage == string.Empty) return false;
            MessageBox.Show(errorMessage + "\nНе може(можуть) бути 0", "Помилка вводу");
            return true;
        }

        #endregion*/
    }
}
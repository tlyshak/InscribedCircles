using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Core;
using InscribedCircles.MainApp.Models;
using InscribedCircles.MainApp.Windows;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace InscribedCircles.MainApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Circle> _circles = new ObservableCollection<Circle>();
        private ICommand _calcCirclesCommand;
        private ICommand _showCoordinatesCommand;
        private double _circleRadius;
        private double _rectangleWidth;
        private double _rectangleHeight;
        private double _minimalGap;
        private ObservableCollection<RadMenuItem> _menuItems = new ObservableCollection<RadMenuItem>();
        private bool _isBusy;
        private Canvas _canvasControl = new Canvas {Background = new SolidColorBrush(Colors.LightSteelBlue)};
        private ICommand _addOptionalCircleCommand;

        #endregion

        #region Properties

        public Canvas CanvasControl
        {
            get { return _canvasControl; }
            set
            {
                _canvasControl = value;
                RaisePropertyChanged(() => CanvasControl);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if(Equals(_isBusy, value)) return;
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ObservableCollection<Circle> Circles
        {
            get { return _circles; }
            set
            {
                if (Equals(_circles, value)) return;
                _circles = value;
                RaisePropertyChanged(() => Circles);
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

        #region Commands

        public ICommand AddOptionalCircleCommand
        {
            get { return _addOptionalCircleCommand ?? (_addOptionalCircleCommand = new RelayCommand(AddOptionalCircle)); }
        }

        public ICommand CalcCirclesCommand
        {
            get { return _calcCirclesCommand ?? (_calcCirclesCommand = new RelayCommand(CalcCircles)); }
        }
        public ICommand ShowCoordinatesCommand
        {
            get { return _showCoordinatesCommand ?? (_showCoordinatesCommand = new RelayCommand(ShowCoordinates)); }
        }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            var radMenuItem = new RadMenuItem {Header = "Файл"};
            radMenuItem.Items.Add(new RadMenuItem {Header = "Вихід", Command = new RelayCommand(() => Application.Current.Shutdown())});
            MenuItems.Add(radMenuItem);
        }
        #endregion

        #region Private Methods

        private void AddOptionalCircle()
        {
            var random = new Random();
            var ellipse = new Ellipse
            {
                Height = CircleRadius * 2,
                Width = CircleRadius * 2,
                Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(50, 100), (byte)random.Next(50, 100),
                        (byte)random.Next(50, 100)))
            };
            var dragBehavior = new MouseDragElementBehavior() { ConstrainToParentBounds = true };
            dragBehavior.Attach(ellipse);
            Canvas.SetLeft(ellipse, 0);
            Canvas.SetTop(ellipse, 0);
            CanvasControl.Children.Add(ellipse);
        }
        private void CalcCircles()
        {
            var hasErrors = ValidateValues();
            if (hasErrors) return;

            var rectangleWithCircles = new InscribedCirclesService();
            var points = rectangleWithCircles.GetCirclesCenters(RectangleWidth, RectangleHeight, CircleRadius, MinimalGap);
            var random = new Random();
            CanvasControl.Children.Clear();
            foreach (var point in points)
            {
                var ellipse = new Ellipse
                {
                    Height = CircleRadius*2,
                    Width = CircleRadius*2,
                    Fill = new SolidColorBrush(Color.FromRgb((byte) random.Next(50, 100), (byte) random.Next(50, 100),
                            (byte) random.Next(50, 100)))
                };
                var dragBehavior = new MouseDragElementBehavior(){ConstrainToParentBounds = true};
                dragBehavior.Attach(ellipse);
                Canvas.SetLeft(ellipse, point.CenterX - CircleRadius);
                Canvas.SetTop(ellipse, point.CenterY - CircleRadius);
                CanvasControl.Children.Add(ellipse);
            }
            Container.RegisterInstance(points);
        }

        private void ShowCoordinates()
        {
            var coordinatesWindow = new CoordinatesCirclesWindow();
            coordinatesWindow.ShowDialog();
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

        #endregion
    }
}
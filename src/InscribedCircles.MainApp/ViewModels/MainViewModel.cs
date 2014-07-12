﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Core;
using InscribedCircles.MainApp.Windows;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace InscribedCircles.MainApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Fields
        private ICommand _calcCirclesCommand;
        private ICommand _showCoordinatesCommand;
        private double _circleRadius;
        private double _rectangleWidth;
        private double _rectangleHeight;
        private double _minimalGap;
        private ObservableCollection<RadMenuItem> _menuItems = new ObservableCollection<RadMenuItem>();
        private Canvas _circlesPlace = new Canvas {Background = new SolidColorBrush(Colors.LightSteelBlue)};
        private ICommand _addNewCircleCommand;
        private readonly Random _random;
        private int _circlesCount;
        private double _newCircleRadius;
        private double _newCircleMaxRadius;

        #endregion

        #region Properties

        public double NewCircleMaxRadius
        {
            get { return _newCircleMaxRadius; }
            set
            {
                if (Equals(_newCircleMaxRadius, value)) return;
                _newCircleMaxRadius = value; 
                RaisePropertyChanged(() => NewCircleMaxRadius);
            }
        }

        public double NewCircleRadius
        {
            get { return _newCircleRadius; }
            set
            {
                if(Equals(_newCircleRadius, value)) return;
                _newCircleRadius = value;
                NewCircleMaxRadius = RectangleHeight < RectangleWidth ? RectangleHeight/2 : RectangleWidth/2;
                if (_newCircleRadius > NewCircleMaxRadius) _newCircleRadius = NewCircleMaxRadius;
                RaisePropertyChanged(() => NewCircleRadius);
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

        public Canvas CirclesPlace
        {
            get { return _circlesPlace; }
            set
            {
                if(Equals(_circlesPlace, value)) return;
                _circlesPlace = value;
                RaisePropertyChanged(() => CirclesPlace);
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

        public ICommand AddNewCircleCommand
        {
            get { return _addNewCircleCommand ?? (_addNewCircleCommand = new RelayCommand(AddNewCircle)); }
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

            _random = new Random();
        }
        #endregion

        #region Private Methods

        private void AddNewCircle()
        {
            if(NewCircleRadius == 0) return;
            AddCircle(0,0, NewCircleRadius);

            CirclesCount = CirclesPlace.Children.Count;
        }

        private void CalcCircles()
        {
            var hasErrors = ValidateValues();
            if (hasErrors) return;

            CirclesPlace.Children.Clear();
            var rectangleWithCircles = new InscribedCirclesService();
            var points =
                rectangleWithCircles.GetCirclesCenters(RectangleWidth, RectangleHeight, CircleRadius, MinimalGap)
                    .ToList();
            if (points.Count > 1000)
            {
                MessageBox.Show("Кількість кіл надто велика і може призвести до втрати швидкодії.\n" +
                                "Попробуйте змінити параметри:)");
                return;
            }
            foreach (var point in points)
            {
                AddCircle(point.CenterX - CircleRadius, point.CenterY - CircleRadius, CircleRadius);
            }
            CirclesCount = CirclesPlace.Children.Count;
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
            Canvas.SetLeft(ellipse, offsetX);
            Canvas.SetTop(ellipse, offsetY);
            CirclesPlace.Children.Add(ellipse);
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
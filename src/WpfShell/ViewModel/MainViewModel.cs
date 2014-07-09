using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CirclesInRectangle;
using GalaSoft.MvvmLight.Command;
using Telerik.Windows.Controls;
using WpfShell.Models;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace WpfShell.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<CustomEllipse> _circles;
        private ICommand _calcCirclesCommand;
        private double _circleRadius;
        private double _rectangleWidth;
        private double _rectangleHeight;
        private double _minimalGap;
        private ObservableCollection<RadMenuItem> _menuItems = new ObservableCollection<RadMenuItem>();

        #endregion

        #region Properties
        public ObservableCollection<CustomEllipse> Circles
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
        public ICommand CalcCirclesCommand
        {
            get { return _calcCirclesCommand ?? (_calcCirclesCommand = new RelayCommand(CalcCircles)); }
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
        private void CalcCircles()
        {
            StaticData.DefinedWidth = RectangleWidth;
            StaticData.DefinedHeight = RectangleHeight;

            var hasErrors = ValidateValues();
            if (hasErrors) return;

            var random = new Random();
            Circles = new ObservableCollection<CustomEllipse>();

            var rectangleWithCircles = new RectangleWithCircles(RectangleWidth, RectangleHeight);
            var points = rectangleWithCircles.GetHexagonsCountFromRectangle(CircleRadius, MinimalGap);
            foreach (var point in points)
            {
                Circles.Add(new CustomEllipse(new Ellipse
                {
                    Width = CircleRadius * 2, // is diameter
                    Height = CircleRadius * 2, // is diameter
                    Fill =
                        new SolidColorBrush(Color.FromRgb((byte)random.Next(100, 200), (byte)random.Next(100, 200),
                            (byte)random.Next(100, 200))) //random color
                })
                {
                    X = point.CenterX - CircleRadius,
                    Y = point.CenterY - CircleRadius
                });
            }
        }

        private bool ValidateValues()
        {
            var errorMessage = (RectangleWidth == 0 ? "Ширина заготовки\n" : string.Empty) +
                               (RectangleHeight == 0 ? "Висота заготовки\n" : string.Empty) +
                               (CircleRadius == 0 ? "Радіус кола\n" : string.Empty);
            if (errorMessage == string.Empty) return false;
            MessageBox.Show(errorMessage + "\nНе може(можуть) бути 0", "Помилка вводу");
            return true;
        }

        #endregion
    }

    public static class StaticData
    {
        public static double Scale;
        public static double DefinedHeight;
        public static double DefinedWidth;
    }
}
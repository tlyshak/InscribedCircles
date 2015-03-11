using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.ViewModels;
using InscribedCircles.Core;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;
using Point = InscribedCircles.Core.Point;

namespace InscribedCircles.MainApp.ViewModels
{
    public class CalculateParametersViewModel : ViewModel, ICalculateParametersViewModel
    {
        private const int MaxCircles = 1000;
        private double _minimalGap;
        private double _rectangleWidth;
        private double _rectangleHeight;
        private bool _calcCirclesAutomatically;
        private double _circleRadius;
        private IEnumerable<Point> _points = new List<Point>();

        #region Properties

        public IEnumerable<Point> Points
        {
            get { return _points; }
            set { _points = value; }
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
        } 
        #endregion

        public ICommand CalculateCommand { get { return new RelayCommand(Calculate); } }

        private void Calculate()
        {
            if (CalcCirclesAutomatically) 
                CalcCircles();

            OnBuildCanvasEvent();
        }
        private void CalcCircles()
        {
            var hasErrors = ValidateValues();
            if (hasErrors) return;

            var rectangleWithCircles = new CircleService();
            var points = rectangleWithCircles.GetCirclesCenters(RectangleWidth, RectangleHeight, CircleRadius, MinimalGap);
            if (points.Count() > MaxCircles)
                RadWindow.Alert("Кількість кіл надто велика і може призвести до втрати швидкодії.\n" +
                                "Попробуйте змінити параметри.");
            else
            {
                Points = points;
                Container.RegisterInstance(Points);
            }
        }

        private bool ValidateValues()
        {
            var errorMessage = (RectangleWidth <= 0 ? "Ширина заготовки\n" : string.Empty) +
                               (RectangleHeight <= 0 ? "Висота заготовки\n" : string.Empty) +
                               (CircleRadius <= 0 ? "Радіус кола\n" : string.Empty);
            if (errorMessage == string.Empty) return false;
            RadWindow.Alert(new DialogParameters
            {
                Header = "Помилка вводу",
                Content = errorMessage + "\nНе може(можуть) бути 0",
                DialogStartupLocation = WindowStartupLocation.CenterScreen
            });
            return true;
        }

        public event EventHandler BuildCanvasEvent;

        protected virtual void OnBuildCanvasEvent()
        {
            var handler = BuildCanvasEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.ViewModels;
using Point = InscribedCircles.Core.Point;

namespace InscribedCircles.MainApp.ViewModels
{
    public class AddCircleViewModel : ViewModel, IAddCircleViewModel
    {
        private ICommand _addNewCircleCommand;
        private double _newCircleRadius;
        private double _minimalGap;

        public double NewCircleRadius
        {
            get { return _newCircleRadius; }
            set
            {
                if (Equals(_newCircleRadius, value)) return;
                _newCircleRadius = value;
                RaisePropertyChanged(() => NewCircleRadius);
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

        public ICommand AddNewCircleCommand
        {
            get { return _addNewCircleCommand ?? (_addNewCircleCommand = new RelayCommand(AddNewCircle)); }
        }

        private void AddNewCircle()
        {
            OnAddCircleEvent();
        }

        public event EventHandler AddNewCircleEvent;

        protected virtual void OnAddCircleEvent()
        {
            var handler = AddNewCircleEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}

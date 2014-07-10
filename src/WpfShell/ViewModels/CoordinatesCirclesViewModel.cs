using System;
using System.Collections.Generic;
using CirclesInRectangle;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using WpfShell.Models;

namespace WpfShell.ViewModels
{
    public class CoordinatesCirclesViewModel : ViewModelBase
    {
        private IUnityContainer _unityContainer;
        private IEnumerable<Point> _points;

        public IEnumerable<Point> Points
        {
            get { return _points; }
            set
            {
                if(Equals(_points, value)) return;
                _points = value;
                RaisePropertyChanged(() => Points);
            }
        }

        public CoordinatesCirclesViewModel()
        {
            _unityContainer = ContainerAccessor.Container;
            if (_unityContainer != null) Points = _unityContainer.Resolve<IEnumerable<Point>>();
        }
    }
}

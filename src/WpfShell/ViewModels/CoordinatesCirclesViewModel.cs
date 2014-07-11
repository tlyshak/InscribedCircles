using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private bool _isBusy;

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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (Equals(_isBusy, value)) return;
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public CoordinatesCirclesViewModel()
        {
            _unityContainer = ContainerAccessor.Container;
            SetPoints();
        }

        public async void SetPoints()
        {
            await Task.Run(() => SetPointsAsync());
        }

        public void SetPointsAsync()
        {
            IsBusy = true;
            if (_unityContainer != null) Points = _unityContainer.Resolve<IEnumerable<Point>>();
            IsBusy = false;
        }
    }
}

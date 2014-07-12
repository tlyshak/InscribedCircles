using System.Collections.Generic;
using System.Threading.Tasks;
using InscribedCircles.Abstraction;
using InscribedCircles.Core;
using Microsoft.Practices.Unity;

namespace InscribedCircles.MainApp.ViewModels
{
    public class CoordinatesCirclesViewModel : ViewModel
    {
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
            SetPoints();
        }

        public async void SetPoints()
        {
            await Task.Run(() => SetPointsAsync());
        }

        public void SetPointsAsync()
        {
            IsBusy = true;
            Points = Container.Resolve<IEnumerable<Point>>();
            IsBusy = false;
        }
    }
}

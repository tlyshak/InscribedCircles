using System.Collections.Generic;
using InscribedCircles.Abstraction;
using InscribedCircles.Core;
using Microsoft.Practices.Unity;

namespace InscribedCircles.MainApp.ViewModels
{
    public class CoordinatesCirclesViewModel : ViewModel
    {
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
            //Points = Container.Resolve<IEnumerable<Point>>();
        }
    }
}

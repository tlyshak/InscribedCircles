using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;

namespace InscribedCircles.Abstraction
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            Container = ContainerAccessor.Container;
        }

        protected IUnityContainer Container { get; set; }
    }
}

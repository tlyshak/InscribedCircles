using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;

namespace InscribedCircles.Abstraction
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            ContainerAccessor.Container = ContainerAccessor.Container ?? new UnityContainer();
            Container = ContainerAccessor.Container;
        }

        protected IUnityContainer Container { get; set; }
    }
}

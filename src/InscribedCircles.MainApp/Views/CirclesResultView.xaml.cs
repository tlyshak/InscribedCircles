using InscribedCircles.Abstraction.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Views
{
    /// <summary>
    /// Interaction logic for CirclesResultView.xaml
    /// </summary>
    [ViewModel(typeof(CirclesResultViewModel))]
    public partial class CirclesResultView
    {
        public CirclesResultView()
        {
            InitializeComponent();
        }
    }
}

using InscribedCircles.Abstraction.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Views
{
    /// <summary>
    /// Interaction logic for AddCircleView.xaml
    /// </summary>
    [ViewModel(typeof(AddCircleViewModel))]
    public partial class AddCircleView
    {
        public AddCircleView()
        {
            InitializeComponent();
        }
    }
}

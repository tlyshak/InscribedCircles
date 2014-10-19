using InscribedCircles.Abstraction.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Views
{
    /// <summary>
    /// Interaction logic for CalculateParametersView.xaml
    /// </summary>
    [ViewModel(typeof(CalculateParametersViewModel))]
    public partial class CalculateParametersView
    {
        public CalculateParametersView()
        {
            InitializeComponent();
        }
    }
}

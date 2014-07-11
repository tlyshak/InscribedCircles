using InscribedCircles.MainApp.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for CoordinatesCirclesWindow.xaml
    /// </summary>
    [ViewModel(typeof(CoordinatesCirclesViewModel))]
    public partial class CoordinatesCirclesWindow
    {
        public CoordinatesCirclesWindow()
        {
            InitializeComponent();
            DataContext = new CoordinatesCirclesViewModel();
        }
    }
}

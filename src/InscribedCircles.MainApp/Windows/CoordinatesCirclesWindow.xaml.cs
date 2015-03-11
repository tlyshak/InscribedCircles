using InscribedCircles.Abstraction.Interfaces.Windows;
using InscribedCircles.MainApp.ViewModels;
using Telerik.Windows.Controls.Navigation;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for CoordinatesCirclesWindow.xaml
    /// </summary>
    public partial class CoordinatesCirclesWindow : ICoordinatesCirclesWindow
    {
        public CoordinatesCirclesWindow()
        {
            InitializeComponent();
            RadWindowInteropHelper.SetShowInTaskbar(this, true);
            DataContext = new CoordinatesCirclesViewModel();
        }
    }
}

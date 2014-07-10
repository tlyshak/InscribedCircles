using WpfShell.Attributes;
using WpfShell.ViewModels;

namespace WpfShell.Windows
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

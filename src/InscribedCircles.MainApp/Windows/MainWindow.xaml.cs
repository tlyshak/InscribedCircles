using InscribedCircles.Abstraction;
using InscribedCircles.MainApp.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ViewModel(typeof(MainViewModel))]
    public partial class MainWindow : IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}

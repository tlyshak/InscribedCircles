using InscribedCircles.Abstraction.Attributes;
using InscribedCircles.Abstraction.Interfaces.Windows;
using InscribedCircles.MainApp.ViewModels;
using Telerik.Windows.Controls.Navigation;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            RadWindowInteropHelper.SetShowInTaskbar(this, true);
            DataContext = new MainViewModel();
        }
    }
}

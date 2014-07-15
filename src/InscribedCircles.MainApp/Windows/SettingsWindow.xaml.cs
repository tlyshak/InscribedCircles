using InscribedCircles.MainApp.Attributes;
using InscribedCircles.MainApp.ViewModels;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    [ViewModel(typeof(SettingsViewModel))]
    public partial class SettingsWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
    }
}

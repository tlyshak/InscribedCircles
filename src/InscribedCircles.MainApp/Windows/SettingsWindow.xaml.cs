using InscribedCircles.Abstraction.Attributes;
using InscribedCircles.Abstraction.Interfaces.Windows;
using InscribedCircles.MainApp.ViewModels;
using Telerik.Windows.Controls.Navigation;

namespace InscribedCircles.MainApp.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    [ViewModel(typeof(SettingsViewModel))]
    public partial class SettingsWindow : ISettingsWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            RadWindowInteropHelper.SetShowInTaskbar(this, true);
        }
    }
}

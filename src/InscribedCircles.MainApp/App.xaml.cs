using System.Windows.Media;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.ViewModels;
using InscribedCircles.Abstraction.Interfaces.Windows;
using InscribedCircles.MainApp.ViewModels;
using InscribedCircles.MainApp.Windows;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace InscribedCircles.MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var container = new UnityContainer();
            StyleManager.ApplicationTheme = new Windows8Theme();
            Windows8Palette.Palette.AccentColor = Colors.Green;
            Windows8Palette.Palette.StrongColor = Colors.DarkGreen;

            container.RegisterType<ICirclesResultViewModel, CirclesResultViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAddCircleViewModel, AddCircleViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICalculateParametersViewModel, CalculateParametersViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMainWindow, MainWindow>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsWindow, SettingsWindow>(new ContainerControlledLifetimeManager());

            ContainerAccessor.Container = container;

            container.Resolve<IMainWindow>().ShowDialog();
        }
    }
}

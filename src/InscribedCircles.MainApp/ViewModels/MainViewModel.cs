using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using InscribedCircles.Abstraction;
using InscribedCircles.Abstraction.Interfaces.Windows;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;

namespace InscribedCircles.MainApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<RadMenuItem> _menuItems = new ObservableCollection<RadMenuItem>();
        private ViewModel _calculateParametersContentViewModel;
        private ViewModel _addCircleContentViewModel;
        private ViewModel _circlesResultContentViewModel;

        #endregion

        #region Properties

        public ViewModel CalculateParametersContentViewModel
        {
            get { return _calculateParametersContentViewModel; }
            set
            {
                if (Equals(_calculateParametersContentViewModel, value)) return;
                _calculateParametersContentViewModel = value;
                RaisePropertyChanged(() => CalculateParametersContentViewModel);
            }
        }

        public ViewModel AddCircleContentViewModel
        {
            get { return _addCircleContentViewModel; }
            set
            {
                if (Equals(_addCircleContentViewModel, value)) return;
                _addCircleContentViewModel = value;
                RaisePropertyChanged(() => AddCircleContentViewModel);
            }
        }

        public ViewModel CirclesResultContentViewModel
        {
            get { return _circlesResultContentViewModel; }
            set
            {
                if (Equals(_circlesResultContentViewModel, value)) return;
                _circlesResultContentViewModel = value;
                RaisePropertyChanged(() => CirclesResultContentViewModel);
            }
        }

        public ObservableCollection<RadMenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (Equals(_menuItems, value)) return;
                _menuItems = value; 
                RaisePropertyChanged(() => MenuItems);
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            AddMenuItems();

            CalculateParametersContentViewModel = Container.Resolve<CalculateParametersViewModel>();
            AddCircleContentViewModel = Container.Resolve<AddCircleViewModel>();
            CirclesResultContentViewModel = Container.Resolve<CirclesResultViewModel>();
        }

        private void AddMenuItems()
        {
            var radMenuItem = new RadMenuItem { Header = "Опції" };
            radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Налаштування",
                Command = new RelayCommand(() => Container.Resolve<ISettingsWindow>().ShowDialog())
            });
            radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Вихід",
                Command = new RelayCommand(() => Application.Current.Shutdown())
            });
            MenuItems.Add(radMenuItem);
        }

        #endregion
    }
}
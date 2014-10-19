using System.Collections.ObjectModel;
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

        /*public double PositionX
        {
            get { return _positionX; }
            set
            {
                _positionX = value; 
                RaisePropertyChanged(() => PositionX);
            }
        }

        public double PositionY
        {
            get { return _positionY; }
            set
            {
                _positionY = value; 
                RaisePropertyChanged(() => PositionY);
            }
        }

        public bool IsCrossed
        {
            get { return _isCrossed; }
            set
            {
                _isCrossed = value;
                RaisePropertyChanged(() => IsCrossed);
            }
        }

        public bool IsBlocked
        {
            get { return _isBlocked; }
            set
            {
                _isBlocked = value;
                RaisePropertyChanged(() => IsBlocked);
            }
        }

        public double NewCircleLeft
        {
            get { return _newCircleLeft; }
            set
            {
                _newCircleLeft = value; 
                RaisePropertyChanged(() => NewCircleLeft);
            }
        }

        public double NewCircleTop
        {
            get { return _newCircleTop; }
            set
            {
                _newCircleTop = value;
                RaisePropertyChanged(() => NewCircleTop);
            }
        }

        public double NewCircleRadius
        {
            get { return _newCircleRadius; }
            set
            {
                if(Equals(_newCircleRadius, value)) return;
                _newCircleRadius = value;
                SetNewCircleMaxRadius();
                RaisePropertyChanged(() => NewCircleRadius);
            }
        }

        public int CirclesCount
        {
            get { return _circlesCount; }
            set
            {
                if (Equals(_circlesCount, value)) return;
                _circlesCount = value;
                RaisePropertyChanged(() => CirclesCount);
            }
        }

        public Canvas InscribingArea
        {
            get { return _inscribingArea; }
            set
            {
                if(Equals(_inscribingArea, value)) return;
                _inscribingArea = value;
                RaisePropertyChanged(() => InscribingArea);
            }
        }
        public double CircleRadius
        {
            get { return _circleRadius; }
            set
            {
                if (Equals(_circleRadius, value)) return;
                _circleRadius = value;
                RaisePropertyChanged(() => CircleRadius);
            }
        }
        public double RectangleWidth
        {
            get { return _rectangleWidth; }
            set
            {
                if (Equals(_rectangleWidth, value)) return;
                _rectangleWidth = value;
                RaisePropertyChanged(() => RectangleWidth);
            }
        }

        public double RectangleHeight
        {
            get { return _rectangleHeight; }
            set
            {
                if (Equals(_rectangleHeight, value)) return;
                _rectangleHeight = value;
                RaisePropertyChanged(() => RectangleHeight);
            }
        }

        public double MinimalGap
        {
            get { return _minimalGap; }
            set
            {
                if (Equals(_minimalGap, value)) return;
                _minimalGap = value;
                RaisePropertyChanged(() => MinimalGap);
            }
        }

        public bool CalcCirclesAutomatically
        {
            get { return _calcCirclesAutomatically; }
            set
            {
                if (Equals(_calcCirclesAutomatically, value)) return;
                _calcCirclesAutomatically = value;
                RaisePropertyChanged(() => CalcCirclesAutomatically);
            }
        }*/

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
            var radMenuItem = new RadMenuItem {Header = "Опції"};
            radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Налаштування",
                Command = new RelayCommand(() => Container.Resolve<ISettingsWindow>().ShowDialog())
            });
            /*radMenuItem.Items.Add(new RadMenuItem
            {
                Header = "Вихід",
                Command = new RelayCommand(() => Application.Current.Shutdown())
            });*/
            MenuItems.Add(radMenuItem);

            CalculateParametersContentViewModel = Container.Resolve<CalculateParametersViewModel>();
            AddCircleContentViewModel = Container.Resolve<AddCircleViewModel>();
            CirclesResultContentViewModel = Container.Resolve<CirclesResultViewModel>();
        }

        #endregion
    }
}
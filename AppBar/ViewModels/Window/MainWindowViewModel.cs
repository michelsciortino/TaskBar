using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppBar.Helpers;

namespace AppBar.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Private Variables

        WindowLocation BarLocation = WindowLocation.NONE;

        #endregion

        #region Private Properties

        private List<ItemViewModel> _items;
        private double _yPosition =0;
        private double _xPosition = 0;
        private double _contentPadding = 0;
        private double _borderSize = 0;
        private SolidColorBrush _borderColor = Application.Current.FindResource("DefaultBorderColorBrush") as SolidColorBrush;
        private SolidColorBrush _backgroundColor = Application.Current.FindResource("DefaultBarBackgroundColorBrush") as SolidColorBrush;
        private double _barMinWidth = 16;
        private double _barMinHeight = 16;
        private double _barWidth = 150;
        private double _barHeight = 32;
        private WindowState _barWindowState;
        private Orientation _barOrientation=Orientation.Horizontal;
        private bool _isLocked = false;
        private string _lockUnlockText="Lock";
        private bool _isOnTop = false;

        #endregion        

        #region Public Properties

        public double YPosition
        {
            get => _yPosition;
            set
            {
                if (_yPosition != value)
                {
                    _yPosition = value;
                    OnPropertyChanged(nameof(YPosition));
                }
            }
        }

        public double XPosition
        {
            get => _xPosition;
            set
            {
                if (_xPosition != value)
                {
                    _xPosition = value;
                    OnPropertyChanged(nameof(XPosition));
                }
            }
        }

        /// <summary>
        /// The AppBar minimum width
        /// </summary>
        public double BarMinWidth
        {
            get => _barMinWidth;
            set
            {
                if (value != _barMinWidth)
                {
                    _barMinWidth = value;
                    OnPropertyChanged(nameof(BarMinWidth));
                }
            }
        }

        /// <summary>
        /// The AppBar minimum height
        /// </summary>
        public double BarMinHeight
        {
            get => _barMinHeight;
            set
            {
                if (value != _barMinHeight)
                    _barMinHeight = value;

                OnPropertyChanged(nameof(BarMinHeight));
            }
        }

        /// <summary>
        /// The AppBar width
        /// </summary>
        public double BarWidth
        {
            get => _barWidth;
            set
            {
                if (value != _barWidth)
                {
                    _barWidth = value;

                    OnPropertyChanged(nameof(BarWidth));
                }
            }
        }

        /// <summary>
        /// The AppBar height
        /// </summary>
        public double BarHeight
        {
            get => _barHeight;
            set
            {
                if (value != _barHeight)
                    _barHeight = value;
                OnPropertyChanged(nameof(BarHeight));
            }
        }

        /// <summary>
        /// The internal content padding of the AppBar
        /// </summary>
        public double ContentPadding
        {
            get => _contentPadding;
            set
            {
                if (value != _contentPadding)
                {
                    _contentPadding = value;
                    OnPropertyChanged(nameof(ContentPadding));
                }
            }
        }

        /// <summary>
        /// The outer AppBar Border Size
        /// </summary>
        public double BorderSize
        {
            get => _borderSize;
            set
            {
                if (value != _borderSize)
                {
                    _borderSize = value;
                    OnPropertyChanged(nameof(BorderSize));
                }
            }
        }

        /// <summary>
        /// The outer AppBar Border Thickness
        /// </summary>
        public Thickness BorderThickness
        {
            get => new Thickness(_borderSize);
            set
            {
                if (value != new Thickness(_borderSize))
                {
                    _borderSize = (int)value.Top;
                    OnPropertyChanged(nameof(BorderThickness));
                }
            }
        }

        /// <summary>
        /// The AppBar Background Color
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor) {
                    _backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        /// <summary>
        /// The outer AppBar Border Color
        /// </summary>
        public SolidColorBrush BorderColor
        {
            get => _borderColor;
            set
            {
                if (value != _borderColor) {
                    _borderColor = value;
                    OnPropertyChanged(nameof(BorderColor));
                }
            }
        }

        public WindowState BarWindowState
        {
            get => _barWindowState; set
            {
                if (value != _barWindowState)
                {
                    _barWindowState = value;
                    OnPropertyChanged(nameof(BarWindowState));
                }
            }
        }

        public Orientation BarOrientation
        {
            get => _barOrientation;
            set
            {
                if (_barOrientation != value)
                {
                    _barOrientation = value;
                    OnPropertyChanged(nameof(BarOrientation));
                }
            }
        }

        /// <summary>
        /// The Item List hosted in the AppBar
        /// </summary>
        public List<ItemViewModel> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public string LockUnlockText
        {
            get => _lockUnlockText;
            set
            {
                if (_lockUnlockText != value)
                {
                    _lockUnlockText = value;
                    OnPropertyChanged(nameof(LockUnlockText));
                }
            }
        }

        public bool IsOnTop
        {
            get => _isOnTop;
            set
            {
                if (_isOnTop != value)
                {
                    _isOnTop = value;
                    OnPropertyChanged(nameof(IsOnTop));
                }
            }
        }

        #endregion

        #region Private Commands

        private ICommand _hideAppBarCommand;
        private ICommand _showAppBarcommand;
        private ICommand _barMouseLeftButtonDownCommand;
        private ICommand _barChangeLocationCommand;
        private ICommand _barChangeOrientationCommand;
        private ICommand _sizeChangedCommand;
        private ICommand _onLoadedCommand;
        private ICommand _barLockUnlockCommand;
        private ICommand _exitCommand;
        private ICommand _topMostCheckedCommand;
        private ICommand _topMostUncheckedCommand;

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to hide the AppBar
        /// </summary>
        public ICommand HideAppBarCommand { get; set; }

        /// <summary>
        /// The command to Show the AppBar
        /// </summary>
        public ICommand ShowAppBarcommand { get; set; }

        /// <summary>
        /// The command to expand the AppBar to full display width
        /// </summary>
        public ICommand MaximizeAppBarCommand { get; set; }

        /// <summary>
        /// The command to close the AppBar
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the menu of the AppBar
        /// </summary>
        public ICommand MenuCommand { get; set; }

        /// <summary>
        /// The command that handles the left mouse bottom down event
        /// </summary>
        public ICommand BarMouseLeftButtonDownCommand
        {
            get
            {
                return _barMouseLeftButtonDownCommand ??
                    (_barMouseLeftButtonDownCommand = new RelayCommand<object>(x => { SolveMouseLeftButtonGesture(x as MouseButtonEventArgs); }));
            }
        }

        /// <summary>
        /// The command that changes the location of the AppBar
        /// </summary>
        public ICommand BarChangeLocationCommand
        {
            get
            {
                return _barChangeLocationCommand ??
                  (_barChangeLocationCommand = new RelayCommand<object>(x => { BarChangeLocation((WindowLocation)x); }));
            }
        }

        /// <summary>
        /// The command that changes the orientation of the AppBar
        /// </summary>
        public ICommand BarChangeOrientationCommand
        {
            get
            {
                return _barChangeOrientationCommand ??
                  (_barChangeOrientationCommand = new RelayCommand<object>(x => { BarChangeOrientation((Orientation)x); }));
            }
        }
        
        /// <summary>
        /// The commands that handles the SizeChanged event of the AppBar
        /// </summary>
        public ICommand SizeChangedCommand
        {
            get
            {
                return _sizeChangedCommand ?? (_sizeChangedCommand = new RelayCommand<object>(x => { UpdateActualBarSize((SizeChangedEventArgs)x); }));
            }
        }

        /// <summary>
        /// The command to set Topmost to true
        /// </summary>
        public ICommand TopMostCheckedCommand
        {
            get
            {
                return _topMostCheckedCommand ?? (_topMostCheckedCommand = new RelayCommand<object>(x => { TopmostChange((RoutedEventArgs)x); }));
            }
        }

        /// <summary>
        /// The command to set Topmost to false
        /// </summary>
        public ICommand TopMostUncheckedCommand
        {
            get
            {
                return _topMostUncheckedCommand ?? (_topMostUncheckedCommand = new RelayCommand<object>(x => { TopmostChange((RoutedEventArgs)x); }));
            }
        }

        /// <summary>
        /// The command that locks and unlocks the AppBar position
        /// </summary>
        public ICommand BarLockUnlockCommand
        {
            get
            {
                return _barLockUnlockCommand ?? (_barLockUnlockCommand = new RelayCommand<object> ( x => { LockUnlockBar(); }));
            }
        }

        /// <summary>
        /// The commands that handles the Onloaded event of the AppBar
        /// </summary>
        public ICommand OnLoadedCommand
        {
            get
            {
                return _onLoadedCommand ?? (_onLoadedCommand = new RelayCommand<object>(x => { Onloaded((RoutedEventArgs)x); }));
            }
        }

        /// <summary>
        /// The command to close the application
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand<object>(x => { Exit(); }));
            }
        }
        
        #endregion

        #region Private Methods 

        /// <summary>
        /// Calcuate the position and moves the AppBar
        /// </summary>
        /// <param name="args"> the new anchor location of the AppBar </param>
        private void BarChangeLocation(WindowLocation args)
        {
            double x = 0, y = 0;
            WindowHelpers.MovePosition(_xPosition, _yPosition, ref x, ref y, (WindowLocation)args, BarWidth, BarHeight);
            BarLocation = args;
            XPosition = x;
            YPosition = y;
        }

        /// <summary>
        /// Changes the orientation of the AppBar
        /// </summary>
        /// <param name="args"> The new Orientation </param>
        private void BarChangeOrientation(Orientation args)
        {
            BarOrientation = args;
        }

        /// <summary>
        /// Solves the mouse left button down gesture
        /// </summary>
        /// <param name="e"> The mouse property </param>
        private void SolveMouseLeftButtonGesture(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    BarChangeLocation(WindowLocation.TOP);
                }
                else
                {
                    if (!_isLocked)
                    {
                        Application.Current.MainWindow.DragMove();
                        if (BarLocation != WindowLocation.NONE)
                            BarLocation = WindowLocation.NONE;
                    }
                }
        }
       
        /// <summary>
        /// Updates the AppBar Size to the actual size
        /// </summary>
        /// <param name="args"></param>
        private void UpdateActualBarSize(SizeChangedEventArgs args)
        {
            BarWidth = args.NewSize.Width;
            BarHeight = args.NewSize.Height;
            if (BarLocation != WindowLocation.NONE)
            {
                BarChangeLocation(BarLocation);
            }
        }

        /// <summary>
        /// Set the initial AppBar size
        /// </summary>
        /// <param name="args"></param>
        private void Onloaded(RoutedEventArgs args)
        {
            BarWidth = ((Window)args.Source).Width;
            BarHeight = ((Window)args.Source).Height;
        }

        /// <summary>
        /// Locks or unlocks the AppBar
        /// </summary>
        private void LockUnlockBar()
        {
            if (_isLocked)
            {
                _isLocked = false;
                LockUnlockText = "Lock";
            }
            else
            {
                _isLocked = true;
                LockUnlockText = "Unlock";
            }
        }

        /// <summary>
        /// Closes the Application
        /// </summary>
        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Changes the Topmost Property
        /// </summary>
        /// <param name="args"> The checkbox which raises the event</param>
        private void TopmostChange(RoutedEventArgs args)
        {
            if (((CheckBox)args.Source).IsChecked == true)
            {
                IsOnTop = true;
            }
            else
                IsOnTop = false;
        }
        
        #endregion
    }
}
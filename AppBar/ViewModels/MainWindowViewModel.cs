using System.Windows;
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

        #region Constructor

        public MainWindowViewModel()
        {

            //BarMouseLeftButtonDownCommand = new RelayParameterizedCommand( param => SolveMouseGesture((MouseButtonEventArgs) param));
        }



        #endregion

        #region Private Properties

        private double _yPosition =0;
        private double _xPosition = 0;
        private int _contentPadding = 0;
        private int _borderSize = 3;
        private SolidColorBrush _borderColor = Brushes.GreenYellow;
        private SolidColorBrush _backgroundColor = Brushes.DarkSeaGreen;
        private int _barMinWidth = 16;
        private int _barMinHeight = 16;
        private int _barWidth = 32;
        private int _barHeight = 32;
        private WindowState _barWindowState;

        #endregion

        #region Private Commands
        private ICommand _hideAppBarCommand;
        private ICommand _showAppBarcommand;
        private ICommand _maximizeAppBarCommand;
        private ICommand _closeCommand;
        private ICommand _menuCommand;        
        private ICommand _barMouseRighttButtonDownCommand;
        private ICommand _barMouseLeftButtonDownCommand;
        private ICommand _barChangeLocationCommand;

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
        public int BarMinWidth
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
        public int BarMinHeight
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
        public int BarWidth
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
        public int BarHeight
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
        public int ContentPadding
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
        public int BorderSize
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

        public ICommand BarMouseRighttButtonDownCommand { get; set; }

        public ICommand BarMouseLeftButtonDownCommand
        {
            get
            {
                return _barMouseLeftButtonDownCommand ??
                    (_barMouseLeftButtonDownCommand = new RelayCommand<object>(x => { SolveMouseLeftButtonGesture(x as MouseButtonEventArgs); }));
            }
        }

        #endregion




        private void SolveMouseLeftButtonGesture(MouseButtonEventArgs e)
        {
            if (((MouseButtonEventArgs)e).ChangedButton == MouseButton.Left)
                if (((MouseButtonEventArgs)e).ClickCount == 2)
                {
                    YPosition = SystemParameters.VirtualScreenTop;
                    System.Windows.Forms.Screen s = Helpers.WindowHelpers.CurrentScreen(new System.Drawing.Point((int)_xPosition,(int)_yPosition));
                    XPosition = s.Bounds.X+ s.Bounds.Width/ 2 - ((double)BarWidth) / 2;
                    BarLocation = WindowLocation.TOP;
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                    if(BarLocation!=WindowLocation.NONE)
                        BarLocation = WindowLocation.NONE;
                }
        }

        public ICommand BarChangeLocationCommand
        {
            get {
                return _barChangeLocationCommand ??
                  (_barChangeLocationCommand = new RelayCommand<object>(x => { BarChangeLocation(x); }));
            }
        }

        private void BarChangeLocation(object args)
        {
            double x=0, y=0;
            WindowHelpers.MovePosition(_xPosition,_yPosition,ref x, ref y, (WindowLocation)args, BarWidth, BarHeight);
            XPosition = x;
            YPosition = y;
        }
    }
}
using AppBar.Core.Models;
using AppBar.Core.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AppBar.ViewModels
{
    /// <summary>
    /// ViewModel for each item hosted in the AppBar
    /// </summary>
    public class ItemViewModel : BaseViewModel
    {
        #region Private Properties

        private double _iconWidth;
        private double _iconHeight;
        private bool _isPinned = false;
        private Program _linkedProgram;
        private bool _isOpened=false;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Application Icon hosted by the Bar Item
        /// </summary>
        public BitmapImage IconSource {
            get => _linkedProgram.Icon;
            set
            {
                _linkedProgram.Icon = value;
                OnPropertyChanged(nameof(IconSource));
            }
        }

        /// <summary>
        /// The Application Name
        /// </summary>
        public string Name
        {
            get => LinkedProgram.Name;
            set
            {
                if (LinkedProgram.Name != value)
                {
                    LinkedProgram.Name = value;
                    OnPropertyChanged(Name);
                }
            }
        }

        /// <summary>
        /// tells if the application is pinned to the AooBar
        /// </summary>
        public bool IsPinned { get => _isPinned; set => _isPinned = value; }

        /// <summary>
        /// Icon Width
        /// </summary>
        public double IconWidth
        {
            get => _iconWidth;
            set
            {
                if (_iconWidth != value)
                {
                    _iconWidth = value;
                    OnPropertyChanged(nameof(IconWidth));
                }
            }
        }

        /// <summary>
        /// Icon Height
        /// </summary>
        public double IconHeight
        {
            get => _iconHeight;
            set
            {
                if (_iconHeight != value)
                {
                    _iconHeight = value;
                    OnPropertyChanged(nameof(IconHeight));
                }
            }
        }

        /// <summary>
        /// The Program linked with the item 
        /// </summary>
        public Program LinkedProgram
        {
            get => _linkedProgram;
            set
            {
                if (_linkedProgram != value)
                {
                    _linkedProgram = value;
                }
            }
        }

        /// <summary>
        /// Indicates if there is an instance of the Program running
        /// </summary>
        public bool IsOpened
        {
            get => _isOpened; set
            {
                if (_isOpened != value)
                {
                    _isOpened = value;
                    OnPropertyChanged(nameof(IsOpened));
                }
            }
        }

        #endregion

        #region Private Commands

        private ICommand _leftMouseButtonClickCommand;

        #endregion
        
        #region Public Commands

        public ICommand LeftMouseButtonClickCommand
        {
            get
            {
                return _leftMouseButtonClickCommand ??
                    (_leftMouseButtonClickCommand = new RelayCommand<object>(e => { SolveMouseLeftButtonGesture(e as MouseButtonEventArgs); }));
            }
        }
        #endregion
        
        #region Constructor

        /// <summary>
        /// Bar Item ViewModel Constructor
        /// </summary>
        /// <param name="isPinned">true if the item is pinned</param>
        /// <param name="linkedProgram"></param>
        /// <param name="iconWidth"></param>
        /// <param name="iconHeight"></param>
        public ItemViewModel()
        {
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Launches or resume a running Application
        /// </summary>
        /// <param name="e"></param>
        private void SolveMouseLeftButtonGesture(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                // TODO: launch application
                MessageBox.Show(LinkedProgram.Name, "Application launched", MessageBoxButton.OK);
        }

        #endregion
        
    }
}

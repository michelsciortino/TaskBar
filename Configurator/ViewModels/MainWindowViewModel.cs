using TaskBar.Core.Models;
using TaskBar.Core.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Configurator.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Private Properties

        private ThemeViewModel _themeVM;
        private BehaviorsViewModel _behaviorsVM;
        private PinnedItemsViewModel _pinnedItemsVM;
        private CustomIconsViewModel _customIconsVM;
        private Page _currentPage = null;
        private BitmapImage _imageSource = null;

        #endregion

        #region Public Properties

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        public ThemeViewModel ThemeSettingsVM => _themeVM ?? (_themeVM = new ThemeViewModel());
        public BehaviorsViewModel BehaviorsVM => _behaviorsVM ?? (_behaviorsVM = new BehaviorsViewModel());
        public PinnedItemsViewModel PinnedItemsVM => _pinnedItemsVM ?? (_pinnedItemsVM = new PinnedItemsViewModel());
        public CustomIconsViewModel CustomIconsVM => _customIconsVM ?? (_customIconsVM = new CustomIconsViewModel());

        public BitmapImage ImageSource {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            CurrentPage = new Views.Theme();
        }

        #endregion

        #region Private Commands

        private ICommand _saveCommand=null;
        private ICommand _reloadCommand = null;

        #endregion

        #region Public Commands

        public ICommand SaveCommand =>  _saveCommand ??
                                        (_saveCommand = new RelayCommand<object>((x) => Config.SaveConfiguration(App.Instance)));

        public ICommand ReloadCommand => _reloadCommand ??
                                        (_reloadCommand = new RelayCommand<object>((x) => LoadConfiguration()));

        #endregion

        #region methods
        private void LoadConfiguration()
        {
            App.Instance = Config.ReadConfiguration();
            ImageSource = App.Instance.CustomIcons[0];
        }
        #endregion
    }
}

using AppBar.Core.ViewModels;
using System.Windows.Controls;

namespace Configurator.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Private Properties

        private Page _currentPage;

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

        #endregion

        public MainWindowViewModel()
        {
            // TODO: creare la pagina delle settings
            CurrentPage = new Page();
        }

    }
}

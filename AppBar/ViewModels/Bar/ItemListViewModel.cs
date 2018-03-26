using System.Collections.Generic;
namespace AppBar.ViewModels
{
    /// <summary>
    /// ViewModel for each item hosted in the AppBar
    /// </summary>
    public class ItemListViewModel:BaseViewModel
    {
        #region Private Properties

        private List<ItemViewModel> _items;

        #endregion

        #region Public Properties

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

        #endregion

        #region Commands

        #endregion
        
    }
}

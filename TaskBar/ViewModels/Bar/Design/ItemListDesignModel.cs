using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using TaskBar.Core.Models;
using TaskBar.Helpers;

namespace TaskBar.ViewModels.Design
{
    /// <summary>
    /// ViewModel for each item hosted in the TaskBar
    /// </summary>
    public class ItemListDesignModel : ItemListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ItemListDesignModel Instance => new ItemListDesignModel();

        #endregion
                        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemListDesignModel()
        {
            //load design test data
            Items = new List<ItemViewModel>
            {
                new ItemViewModel(new Program
                    {
                        Name="notepad",
                        Icon=Config.UnknownImageSource_16x16,
                        Path="notepad.exe",
                    },16,16,false,true),
                new ItemViewModel(new Program
                    {
                        Name="snippet tool",
                        Icon=Config.UnknownImageSource_16x16,
                        Path="snippettool.exe",
                    },
                    16,
                    16,false,true
                ),
                new ItemViewModel(
                    new Program
                    {
                        Name="paint",
                        Icon=Config.UnknownImageSource_16x16,
                        Path="paint.exe",
                    },
                    16,
                    16,false,true),
                new ItemViewModel( new Program
                    {
                        Name="explorer",
                        Icon=Config.UnknownImageSource_16x16,
                        Path="explorer.exe",
                    },
                    16,
                    16,false,true),
            };
        }

        #endregion
    }
}

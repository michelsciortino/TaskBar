using System;
using AppBar.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace AppBar.ViewModels.Design
{
    /// <summary>
    /// ViewModel for each item hosted in the AppBar
    /// </summary>
    public class ItemDesignModel : ItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ItemDesignModel Instance => new ItemDesignModel();

        #endregion
        
        #region Constructor
        
        /// <summary>
        /// A test constructor
        /// </summary>
        public ItemDesignModel()
        {
            IsPinned = true;
            LinkedProgram = new Program {
                Name = "notepad",
                IsRunning = false,
                Icon = new BitmapImage(new Uri("C:/Users/Michel/source/repos/AppBar/AppBar/Images/siren.png")),
                ActiveProcesses = new List<System.Diagnostics.Process>(),
                Path = "notepad.exe",
            };
            IconWidth = 32;
            IconHeight = 32;
            IsOpened = false;
        }
        
        #endregion        
    }
}

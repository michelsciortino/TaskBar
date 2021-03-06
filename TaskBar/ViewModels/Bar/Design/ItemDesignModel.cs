﻿using System;
using TaskBar.Core.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using TaskBar.Core.Helpers;

namespace TaskBar.ViewModels.Design
{
    /// <summary>
    /// ViewModel for each item hosted in the TaskBar
    /// </summary>
    public class ItemDesignModel : ItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ItemDesignModel Instance => new ItemDesignModel();

        public Program Program { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// A test constructor
        /// </summary>
        public ItemDesignModel(){
            IsPinned = true;
            LinkedProgram = new Program {
                Name = "notepad",
                Icon = Config.UnknownImageSource_16x16,
                Path = "notepad.exe",
            };
            IconWidth = 32;
            IconHeight = 32;
            IsOpened = true;
        }
        
        #endregion        
    }
}

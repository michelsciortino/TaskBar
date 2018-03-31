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
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Program
                    {
                        Name="notepad",
                        IsRunning=false,
                        Icon=Config.UnknownImageSource_16x16,
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="notepad.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Program
                    {
                        Name="snippet tool",
                        IsRunning=false,
                        Icon=Config.UnknownImageSource_16x16,
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="snippettool.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Program
                    {
                        Name="paint",
                        IsRunning=false,
                        Icon=Config.UnknownImageSource_16x16,
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="paint.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Program
                    {
                        Name="explorer",
                        IsRunning=false,
                        Icon=Config.UnknownImageSource_16x16,
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="explorer.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
            };
        }

        #endregion
    }
}

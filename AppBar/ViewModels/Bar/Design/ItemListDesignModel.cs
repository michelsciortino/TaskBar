using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace AppBar.ViewModels.Design
{
    /// <summary>
    /// ViewModel for each item hosted in the AppBar
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
            Uri uri = new Uri("C:/Users/Michel/source/repos/AppBar/AppBar/Images/siren.png");
            //load design test data
            Items = new List<ItemViewModel>
            {
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Models.Program
                    {
                        Name="notepad",
                        IsRunning=false,
                        Icon=new BitmapImage(uri),
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="notepad.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Models.Program
                    {
                        Name="snippet tool",
                        IsRunning=false,
                        Icon=new BitmapImage(uri),
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="snippettool.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Models.Program
                    {
                        Name="paint",
                        IsRunning=false,
                        Icon=new BitmapImage(uri),
                        ActiveProcesses=new List<System.Diagnostics.Process>(),
                        Path="paint.exe",
                    },
                    IconWidth = 16,
                    IconHeight= 16
                },
                new ItemViewModel
                {
                    IsPinned=true,
                    LinkedProgram = new Models.Program
                    {
                        Name="explorer",
                        IsRunning=false,
                        Icon=new BitmapImage(uri),
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

using AppBar.Models;
using AppBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppBar
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Default configuration filename
        /// </summary>
        private const string ConfigFilename= ".configuration";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string WorkingDirectoryPath=Assembly.GetExecutingAssembly().Location;

            Config.ReadConfiguration(WorkingDirectoryPath + ConfigFilename);
            
            MainWindow AppBar = new MainWindow();

            //Showing the main window
            AppBar.Show();

            //Saving current configuration instance
            Config.SaveConfiguration(WorkingDirectoryPath + ConfigFilename);
        }

    }
}

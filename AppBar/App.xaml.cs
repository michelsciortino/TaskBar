using AppBar.Core.Models;
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
        public static Config Instance = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Instance=Config.ReadConfiguration();
            
            MainWindow AppBar = new MainWindow();

            //Showing the main window
            AppBar.Show();
        }

    }
}

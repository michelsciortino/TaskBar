using AppBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppBar
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Uri uri = new Uri("C:/Users/Michel/source/repos/AppBar/AppBar/Images/siren.png");

            //showing the main window
            MainWindow AppBar = new MainWindow();

            //loading some test content
            ((MainWindowViewModel)AppBar.DataContext).Items = new List<ItemViewModel>()
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
                    IconHeight= 16,
                    IsOpened = false
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
                    IconHeight= 16,
                    IsOpened = true
                },
            };

            AppBar.Show();
        }

    }
}

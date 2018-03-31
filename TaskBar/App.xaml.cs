using TaskBar.Core.Models;
using System.Windows;
using TaskBar.Core;

namespace TaskBar
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
            
            MainWindow TaskBar = new MainWindow();

            //Showing the main window
            TaskBar.Show();
        }
    }
}

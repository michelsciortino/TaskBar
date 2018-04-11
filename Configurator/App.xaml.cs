using TaskBar.Core.Models;
using System.Windows;

namespace Configurator
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Config Configuration = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration = Config.ReadConfiguration();
            MainWindow Configurator = new MainWindow();

            //Showing the main window
            Configurator.Show();
        }
    }
}

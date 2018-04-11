using TaskBar.Core.Models;
using System.Windows;
using static TaskBar.Core.WinApi.ShellApi;
using System;
using System.Windows.Interop;
using TaskBar.ViewModels;
using System.Windows.Media;
using TaskBar.Helpers;

namespace TaskBar
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Config Config = null;
        private IntPtr hwnd;
        private HwndSource hsource;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Config=Config.ReadConfiguration();
            
            MainWindow TaskBar = new MainWindow();
            TaskBar.SourceInitialized += TaskBar_SourceInitialized;

            //Showing the main window
            TaskBar.Show();
        }

        private void TaskBar_SourceInitialized(object sender, EventArgs e)
        {
            if ((hwnd = new WindowInteropHelper((Window)sender).Handle) == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not get window handle.");
            }
            hsource = HwndSource.FromHwnd(hwnd);
            hsource.AddHook(WndProc);
        }

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case (int)WindowMessage.WM_DWMCOLORIZATIONCOLORCHANGED:
                    if(Config.BackgroundAsSysColor)
                        ((MainWindowViewModel)MainWindow.DataContext).BackgroundColor =
                            new SolidColorBrush(ColorsHelper.ColorFromHex(
                                Core.WinApi.NativeMethods.GetWindowColorizationColor(true)));
                    if (Config.AccentAsSysColor)
                        ((MainWindowViewModel)MainWindow.DataContext).AccentColor =
                            new SolidColorBrush(ColorsHelper.ColorFromHex(
                                Core.WinApi.NativeMethods.GetWindowColorizationColor(true)));
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

    }
}

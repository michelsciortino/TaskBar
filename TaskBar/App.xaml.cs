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
    public partial class App : Application 
    {
        public static Config Configuration = null;
        private IntPtr hwnd;
        private HwndSource hsource;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Loading configuration
            Configuration=Config.ReadConfiguration();
            
            MainWindow TaskBar = new MainWindow();
            TaskBar.SourceInitialized += TaskBar_SourceInitialized;

            //Showing the main window
            TaskBar.Show();
        }

        /// <summary>
        /// Gets a Handle for the current window and adds a hook for listening to Windows Messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskBar_SourceInitialized(object sender, EventArgs e)
        {
            //Getting a Handle for this window
            if ((hwnd = new WindowInteropHelper((Window)sender).Handle) == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not get window handle.");
            }
            hsource = HwndSource.FromHwnd(hwnd);

            //Adding a Hook to the window handle's source
            hsource.AddHook(WndProc);
        }

        /// <summary>
        /// Implementation of the WinProc procedure for listening to Windows Messages
        /// </summary>
        /// <param name="hwnd">The window's Handle</param>
        /// <param name="msg">The received message</param>
        /// <param name="wParam">A param of the message</param>
        /// <param name="lParam">A param of the message</param>
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                //If Windows theme color has changed
                case (int)WindowMessage.WM_DWMCOLORIZATIONCOLORCHANGED:
                    if(Configuration.BackgroundAsSysColor) //updating TaskBar background
                        ((MainWindowViewModel)MainWindow.DataContext).BackgroundColor =
                            new SolidColorBrush(ColorsHelper.ColorFromHex(
                                Core.WinApi.NativeMethods.GetWindowColorizationColor(true)));
                    if (Configuration.AccentAsSysColor) //updating AccentColor
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

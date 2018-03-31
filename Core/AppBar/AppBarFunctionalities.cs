using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using static TaskBar.Core.WinApi.ShellApi;

namespace TaskBar.Core
{
    public class AppBarFunctionalities
    {
        #region Private Properties

        private static int nRegisteredWindowInfo;
        private delegate void ResizeDelegate(Window appbarWindow, Rect rect);
        private RegisterInfo Info;

        #endregion

        #region Constructor

        /// <summary>
        /// Provides AppBar Functionalities to a Window
        /// </summary>
        /// <param name="window"> The window to handle </param>
        public AppBarFunctionalities(Window window)
        {
            Info = new RegisterInfo
            {
                Window = window,
                Handle = new WindowInteropHelper(window).Handle,
                IsRegistered = false,
                CallbackId = -1,
                Position = AppBarDockPosition.Float,
                Coordinates = new Point(int.MinValue, int.MinValue)
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Window Messages Listener
        /// </summary>
        /// <param name="hwnd"> Window Handle </param>
        /// <param name="msg"> Received Message to act upon </param>
        /// <param name="wParam"> Window Message Parameter </param>
        /// <param name="lParam"> Window Message Parameter </param>
        /// <param name="handled"> True if Whether events resulting should be marked handled </param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == Info.CallbackId)
            {
                if (wParam.ToInt32() == (int)AppBarNotification.POSCHANGED)
                {
                    AppBarSetPos();
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        private void AppBarSetPos()
        {
            var barData = new APPBARDATA();
            barData.cbSize = Marshal.SizeOf(barData);
            barData.hWnd = Info.Handle;
            barData.uEdge = (int)Info.Position;

            // Setting Position Rectangle
            SetDestinationRect(ref barData);
            SHAppBarMessage(AppBarMessage.SETPOS, ref barData);

            Info.Window.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                new ResizeDelegate(DoResize), Info.Window, new Rect(Info.Coordinates.X, Info.Coordinates.Y, Info.Window.ActualWidth, Info.Window.ActualHeight));
        }

        private void SetDestinationRect(ref APPBARDATA barData)
        {
            Screen screen = Screen.FromHandle(Info.Handle);

            // Transforms a coordinate from WPF space to Screen space
            var toPixel = PresentationSource.FromVisual(Info.Window).CompositionTarget.TransformToDevice;
            // Transforms a coordinate from Screen space to WPF space
            var toWpfUnit = PresentationSource.FromVisual(Info.Window).CompositionTarget.TransformFromDevice;

            // Transform window size from wpf units (1/96 ") to real pixels, for win32 usage
            var sizeInPixels = toPixel.Transform(new Vector(Info.Window.ActualWidth, Info.Window.ActualHeight));
            var screenSizeInPixels = toPixel.Transform(new Vector(screen.Bounds.Width, screen.Bounds.Height));

            RECT screenBounds = GetRectBounds(screen.Bounds);
            barData.rc = screenBounds;
            //Getting Display avaible workarea to dock the ApppBar (takes care of Window Taskbar dock)
            SHAppBarMessage(AppBarMessage.QUERYPOS, ref barData);

            switch (Info.Position)
            {
                case AppBarDockPosition.Left:
                    barData.rc.right = barData.rc.left + (int)Math.Round(sizeInPixels.X);
                    barData.rc.top = (int)(barData.rc.top + (double)barData.rc.Height / 2 - Info.Window.Height / 2);
                    barData.rc.bottom = barData.rc.top + (int)Info.Window.Height;
                    break;
                case AppBarDockPosition.Top:
                    barData.rc.bottom = barData.rc.top + (int)Math.Round(sizeInPixels.Y);
                    barData.rc.left = barData.rc.left + (int)((double)barData.rc.Width / 2 - Info.Window.Width / 2);
                    barData.rc.right = barData.rc.left + (int)Info.Window.Width;
                    break;
                case AppBarDockPosition.Right:
                    barData.rc.left = barData.rc.right - (int)Math.Round(sizeInPixels.X);
                    barData.rc.top = (int)(barData.rc.top + (double)barData.rc.Height / 2 - Info.Window.Height / 2);
                    barData.rc.bottom = barData.rc.top + (int)Info.Window.Height;
                    break;
                case AppBarDockPosition.Bottom:
                    barData.rc.top = barData.rc.bottom - (int)Math.Round(sizeInPixels.Y);
                    barData.rc.left = barData.rc.left + (int)((double)barData.rc.Width / 2 - Info.Window.Width / 2);
                    barData.rc.right = barData.rc.left + (int)Info.Window.Width;
                    break;
            }

            Info.Coordinates = new Point(barData.rc.left, barData.rc.top);
        }

        private static void DoResize(Window appbarWindow, Rect rect)
        {
            appbarWindow.Width = rect.Width;
            appbarWindow.Height = rect.Height;
            appbarWindow.Top = rect.Top;
            appbarWindow.Left = rect.Left;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes AppBar Location 
        /// </summary>
        /// <param name="position"> Position where to dock the AppBar </param>
        public void SetAppBar(AppBarDockPosition position)
        {
            // If the AppBar is currently not docked => return
            if (position == AppBarDockPosition.Float && Info.Position == AppBarDockPosition.Float)
                return;

            APPBARDATA abd;
            abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = Info.Handle;
            abd.uEdge = (int)Info.Position;
            int renderPolicy;

            // If the AppBar has to be Undocked           
            if (position == AppBarDockPosition.Float)
            {
                Info.Position = position;
                // If the AppBar is currently Docked => remove Dock
                if (Info.IsRegistered)
                {
                    uint ret= SHAppBarMessage(AppBarMessage.REMOVE, ref abd);
                    if (ret == 1)
                    {
                        Info.IsRegistered = false;
                    }
                    else
                        throw new SystemException("Failed to Undock the window");
                }
                // Restore normal desktop window manager attributes
                renderPolicy = (int)DWMNCRenderingPolicy.UseWindowStyle;
                DwmSetWindowAttribute(abd.hWnd, (int)DWMWINDOWATTRIBUTE.DWMA_EXCLUDED_FROM_PEEK, ref renderPolicy, sizeof(int));
                DwmSetWindowAttribute(abd.hWnd, (int)DWMWINDOWATTRIBUTE.DWMA_DISALLOW_PEEK, ref renderPolicy, sizeof(int));
                return;
            }
            Info.Position = position;
            
            
            if (!Info.IsRegistered)
            {
                Info.IsRegistered = true;
                Info.CallbackId = RegisterWindowMessage($"AppBarMessage{nRegisteredWindowInfo++}");
                abd.uCallbackMessage = Info.CallbackId;

                uint ret = SHAppBarMessage((int)AppBarMessage.NEW, ref abd);

                if (ret == 1)
                {
                    HwndSource source = HwndSource.FromHwnd(Info.Handle);
                    source.AddHook(WndProc);
                }
                else
                    throw new SystemException("Failed to hook the window");
            }

            // Set desktop window manager attributes to prevent window
            // from being hidden when peeking at the desktop or when
            // the 'show desktop' button is pressed
            renderPolicy = (int)DWMNCRenderingPolicy.Enabled;

            DwmSetWindowAttribute(abd.hWnd, (int)DWMWINDOWATTRIBUTE.DWMA_EXCLUDED_FROM_PEEK, ref renderPolicy, sizeof(int));
            DwmSetWindowAttribute(abd.hWnd, (int)DWMWINDOWATTRIBUTE.DWMA_DISALLOW_PEEK, ref renderPolicy, sizeof(int));

            AppBarSetPos();
        }

        #endregion        

        #region Helpers

        private static RECT GetRectBounds(System.Drawing.Rectangle screenBounds)
        {
            RECT rc = new RECT
            {
                left = screenBounds.Left,
                right = screenBounds.Right,
                top = screenBounds.Top,
                bottom = screenBounds.Bottom
            };
            return rc;
        }

        #endregion

        /*Future features
         */
        /// <summary>
        /// Dictionary to save registered AppBars Infos
        /// </summary>
        /*private static readonly Dictionary<Window, RegisterInfo> RegisteredWindowInfo = 
         * new Dictionary<Window, RegisterInfo>();*/
        /// <summary>
        /// Get the RegisterInfo of a Registed Window of the same program
        /// </summary>
        /// <param name="wnd"> Window to get Info of </param>
        /// <returns></returns>
        /*public static RegisterInfo GetRegisterInfo(Window wnd)
        {
            if (RegisteredWindowInfo.ContainsKey(wnd))
            {
                return RegisteredWindowInfo[wnd];
            }
            return null;
        }
        */
    }

    public class RegisterInfo
    {
        public int CallbackId { get; set; }
        public Window Window { get; set; }
        public AppBarDockPosition Position { get; set; }
        public Point Coordinates { get; set; }
        public bool IsRegistered { get; set; }
        public IntPtr Handle { get; set; }
    }
}

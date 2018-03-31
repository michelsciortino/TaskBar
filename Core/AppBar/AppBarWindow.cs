using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using TaskBar.Core.WinApi;
using static TaskBar.Core.WinApi.ShellApi;

namespace TaskBar.Core
{
    public class AppBarWindow : Window
    {
        #region Private Properties

        private int _AppBarMessageId;
        private bool IsAppBarRegistered;
        private bool IsInAppBarResize;
        private Rect WindowBounds
        {
            set
            {
                this.Left = DesktopDimensionToWpf(value.Left);
                this.Top = DesktopDimensionToWpf(value.Top);
                this.Width = DesktopDimensionToWpf(value.Width);
                this.Height = DesktopDimensionToWpf(value.Height);
            }
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Dock Position of the AppBar
        /// </summary>
        public AppBarDockPosition DockPosition
        {
            get { return (AppBarDockPosition)GetValue(DockPositionProperty); }
            set { SetValue(DockPositionProperty, value); }
        }

        /// <summary>
        /// Current Monitor
        /// </summary>
        public MonitorInfo Monitor
        {
            get { return (MonitorInfo)GetValue(MonitorProperty); }
            set { SetValue(MonitorProperty, value); }
        }

        public int DockedWidthOrHeight
        {
            get { return (int)GetValue(DockedWidthOrHeightProperty); }
            set { SetValue(DockedWidthOrHeightProperty, value); }
        }

        public int AppBarMessageId
        {
            get
            {
                if (_AppBarMessageId == 0)
                {
                    _AppBarMessageId = RegisterWindowMessage("AppBarMessage_EEDFB5206FC3");
                }

                return _AppBarMessageId;
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DockPositionProperty =
           DependencyProperty.Register("DockMode", typeof(AppBarDockPosition), typeof(AppBarWindow),
               new FrameworkPropertyMetadata(AppBarDockPosition.Left, DockLocation_Changed));

        public static readonly DependencyProperty MonitorProperty =
           DependencyProperty.Register("Monitor", typeof(MonitorInfo), typeof(AppBarWindow),
               new FrameworkPropertyMetadata(null, DockLocation_Changed));

        public static readonly DependencyProperty DockedWidthOrHeightProperty =
            DependencyProperty.Register("DockedWidthOrHeight", typeof(int), typeof(AppBarWindow),
                new FrameworkPropertyMetadata(200, DockLocation_Changed, DockedWidthOrHeight_Coerce));

        #endregion

        #region Callbacks

        private static void DockLocation_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var @this = (AppBarWindow)d;

            if (@this.IsAppBarRegistered)
            {
                @this.OnDockLocationChanged();
            }
        }

        private static void MinMaxHeightWidth_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(DockedWidthOrHeightProperty);
        }

        private static object DockedWidthOrHeight_Coerce(DependencyObject d, object baseValue)
        {
            var @this = (AppBarWindow)d;
            var newValue = (int)baseValue;

            switch (@this.DockPosition)
            {
                case AppBarDockPosition.Left:
                case AppBarDockPosition.Right:
                    return BoundIntToDouble(newValue, @this.MinWidth, @this.MaxWidth);

                case AppBarDockPosition.Top:
                case AppBarDockPosition.Bottom:
                    return BoundIntToDouble(newValue, @this.MinHeight, @this.MaxHeight);

                default: throw new NotSupportedException();
            }
        }

        #endregion

        #region Public Methods

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case (int)WindowMessage.WM_MOUSEMOVE:
                    System.Diagnostics.Debug.WriteLine($"{msg} WM_MOUSEMOVE");
                    break;
                case (int)WindowMessage.WM_LBUTTONDOWN:
                    System.Diagnostics.Debug.WriteLine($"{msg} WM_LBUTTONDOWN");
                    DragMove();
                    break;
                case (int)WindowMessage.WM_LBUTTONDBLCLK:
                    System.Diagnostics.Debug.WriteLine($"{msg} WM_LBUTTONDBLCLK");
                    break;
            }
            string val = msg.ToString("X");
            if (msg == (int)WindowMessage.WM_WINDOWPOSCHANGING && !IsInAppBarResize)
            {
                var wp = Marshal.PtrToStructure<WINDOWPOS>(lParam);
                wp.flags |= SWP_NOMOVE | SWP_NOSIZE;
                Marshal.StructureToPtr(wp, lParam, false);
            }
            else if (msg == (int)WindowMessage.WM_ACTIVATE)
            {
                var abd = GetAppBarData();
                SHAppBarMessage(AppBarMessage.ACTIVATE, ref abd);
            }
            else if (msg == (int)WindowMessage.WM_WINDOWPOSCHANGED)
            {
                var abd = GetAppBarData();
                SHAppBarMessage(AppBarMessage.WINDOWPOSCHANGED, ref abd);
            }
            else if (msg == AppBarMessageId)
            {
                switch ((AppBarNotification)(int)wParam)
                {
                    case AppBarNotification.POSCHANGED:
                        OnDockLocationChanged();
                        handled = true;
                        break;
                }
            }

            return IntPtr.Zero;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Bounds a <see cref="int"/> value between <see cref="double"/> min and <see cref="double"/> max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>The bounded value as </returns>
        private static int BoundIntToDouble(int value, double min, double max)
        {
            if (min > value)
            {
                return (int)Math.Ceiling(min);
            }
            if (max < value)
            {
                return (int)Math.Floor(max);
            }

            return value;
        }

        /// <summary>
        /// Scales a desktop dimension for the current monitor DPI
        /// </summary>
        /// <param name="dim">Dimension to scale</param>
        /// <returns>A scaled dimension</returns>
        private double DesktopDimensionToWpf(double dim)
        {
            var dpi = VisualTreeHelper.GetDpi(this);
            return dim / dpi.PixelsPerDip;
        }

        /// <summary>
        /// Scales a wpf dimension for the current monitor DPI
        /// </summary>
        /// <param name="dim">Dimension to scale</param>
        /// <returns>A scaled dimension</returns>
        private int WpfDimensionToDesktop(double dim)
        {
            var dpi = VisualTreeHelper.GetDpi(this);
            return (int)Math.Ceiling(dim * dpi.PixelsPerDip);
        }
        
        /// <summary>
        /// Returns the current monitor; if the monitor has not been selected yet, it returns the primary monitor
        /// </summary>
        /// <returns></returns>
        private MonitorInfo GetSelectedMonitor()
        {
            var monitor = this.Monitor;
            var allMonitors = MonitorInfo.GetAllMonitors();
            if (monitor == null || !allMonitors.Contains(monitor))
            {
                monitor = allMonitors.First(f => f.IsPrimary);
            }
            return monitor;
        }

        /// <summary>
        /// Returns a new AppBarData structure based on current configuration
        /// </summary>
        /// <returns>A new AppBarData structure</returns>
        private APPBARDATA GetAppBarData()
        {
            return new APPBARDATA()
            {
                cbSize = Marshal.SizeOf(typeof(APPBARDATA)),
                hWnd = new WindowInteropHelper(this).Handle,
                uCallbackMessage = AppBarMessageId,
                uEdge = (int)DockPosition
            };
        }

        /// <summary>
        /// Request a new position for the docked AppBar to the System
        /// </summary>
        private void OnDockLocationChanged()
        {
            if (IsInAppBarResize)
            {
                return;
            }

            var abd = GetAppBarData();
            abd.rc = (RECT)GetSelectedMonitor().ViewportBounds;

            SHAppBarMessage(AppBarMessage.QUERYPOS, ref abd);

            var dockedWidthOrHeightInDesktopPixels = WpfDimensionToDesktop(DockedWidthOrHeight);
            switch (DockPosition)
            {
                case AppBarDockPosition.Top:
                    abd.rc.bottom = abd.rc.top + dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockPosition.Bottom:
                    abd.rc.top = abd.rc.bottom - dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockPosition.Left:
                    abd.rc.right = abd.rc.left + dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockPosition.Right:
                    abd.rc.left = abd.rc.right - dockedWidthOrHeightInDesktopPixels;
                    break;
                default: throw new NotSupportedException();
            }

            SHAppBarMessage(AppBarMessage.SETPOS, ref abd);
            IsInAppBarResize = true;
            try
            {
                WindowBounds = (Rect)abd.rc;
            }
            finally
            {
                IsInAppBarResize = false;
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Throwing the hook to listen for WindowMessages
        /// </summary>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var source = (HwndSource)PresentationSource.FromVisual(this);
            source.AddHook(WndProc);

            var abd = GetAppBarData();
            SHAppBarMessage(AppBarMessage.NEW, ref abd);

            this.IsAppBarRegistered = false;
            //OnDockLocationChanged();
        }

        /// <summary>
        /// Unregistering the AppBar 
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (IsAppBarRegistered)
            {
                var abd = GetAppBarData();
                var ret=SHAppBarMessage(AppBarMessage.REMOVE, ref abd);
                IsAppBarRegistered = false;
            }
        }

        /// <summary>
        /// Rescaling the AppBar when Display DPI changes
        /// </summary>
        /// <param name="oldDpi">Old Display DPI</param>
        /// <param name="newDpi">New Display DPI</param>
        protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
        {
            base.OnDpiChanged(oldDpi, newDpi);
            if (IsAppBarRegistered)
                OnDockLocationChanged();
        }

        #endregion
    }
}      
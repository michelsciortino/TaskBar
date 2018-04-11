using System;
using System.Drawing;
using System.Runtime.InteropServices;
using static TaskBar.Core.WinApi.ShellApi;

namespace TaskBar.Core.WinApi
{
    public static class NativeMethods
    {
        const string User32 = "user32.dll";
        const string Shell32 = "shell32.dll";
        internal struct DWM_COLORIZATION_PARAMS
        {
            public uint ColorizationColor;
            public uint ColorizationAfterglow;
            public uint ColorizationColorBalance;
            public uint ColorizationAfterglowBalnce;
            public uint ColorizationBlurBalance;
            public uint ColorizationGlassReflectionIntensity;
            public uint ColorizationOpaqueBlend;
        }
        
        #region Native Methods

        /// <summary>
        /// In WM_SYSCOMMAND messages, the four low-order bits of the wParam parameter are used internally by
        /// the system. To obtain the correct result when testing the value of wParam, an application must
        /// combine the value 0xFFF0 with the wParam value by using the bitwise AND operator.
        /// </summary>
        /// <param name="wparam">window message param</param>
        /// <returns></returns>
        public static int SC_FROM_WPARAM(IntPtr wparam)
        {
            return ((int)wparam & 0xfff0);
        }

        /// <summary>
        /// Sends an appbar message to the system.
        /// </summary>
        /// <param name="dwMessage"></param>
        /// <param name="pData"></param>
        /// <returns></returns>
        [DllImport(Shell32, CallingConvention = CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(AppBarMessage dwMessage, ref APPBARDATA pData);

        /// <summary>
        /// Defines a new window message that is guaranteed to be unique throughout the system. The message value can be used when sending or posting messages.
        /// </summary>
        /// <param name="msg">The message to be registered.</param>
        /// <returns>
        /// If the message is successfully registered, the return value is a message identifier in the range 0xC000 through 0xFFFF.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(User32, CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
        /// These windows are ordered according to their appearance on the screen.
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        /// A handle to the window to precede the positioned window in the Z order.
        /// This parameter must be a window handle or one of the following values.
        /// </param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels.</param>
        /// <param name="flags">
        /// The window sizing and positioning flags.
        /// This parameter can be a combination of the following values.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(User32, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        /// <summary>
        /// The GetMonitorInfo function retrieves information about a display monitor.
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="lpmi">
        /// A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.
        /// You must set the cbSize member of the structure to sizeof(MONITORINFO) or sizeof(MONITORINFOEX) before calling
        /// the GetMonitorInfo function.Doing so lets the function determine the type of structure you are passing to it.
        /// The MONITORINFOEX structure is a superset of the MONITORINFO structure. It has one additional member:
        /// a string that contains a name for the display monitor.Most applications have no use for a display monitor name,
        /// and so can save some bytes by using a MONITORINFO structure.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        ///If the function fails, the return value is zero.
        ///</returns>
        [DllImport(User32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

        /// <summary>
        /// The EnumDisplayMonitors function enumerates display monitors
        /// (including invisible pseudo-monitors associated with the mirroring drivers)
        /// that intersect a region formed by the intersection of a specified clipping rectangle
        /// and the visible region of a device context. EnumDisplayMonitors calls
        /// an application-defined MonitorEnumProc callback function once for each monitor that is enumerated.
        /// Note that GetSystemMetrics (SM_CMONITORS) counts only the display monitors.
        /// </summary>
        /// <param name="hdc">
        /// A handle to a display device context that defines the visible region of interest.
        /// If this parameter is NULL, the hdcMonitor parameter passed to the callback function will be NULL,
        /// and the visible region of interest is the virtual screen that encompasses all the displays on the desktop.
        ///</param>
        /// <param name="lprcClip">
        /// A pointer to a RECT structure that specifies a clipping rectangle.
        /// The region of interest is the intersection of the clipping rectangle with the visible region specified by hdc.
        /// If hdc is non-NULL, the coordinates of the clipping rectangle are relative to the origin of the hdc.
        /// If hdc is NULL, the coordinates are virtual-screen coordinates.
        /// This parameter can be NULL if you don't want to clip the region specified by hdc.
        /// </param>
        /// <param name="lpfnEnum">A pointer to a MonitorEnumProc application-defined callback function.</param>
        /// <param name="dwData">Application-defined data that EnumDisplayMonitors passes directly to the MonitorEnumProc function.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(User32)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false)]
        internal static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);

        public static string GetWindowColorizationColor(bool opaque)
        {
            DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);
            Color ret= Color.FromArgb(  (byte)(opaque ? 255 : parameters.ColorizationColor >> 24),
                                    (byte)(parameters.ColorizationColor >> 16),
                                    (byte)(parameters.ColorizationColor >> 8),
                                    (byte)parameters.ColorizationColor);
            return ColorTranslator.ToHtml(ret);
        }

        #endregion
    }

}

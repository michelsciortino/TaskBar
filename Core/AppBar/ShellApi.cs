using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace TaskBar.Core.WinApi
{
    public static class ShellApi
    {
        #region Constants

        const string User32 = "user32.dll";
        const string Shell32 = "shell32.dll";

        public const int
            SWP_NOMOVE = 0x0002,
            SWP_NOSIZE = 0x0001;

        public const int
            SC_MOVE = 0xF010;

        private const int
            CCHDEVICENAME = 32;

        #endregion

        #region Enums

        public enum WindowMessage
	    {
		    WM_NULL                        = 0x0000,
		    WM_CREATE                      = 0x0001,
		    WM_DESTROY                     = 0x0002,
		    WM_MOVE                        = 0x0003,
		    WM_SIZE                        = 0x0005,
		    WM_ACTIVATE                    = 0x0006, //Sent to both the window being activated and the window being deactivated.
            WM_SETFOCUS                    = 0x0007,
		    WM_KILLFOCUS                   = 0x0008,
		    WM_ENABLE                      = 0x000A,
		    WM_SETREDRAW                   = 0x000B,
		    WM_SETTEXT                     = 0x000C,
		    WM_GETTEXT                     = 0x000D,
		    WM_GETTEXTLENGTH               = 0x000E,
		    WM_PAINT                       = 0x000F,
		    WM_CLOSE                       = 0x0010,
		    WM_QUERYENDSESSION             = 0x0011,
		    WM_QUERYOPEN                   = 0x0013,
		    WM_ENDSESSION                  = 0x0016,
		    WM_QUIT                        = 0x0012,
		    WM_ERASEBKGND                  = 0x0014,
		    WM_SYSCOLORCHANGE              = 0x0015,
		    WM_SHOWWINDOW                  = 0x0018,
		    WM_WININICHANGE                = 0x001A,
		    WM_SETTINGCHANGE               = WM_WININICHANGE,
		    WM_DEVMODECHANGE               = 0x001B,
		    WM_ACTIVATEAPP                 = 0x001C,
		    WM_FONTCHANGE                  = 0x001D,
		    WM_TIMECHANGE                  = 0x001E,
		    WM_CANCELMODE                  = 0x001F,
		    WM_SETCURSOR                   = 0x0020,
		    WM_MOUSEACTIVATE               = 0x0021,
		    WM_CHILDACTIVATE               = 0x0022,
		    WM_QUEUESYNC                   = 0x0023,
		    WM_GETMINMAXINFO               = 0x0024,
		    WM_PAINTICON                   = 0x0026,
		    WM_ICONERASEBKGND              = 0x0027,
		    WM_NEXTDLGCTL                  = 0x0028,
		    WM_SPOOLERSTATUS               = 0x002A,
		    WM_DRAWITEM                    = 0x002B,
		    WM_MEASUREITEM                 = 0x002C,
		    WM_DELETEITEM                  = 0x002D,
		    WM_VKEYTOITEM                  = 0x002E,
		    WM_CHARTOITEM                  = 0x002F,
		    WM_SETFONT                     = 0x0030,
		    WM_GETFONT                     = 0x0031,
		    WM_SETHOTKEY                   = 0x0032,
		    WM_GETHOTKEY                   = 0x0033,
		    WM_QUERYDRAGICON               = 0x0037,
		    WM_COMPAREITEM                 = 0x0039,
		    WM_GETOBJECT                   = 0x003D,
		    WM_COMPACTING                  = 0x0041,
		    WM_COMMNOTIFY                  = 0x0044,
		    WM_WINDOWPOSCHANGING           = 0x0046,
		    WM_WINDOWPOSCHANGED            = 0x0047,     //Sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
            WM_POWER                       = 0x0048,
		    WM_COPYDATA                    = 0x004A,
		    WM_CANCELJOURNAL               = 0x004B,
		    WM_NOTIFY                      = 0x004E,
		    WM_INPUTLANGCHANGEREQUEST      = 0x0050,
		    WM_INPUTLANGCHANGE             = 0x0051,
		    WM_TCARD                       = 0x0052,
		    WM_HELP                        = 0x0053,
		    WM_USERCHANGED                 = 0x0054,
		    WM_NOTIFYFORMAT                = 0x0055,
		    WM_CONTEXTMENU                 = 0x007B,
		    WM_STYLECHANGING               = 0x007C,
		    WM_STYLECHANGED                = 0x007D,
		    WM_DISPLAYCHANGE               = 0x007E,
		    WM_GETICON                     = 0x007F,
		    WM_SETICON                     = 0x0080,
		    WM_NCCREATE                    = 0x0081,
		    WM_NCDESTROY                   = 0x0082,
		    WM_NCCALCSIZE                  = 0x0083,
		    WM_NCHITTEST                   = 0x0084,    //Sent to a window in order to determine what part of the window corresponds to a particular screen coordinate. 
            WM_NCPAINT                     = 0x0085,
		    WM_NCACTIVATE                  = 0x0086,
		    WM_GETDLGCODE                  = 0x0087,
		    WM_SYNCPAINT                   = 0x0088,
		    WM_NCMOUSEMOVE                 = 0x00A0,
		    WM_NCLBUTTONDOWN               = 0x00A1,
		    WM_NCLBUTTONUP                 = 0x00A2,
		    WM_NCLBUTTONDBLCLK             = 0x00A3,
		    WM_NCRBUTTONDOWN               = 0x00A4,
		    WM_NCRBUTTONUP                 = 0x00A5,
		    WM_NCRBUTTONDBLCLK             = 0x00A6,
		    WM_NCMBUTTONDOWN               = 0x00A7,
		    WM_NCMBUTTONUP                 = 0x00A8,
		    WM_NCMBUTTONDBLCLK             = 0x00A9,
		    WM_NCXBUTTONDOWN               = 0x00AB,
		    WM_NCXBUTTONUP                 = 0x00AC,
		    WM_NCXBUTTONDBLCLK             = 0x00AD,
		    WM_INPUT_DEVICE_CHANGE         = 0x00FE,
		    WM_INPUT                       = 0x00FF,
		    WM_KEYFIRST                    = 0x0100,
		    WM_KEYDOWN                     = 0x0100,
		    WM_KEYUP                       = 0x0101,
		    WM_CHAR                        = 0x0102,
		    WM_DEADCHAR                    = 0x0103,
		    WM_SYSKEYDOWN                  = 0x0104,
		    WM_SYSKEYUP                    = 0x0105,
		    WM_SYSCHAR                     = 0x0106,
		    WM_SYSDEADCHAR                 = 0x0107,
		    WM_UNICHAR                     = 0x0109,
		    WM_KEYLAST                     = 0x0109,
		    WM_IME_STARTCOMPOSITION        = 0x010D,
		    WM_IME_ENDCOMPOSITION          = 0x010E,
		    WM_IME_COMPOSITION             = 0x010F,
		    WM_IME_KEYLAST                 = 0x010F,
		    WM_INITDIALOG                  = 0x0110,
		    WM_COMMAND                     = 0x0111,
		    WM_SYSCOMMAND                  = 0x0112,
		    WM_TIMER                       = 0x0113,
		    WM_HSCROLL                     = 0x0114,
		    WM_VSCROLL                     = 0x0115,
		    WM_INITMENU                    = 0x0116,
		    WM_INITMENUPOPUP               = 0x0117,
		    WM_MENUSELECT                  = 0x011F,
		    WM_MENUCHAR                    = 0x0120,
		    WM_ENTERIDLE                   = 0x0121,
		    WM_MENURBUTTONUP               = 0x0122,
		    WM_MENUDRAG                    = 0x0123,
		    WM_MENUGETOBJECT               = 0x0124,
		    WM_UNINITMENUPOPUP             = 0x0125,
		    WM_MENUCOMMAND                 = 0x0126,
		    WM_CHANGEUISTATE               = 0x0127,
		    WM_UPDATEUISTATE               = 0x0128,
		    WM_QUERYUISTATE                = 0x0129,
		    WM_CTLCOLORMSGBOX              = 0x0132,
		    WM_CTLCOLOREDIT                = 0x0133,
		    WM_CTLCOLORLISTBOX             = 0x0134,
		    WM_CTLCOLORBTN                 = 0x0135,
		    WM_CTLCOLORDLG                 = 0x0136,
		    WM_CTLCOLORSCROLLBAR           = 0x0137,
		    WM_CTLCOLORSTATIC              = 0x0138,
		    MN_GETHMENU                    = 0x01E1,
		    WM_MOUSEFIRST                  = 0x0200,
		    WM_MOUSEMOVE                   = 0x0200,
		    WM_LBUTTONDOWN                 = 0x0201,
		    WM_LBUTTONUP                   = 0x0202,
		    WM_LBUTTONDBLCLK               = 0x0203,
		    WM_RBUTTONDOWN                 = 0x0204,
		    WM_RBUTTONUP                   = 0x0205,
		    WM_RBUTTONDBLCLK               = 0x0206,
		    WM_MBUTTONDOWN                 = 0x0207,
		    WM_MBUTTONUP                   = 0x0208,
		    WM_MBUTTONDBLCLK               = 0x0209,
		    WM_MOUSEWHEEL                  = 0x020A,
		    WM_XBUTTONDOWN                 = 0x020B,
		    WM_XBUTTONUP                   = 0x020C,
		    WM_XBUTTONDBLCLK               = 0x020D,
		    WM_MOUSEHWHEEL                 = 0x020E,
		    WM_PARENTNOTIFY                = 0x0210,
		    WM_ENTERMENULOOP               = 0x0211,
		    WM_EXITMENULOOP                = 0x0212,
		    WM_NEXTMENU                    = 0x0213,
		    WM_SIZING                      = 0x0214,
		    WM_CAPTURECHANGED              = 0x0215,
		    WM_MOVING                      = 0x0216,
		    WM_POWERBROADCAST              = 0x0218,
		    WM_DEVICECHANGE                = 0x0219,
		    WM_MDICREATE                   = 0x0220,
		    WM_MDIDESTROY                  = 0x0221,
		    WM_MDIACTIVATE                 = 0x0222,
		    WM_MDIRESTORE                  = 0x0223,
		    WM_MDINEXT                     = 0x0224,
		    WM_MDIMAXIMIZE                 = 0x0225,
		    WM_MDITILE                     = 0x0226,
		    WM_MDICASCADE                  = 0x0227,
		    WM_MDIICONARRANGE              = 0x0228,
		    WM_MDIGETACTIVE                = 0x0229,
		    WM_MDISETMENU                  = 0x0230,
		    WM_ENTERSIZEMOVE               = 0x0231,
		    WM_EXITSIZEMOVE                = 0x0232,
		    WM_DROPFILES                   = 0x0233,
		    WM_MDIREFRESHMENU              = 0x0234,
		    WM_IME_SETCONTEXT              = 0x0281,
		    WM_IME_NOTIFY                  = 0x0282,
		    WM_IME_CONTROL                 = 0x0283,
		    WM_IME_COMPOSITIONFULL         = 0x0284,
		    WM_IME_SELECT                  = 0x0285,
		    WM_IME_CHAR                    = 0x0286,
		    WM_IME_REQUEST                 = 0x0288,
		    WM_IME_KEYDOWN                 = 0x0290,
		    WM_IME_KEYUP                   = 0x0291,
		    WM_MOUSEHOVER                  = 0x02A1,
		    WM_MOUSELEAVE                  = 0x02A3,
		    WM_NCMOUSEHOVER                = 0x02A0,
		    WM_NCMOUSELEAVE                = 0x02A2,
		    WM_WTSSESSION_CHANGE           = 0x02B1,
		    WM_TABLET_FIRST                = 0x02c0,
		    WM_TABLET_LAST                 = 0x02df,
		    WM_CUT                         = 0x0300,
		    WM_COPY                        = 0x0301,
		    WM_PASTE                       = 0x0302,
		    WM_CLEAR                       = 0x0303,
		    WM_UNDO                        = 0x0304,
		    WM_RENDERFORMAT                = 0x0305,
		    WM_RENDERALLFORMATS            = 0x0306,
		    WM_DESTROYCLIPBOARD            = 0x0307,
		    WM_DRAWCLIPBOARD               = 0x0308,
		    WM_PAINTCLIPBOARD              = 0x0309,
		    WM_VSCROLLCLIPBOARD            = 0x030A,
		    WM_SIZECLIPBOARD               = 0x030B,
		    WM_ASKCBFORMATNAME             = 0x030C,
		    WM_CHANGECBCHAIN               = 0x030D,
		    WM_HSCROLLCLIPBOARD            = 0x030E,
		    WM_QUERYNEWPALETTE             = 0x030F,
		    WM_PALETTEISCHANGING           = 0x0310,
		    WM_PALETTECHANGED              = 0x0311,
		    WM_HOTKEY                      = 0x0312,
		    WM_PRINT                       = 0x0317,
		    WM_PRINTCLIENT                 = 0x0318,
		    WM_APPCOMMAND                  = 0x0319,
		    WM_THEMECHANGED                = 0x031A,
		    WM_CLIPBOARDUPDATE             = 0x031D,
		    WM_DWMCOMPOSITIONCHANGED       = 0x031E,
		    WM_DWMNCRENDERINGCHANGED       = 0x031F,
		    WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320,
		    WM_DWMWINDOWMAXIMIZEDCHANGE    = 0x0321,
		    WM_GETTITLEBARINFOEX           = 0x033F,
		    WM_HANDHELDFIRST               = 0x0358,
		    WM_HANDHELDLAST                = 0x035F,
		    WM_AFXFIRST                    = 0x0360,
		    WM_AFXLAST                     = 0x037F,
		    WM_PENWINFIRST                 = 0x0380,
		    WM_PENWINLAST                  = 0x038F,
		    WM_APP                         = 0x8000,
		    WM_USER                        = 0x0400,
		    WM_REFLECT                     = WM_USER + 0x1C00,
	    }

        public enum AppBarNotification
        {
            /// <summary>
            /// Notifies an appbar that the taskbar's autohide or 
            /// always-on-top state has changed—that is, the user has selected 
            /// or cleared the "Always on top" or "Auto hide" check box on the
            /// taskbar's property sheet. 
            /// </summary>
            STATECHANGE = 0x00000000,
            /// <summary>
            /// Notifies an appbar when an event has occurred that may affect 
            /// the appbar's size and position. Events include changes in the
            /// taskbar's size, position, and visibility state, as well as the
            /// addition, removal, or resizing of another appbar on the same 
            /// side of the screen.
            /// </summary>
            POSCHANGED = 0x00000001,
            /// <summary>
            /// Notifies an appbar when a full-screen application is opening or
            /// closing. This notification is sent in the form of an 
            /// application-defined message that is set by the ABM_NEW message. 
            /// </summary>
            FULLSCREENAPP = 0x00000002,
            /// <summary>
            /// Notifies an appbar that the user has selected the Cascade, 
            /// Tile Horizontally, or Tile Vertically command from the 
            /// taskbar's shortcut menu.
            /// </summary>
            WINDOWARRANGE = 0x00000003
        }

        public enum AppBarMessage
        {
            /// <summary>
            /// Registers a new appbar and specifies the message identifier
            /// that the system should use to send notification messages to 
            /// the appbar. 
            /// </summary>
            NEW = 0x00000000,
            /// <summary>
            /// Unregisters an appbar, removing the bar from the system's 
            /// internal list.
            /// </summary>
            REMOVE = 0x00000001,
            /// <summary>
            /// Requests a size and screen position for an appbar.
            /// </summary>
            QUERYPOS = 0x00000002,
            /// <summary>
            /// Sets the size and screen position of an appbar. 
            /// </summary>
            SETPOS = 0x00000003,
            /// <summary>
            /// Retrieves the autohide and always-on-top states of the 
            /// Microsoft® Windows® taskbar. 
            /// </summary>
            GETSTATE = 0x00000004,
            /// <summary>
            /// Retrieves the bounding rectangle of the Windows taskbar. 
            /// </summary>
            GETTASKBARPOS = 0x00000005,
            /// <summary>
            /// Notifies the system that an appbar has been activated. 
            /// </summary>
            ACTIVATE = 0x00000006,
            /// <summary>
            /// Retrieves the handle to the autohide appbar associated with
            /// a particular edge of the screen. 
            /// </summary>
            GETAUTOHIDEBAR = 0x00000007,
            /// <summary>
            /// Registers or unregisters an autohide appbar for an edge of 
            /// the screen. 
            /// </summary>
            SETAUTOHIDEBAR = 0x00000008,
            /// <summary>
            /// Notifies the system when an appbar's position has changed. 
            /// </summary>
            WINDOWPOSCHANGED = 0x00000009,
            /// <summary>
            /// Sets the state of the appbar's autohide and always-on-top 
            /// attributes.
            /// </summary>
            SETSTATE = 0x0000000a,
            /// <summary>
            /// Windows XP and later: Retrieves the handle to the autohide
            /// appbar associated with a particular edge of a particular monitor.
            /// </summary>
            GETAUTOHIDEBAREX = 0x0000000B,
            /// <summary>
            /// Windows XP and later: Registers or unregisters an autohide appbar
            /// for an edge of a particular monitor.
            /// attributes.
            /// </summary>
            SETAUTOHIDEBAREX = 0x0000000C
        }

        public enum AppBarDockPosition
        {
            Left = 0,
            Top,
            Right,
            Bottom,
            Float
        }

        [Flags]
        public enum MonitorInfoOf
        {
            PRIMARY = 0x1
        }

        [Flags]
        public enum AppBarStates
        {
            AutoHide = 0x00000001,
            AlwaysOnTop = 0x00000002
        }

        [Flags]
        internal enum DWMNCRenderingPolicy
        {
            UseWindowStyle,
            Disabled,
            Enabled,
            Last
        }

        [System.Flags]
        internal enum DWMWINDOWATTRIBUTE
        {
            DWMA_NCRENDERING_ENABLED = 1,
            DWMA_NCRENDERING_POLICY,
            DWMA_TRANSITIONS_FORCEDISABLED,
            DWMA_ALLOW_NCPAINT,
            DWMA_CPATION_BUTTON_BOUNDS,
            DWMA_NONCLIENT_RTL_LAYOUT,
            DWMA_FORCE_ICONIC_REPRESENTATION,
            DWMA_FLIP3D_POLICY,
            DWMA_EXTENDED_FRAME_BOUNDS,
            DWMA_HAS_ICONIC_BITMAP,
            DWMA_DISALLOW_PEEK,
            DWMA_EXCLUDED_FROM_PEEK,
            DWMA_LAST
        }

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public int Width
            {
                get { return right - left; }
            }

            public int Height
            {
                get { return bottom - top; }
            }

            public void Offset(int dx, int dy)
            {
                left += dx;
                top += dy;
                right += dx;
                bottom += dy;
            }

            public bool IsEmpty
            {
                get
                {
                    return left >= right || top >= bottom;
                }
            }

            public static explicit operator Int32Rect(RECT r)
            {
                return new Int32Rect(r.left, r.top, r.Width, r.Height);
            }

            public static explicit operator Rect(RECT r)
            {
                return new Rect(r.left, r.top, r.Width, r.Height);
            }

            public static explicit operator RECT(Rect r)
            {
                return new RECT((int)r.Left, (int)r.Top, (int)r.Right, (int)r.Bottom);
            }
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MONITORINFOEX
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public MonitorInfoOf dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string szDevice;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;

            public Rect Bounds
            {
                get { return new Rect(x, y, cx, cy); }
                set
                {
                    x = (int)value.X;
                    y = (int)value.Y;
                    cx = (int)value.Width;
                    cy = (int)value.Height;
                }
            }
        }

        #endregion
                
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

        #endregion
    }
}
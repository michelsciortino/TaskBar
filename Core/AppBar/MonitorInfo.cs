using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using static TaskBar.Core.WinApi.NativeMethods;
using static TaskBar.Core.WinApi.ShellApi;

namespace TaskBar.Core.WinApi
{
    public sealed class MonitorInfo : IEquatable<MonitorInfo>
    {
        #region Public Properties

        public Rect ViewportBounds { get; }

        public Rect WorkAreaBounds { get; }

        public bool IsPrimary { get; }

        public string DeviceId { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a list of all active monitors
        /// </summary>
        public static IEnumerable<MonitorInfo> GetAllMonitors()
        {
            var monitors = new List<MonitorInfo>();
            MonitorEnumDelegate callback = delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
            {
                MONITORINFOEX mi = new MONITORINFOEX
                {
                    cbSize = Marshal.SizeOf(typeof(MONITORINFOEX))
                };
                if (!GetMonitorInfo(hMonitor, ref mi))
                {
                    throw new Win32Exception();
                }

                monitors.Add(new MonitorInfo(mi));
                return true;
            };

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);

            return monitors;
        }

        public bool Equals(MonitorInfo other) => DeviceId == other?.DeviceId;

        public static bool operator ==(MonitorInfo a, MonitorInfo b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(MonitorInfo a, MonitorInfo b) => !(a == b);

            #region Overrides
        
            public override string ToString() => DeviceId;

            public override bool Equals(object obj) => Equals(obj as MonitorInfo);

            public override int GetHashCode() => DeviceId.GetHashCode();

            #endregion

        #endregion

        #region Internal Methods
        internal MonitorInfo(MONITORINFOEX mex)
        {
            ViewportBounds = (Rect)mex.rcMonitor;
            WorkAreaBounds = (Rect)mex.rcWork;
            IsPrimary = mex.dwFlags.HasFlag(ShellApi.MonitorInfoOf.PRIMARY);
            DeviceId = mex.szDevice;
        }

        #endregion
    }
}

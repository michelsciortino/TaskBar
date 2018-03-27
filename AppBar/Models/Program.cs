using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System;

namespace AppBar.Models
{
    [Serializable]
    public class Program
    {
        public bool IsRunning { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public BitmapImage Icon { get; set; }
        public List<Process> ActiveProcesses { get; set; }
    }
}

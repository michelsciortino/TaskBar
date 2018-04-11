using TaskBar.Core;
using TaskBar.Core.Models;
using TaskBar.ViewModels;

namespace TaskBar
{
    /// <summary>
    /// Window of the TaskBar
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            //Setting a new MainWindow View Model as the datacontext of the MainWindow
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }
    }
}

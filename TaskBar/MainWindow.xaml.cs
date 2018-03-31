using TaskBar.Core;
using TaskBar.Core.Models;
using TaskBar.ViewModels;

namespace TaskBar
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }
    }
}

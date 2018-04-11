using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TaskBar.Controls
{
    /// <summary>
    /// Logica di interazione per BarItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public ItemControl()
        {
            InitializeComponent();
        }

        /*public SolidColorBrush ActiveColor
        {
            get { return (SolidColorBrush)GetValue(ActiveColorProperty); }
            set { SetValue(ActiveColorProperty, value); }
        }*/

        // Using a DependencyProperty as the backing store for ActiveColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveColorProperty =
            DependencyProperty.Register("ActiveColor", typeof(SolidColorBrush), typeof(ItemControl), new PropertyMetadata((SolidColorBrush)Brushes.Green));
    }
}

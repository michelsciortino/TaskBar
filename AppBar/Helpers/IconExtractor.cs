using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AppBar.Helpers
{
    public class IconExtractor
    {
        #region Singletons

        public static BitmapImage UnknownImageSource_16x16 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_16x16"));
        public static BitmapImage UnknownImageSource_24x24 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_24x24"));
        public static BitmapImage UnknownImageSource_32x32 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_32x32"));
        public static BitmapImage UnknownImageSource_44x44= new BitmapImage(new Uri("/AppBar;component/Images/Unknown_44x44"));

        #endregion

        public static BitmapImage ExtractIconSource()
        {
            BitmapImage image=null;

            return image;
        }
    }
}

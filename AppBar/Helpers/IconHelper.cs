using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using IconExtractor;
namespace AppBar.Helpers
{
    public class IconHelper
    {
        #region Singletons

        public static BitmapImage UnknownImageSource_16x16 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_16x16"));
        public static BitmapImage UnknownImageSource_24x24 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_24x24"));
        public static BitmapImage UnknownImageSource_32x32 = new BitmapImage(new Uri("/AppBar;component/Images/Unknown_32x32"));
        public static BitmapImage UnknownImageSource_44x44= new BitmapImage(new Uri("/AppBar;component/Images/Unknown_44x44"));

        #endregion

        public static List<BitmapImage> ExtractIconSource(string fullPath)
        {
            List<BitmapImage> images=null;
            Icon[] splitIcons = null;

            try
            {
                Extractor e = new Extractor(fullPath);
                splitIcons = e.GetAllIcons();

                foreach (Icon i in splitIcons)
                    images.Add(BitmapToBitmapImage(i));
            }
            catch
            {
                images.Add(UnknownImageSource_16x16);
                images.Add(UnknownImageSource_24x24);
                images.Add(UnknownImageSource_32x32);
                images.Add(UnknownImageSource_44x44);
                return images;
            }

            return images;
        }

        public static BitmapImage BitmapToBitmapImage(Icon icon)
        {
            BitmapImage image = new BitmapImage();
            Bitmap bitmap = IconUtil.ToBitmap(icon);
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;                
                image.BeginInit();
                image.StreamSource = memory;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image;
        }
    }
}

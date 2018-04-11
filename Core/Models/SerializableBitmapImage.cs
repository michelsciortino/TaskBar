using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace TaskBar.Core.Models
{
    [Serializable]
    public class SerializableBitmapImage
    {
        byte[] image;
        public SerializableBitmapImage(BitmapImage img)
        {
            this.image = BitmapImageToBytes(img);
        }

        public BitmapImage Deserialize()
        {
            return BytesToBitmapImage(this.image);
        }
        
        public static byte[] BitmapImageToBytes(BitmapImage bitmapImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage.UriSource));
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        public static BitmapImage BytesToBitmapImage(byte[] imageBytes)
        {
            BitmapImage image;
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
            }
            return image;
        }
    }
}

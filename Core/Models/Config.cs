using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace AppBar.Core.Models
{
    /// <summary>
    /// AppBar Configuration
    /// </summary>
    public class Config
    {
        #region static variables
        
        public static BitmapImage UnknownImageSource_24x24 = new BitmapImage(Helpers.UriHelper.GetUri(null, "Images/Unknown_24x24.png"));
        public static BitmapImage UnknownImageSource_32x32 = new BitmapImage(Helpers.UriHelper.GetUri(null, "Images/Unknown_32x32.png"));
        public static BitmapImage UnknownImageSource_44x44 = new BitmapImage(Helpers.UriHelper.GetUri(null, "Images/Unknown_44x44.png"));
        public static BitmapImage UnknownImageSource_16x16 = new BitmapImage(Helpers.UriHelper.GetUri(null, "Images/Unknown_16x16.png"));

        /// <summary>
        /// Working directory of the program
        /// </summary>
        static string WorkingDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Default configuration filename
        /// </summary>
        private const string ConfigFilename = ".configuration";

        #endregion
        #region Public Properties

            #region Theme
                #region Colors
                public string BackgroundColor;
                public string BorderColor;
                public string AccentColor;
                public string TextColor;
                public bool Transparency;
                #endregion
                #region Sizes
                public double IconSize;
                public double IconsSpacing;
                public double HorizontalPadding;
                public double VerticalPadding;
        #endregion
        #endregion
        #region Behaviors
        public bool OnTop;
        public bool 
        #endregion
        #region PinnedItems
        /// <summary>
        /// List of programs saved in the configuration
        /// </summary>
        public List<Program> Programs;
        #endregion
        #region CustomIcons
        /// <summary>
        /// List of custom icons
        /// </summary>
        public List<BitmapImage> CustomIcons;
        #endregion







        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="pList"> List of programs </param>
        /// <param name="customIconList"> List of custom icons </param>
        public Config(List<Program> pList = null,List<BitmapImage> customIconList = null)
        {
            if (pList != null)
                Programs = new List<Program>(pList);
            else
                Programs = new List<Program>();
            if (customIconList != null)
                CustomIcons = new List<BitmapImage>(customIconList);
            else
                CustomIcons = new List<BitmapImage>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a Configuration from file
        /// </summary>
        /// <param name="filename">Path + filename of configuration file</param>
        /// <returns>A Config if the Deserialization succeeded : null otherwise</returns>
        public static Config ReadConfiguration()
        {
            Config newConfig = null;
            
            try
            {
                SerializableConfig serializable= Helpers.Serializer.DeserializeObj<SerializableConfig>($"{WorkingDirectoryPath}{ConfigFilename}");
                newConfig = serializable.Deserialize();
            }
            catch
            {
                return null;
            }

            return newConfig;
        }

        /// <summary>
        /// SAve a Configuration to file
        /// </summary>
        /// <param name="config">Configuration to be saved</param>
        /// <returns>True if Serialization succeeded : False otherwise</returns>
        public static bool SaveConfiguration(Config config)
        {
            try
            {
                SerializableConfig serializable = new SerializableConfig(config);
                Helpers.Serializer.SerializeObj<SerializableConfig>(serializable, $"{WorkingDirectoryPath}{ConfigFilename}");
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Loads an Empty Configuration
        /// </summary>
        public static Config GetDefaultConfiguration()
        {
            List<BitmapImage> list = new List<BitmapImage>();
            list.Add(UnknownImageSource_16x16);
            list.Add(UnknownImageSource_16x16);
            list.Add(UnknownImageSource_16x16);
            list.Add(UnknownImageSource_16x16);
            return new Config(null, list);
        }

        #endregion

        
    }

    [Serializable]
    public class SerializableConfig
    {
        List<byte[]> CustomIcons;
        List<Program> Programs;

        public SerializableConfig(Config config)
        {
            Programs = new List<Program>(config.Programs);
            CustomIcons = new List<byte[]>();
            foreach (BitmapImage img in config.CustomIcons)
            {
                CustomIcons.Add(BitmapImageToBytes(img));
            }
        }

        public Config Deserialize()
        {
            List<Program> programs = new List<Program>(this.Programs);
            List<BitmapImage> customIcons = new List<BitmapImage>();

            foreach (byte[] bytes in this.CustomIcons)
                customIcons.Add(BytesToBitmapImage(bytes));

            return new Config(programs, customIcons);
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

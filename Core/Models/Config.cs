using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using TaskBar.Core.Helpers;

namespace TaskBar.Core.Models
{
    /// <summary>
    /// AppBar Configuration
    /// </summary>
    public class Config
    {
        #region static variables

        public static BitmapImage UnknownImageSource_24x24 = new BitmapImage(UriHelper.GetUri(null, "Images/Unknown_24x24.png"));
        public static BitmapImage UnknownImageSource_32x32 = new BitmapImage(UriHelper.GetUri(null, "Images/Unknown_32x32.png"));
        public static BitmapImage UnknownImageSource_44x44 = new BitmapImage(UriHelper.GetUri(null, "Images/Unknown_44x44.png"));
        public static BitmapImage UnknownImageSource_16x16 = new BitmapImage(UriHelper.GetUri(null, "Images/Unknown_16x16.png"));

        /// <summary>
        /// Working directory of the program
        /// </summary>
        static string WorkingDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Default configuration filename
        /// </summary>
        private const string ConfigFilename = ".configuration";
        public const string DefaultBackgroundColor = "#80000000";
        public const string DefaultBorderColor = "#00000000";
        public const string DefaultAccentColor = "#ffffffff";// WinApi.NativeMethods.GetWindowColorizationColor(true);
        public const string DefaultTextColor = "#ffffff";
        #endregion

        #region Public Properties

        #region Theme
        #region Colors
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; } 
        public string AccentColor { get; set; }
        public string TextColor { get; set; }
        public int Transparency { get; set; } = 128;
        public bool BackgroundAsSysColor { get; set; } = true;
        public bool AccentAsSysColor { get; set; } = true;
        #endregion
        #region Sizes
        public double IconSize { get; set; }
        public double IconsSpacing { get; set; }
        public double HorizontalPadding { get; set; }
        public double VerticalPadding { get; set; }
        #endregion
        #endregion

        #region Behaviors

        public bool OnTop { get; set; } = false;
        public bool Docked { get; set; } = false;
        public int Position { get; set; } = (int)WinApi.ShellApi.AppBarDockPosition.Float;
        public bool AutoHide { get; set; } = false;
        public bool Locked { get; set; } = false;
        public bool EdgeMagnet { get; set; } = false;
        public bool DockUnpinnedPrograms { get; set; } = false;

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
        public Config(List<Program> pList = null,
            List<BitmapImage> customIconList = null,
            string backgroundColor = DefaultBackgroundColor,
            string borderColor= DefaultBorderColor,
            string accentColor=DefaultBorderColor,
            string textColor=DefaultTextColor,
            int transparency =128,
            bool backgroundAsSysColor=false,
            bool accentAsSysColor=true)
        {
            if (pList != null)
                Programs = new List<Program>(pList);
            else
                Programs = new List<Program>();
            if (customIconList != null)
                CustomIcons = new List<BitmapImage>(customIconList);
            else
                CustomIcons = new List<BitmapImage>();
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            if(accentAsSysColor)
                AccentColor= WinApi.NativeMethods.GetWindowColorizationColor(true);
            else
                AccentColor = accentColor;
            TextColor = textColor;
            BackgroundAsSysColor = backgroundAsSysColor;
            AccentAsSysColor = accentAsSysColor;
            if (transparency > 255)
                transparency = 255;
            if (transparency < 0)
                transparency = 0;
            Transparency = transparency;
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
                SerializableConfig serializable = Serializer.DeserializeObj<SerializableConfig>($"{WorkingDirectoryPath}{ConfigFilename}");
                newConfig = serializable.Deserialize();
            }
            catch
            {
                // No config file found
                newConfig = new Config();
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
                Serializer.SerializeObj(serializable, $"{WorkingDirectoryPath}{ConfigFilename}");
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Helpers



        #endregion
    }

    [Serializable]
    public class SerializableConfig
    {        
        string BackgroundColor,BorderColor,AccentColor,TextColor;
        public int Transparency;
        public bool BackgroundAsSysColor, AccentAsSysColor;
        public double IconSize, IconsSpacing, HorizontalPadding, VerticalPadding;
        public bool OnTop, Docked, AutoHide, Locked, EdgeMagnet, DockUnpinnedPrograms;
        public int Position;
        List<SerializableProgram> Programs;
        List<SerializableBitmapImage> CustomIcons;

        public SerializableConfig(Config config)
        {
            Programs = new List<SerializableProgram>();
            foreach (Program p in config.Programs)
            {
                Programs.Add(new SerializableProgram(p));
            }

            CustomIcons = new List<SerializableBitmapImage>();
            foreach (BitmapImage img in config.CustomIcons)
            {
                CustomIcons.Add(new SerializableBitmapImage(img));
            }
            BackgroundColor = config.BackgroundColor;
            BorderColor = config.BorderColor;
            AccentColor = config.AccentColor;
            TextColor = config.TextColor;
            Transparency = config.Transparency;
            BackgroundAsSysColor = config.BackgroundAsSysColor;
            AccentAsSysColor = config.AccentAsSysColor;
            IconSize = config.IconSize;
            IconsSpacing = config.IconsSpacing;
            HorizontalPadding = config.HorizontalPadding;
            VerticalPadding = config.VerticalPadding;
            OnTop = config.OnTop;
            Docked = config.Docked;
            AutoHide = config.AutoHide;
            Locked = config.Locked;
            EdgeMagnet = config.EdgeMagnet;
            DockUnpinnedPrograms = config.DockUnpinnedPrograms;
            Position = config.Position;
        }

        public Config Deserialize()
        {
            List<Program> programs = new List<Program>();
            List<BitmapImage> customIcons = new List<BitmapImage>();

            foreach (SerializableProgram sp in Programs)
            {
                programs.Add(sp.Deserialize());
            }
            foreach (SerializableBitmapImage sbi in this.CustomIcons)
                customIcons.Add(sbi.Deserialize());
            return new Config(programs, customIcons,BackgroundColor,BorderColor,AccentColor,TextColor,Transparency,BackgroundAsSysColor,AccentAsSysColor);
        }
    }
}
using System.Windows.Media.Imaging;
using System;

namespace TaskBar.Core.Models
{
    public class Program
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public BitmapImage Icon { get; set; }
    }

    [Serializable]
    public class SerializableProgram
    {
        public string Name, Path;
        public SerializableBitmapImage Icon;     

        public SerializableProgram(Program p)
        {
            this.Name = p.Name;
            this.Path = p.Path;
            this.Icon = new SerializableBitmapImage(p.Icon);
        }
        public Program Deserialize()
        {
            return new Program
            {
                Name = this.Name,
                Path = this.Path,
                Icon = this.Icon.Deserialize()
            };
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppBar.Core.Helpers
{
    public static class Serializer
    {
        public static void SerializeObj<T>(T serializableObj,string filename,bool append = false)
        {
            if (serializableObj == null)
                return;
            using (Stream stream= File.Open(filename,append? FileMode.Append: FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, serializableObj);
            }
        }

        public static T DeserializeObj<T>(string filepath)
        {
            using (Stream stream = File.Open(filepath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppBar.Helpers
{
    class Serializer
    {
        public void SerializeObj<T>(T serializableObj,string filename,bool append = false)
        {
            if (serializableObj == null)
                return;
            using (Stream stream= File.Open(filename,append? FileMode.Append: FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
            }
        }
    }
}

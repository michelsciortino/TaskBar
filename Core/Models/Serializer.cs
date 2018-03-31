using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskBar.Core.Helpers
{
    public static class Serializer
    {
        public static void SerializeObj<T>(T serializableObj,string filename,bool append = false)
        {
            if (serializableObj == null)
                return;
            // if a .conf file already exists, make a backup and delete it
            if (File.Exists(filename))
            {
                // if a backup exist, delete it
                if (File.Exists(filename + ".old"))
                {
                    File.Delete(filename + ".old");
                }
                // opening existing file
                Stream old = File.Open(filename, FileMode.Open);
                // creating a backupfile
                FileStream backup =File.OpenWrite(filename + ".old");
                // coping the stream to the backup file
                old.CopyTo(backup);

                // releasing files
                old.Close(); 
                backup.Close();
            }
            // deleting old file
            File.Delete(filename);
            // writing new file
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

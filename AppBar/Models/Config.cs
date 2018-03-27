using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBar.Models
{
    [Serializable]
    public class Config
    {
        List<Program> Programs;
        List<string> UserIconsPaths;

        public Config(List<Program> pList,List<String> uipList)
        {
            Programs = new List<Program>(pList);
            UserIconsPaths = new List<string>(uipList);
        }


        public static Config ReadConfiguration()
        {
            Config newConfig = null;

            return newConfig;
        }

        public static bool SaveConfiguration()
        {
            return true;
        }
    }
}

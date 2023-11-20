using EnumsNamespace;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace PENTAGON
{
    public class Program
    {
        public static Player player1;
        public static Store store;
        public static Dungeon dungeon;
  
        static void Main()
        {
            GameManager.Instance.GameDataSetting();
            GameManager.Instance.GameStart();
        }
    }
}
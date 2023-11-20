using EnumsNamespace;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace PENTAGON
{
    public class Program
    {
        public static Player player1;

        static void Main()
        {
            GameManager.Instance.GameDataSetting();
            GameManager.Instance.GameStart();
        }

    }
}
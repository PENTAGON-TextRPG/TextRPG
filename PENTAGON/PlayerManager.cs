using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PENTAGON
{
    public class PlayerManager
    {
        public static PlayerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerManager();
                }
                return _instance;
            }
        }
        public void ChoiceJob(string nickname)
        {
            Console.WriteLine("직업을 선택해주세요");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");
            Console.WriteLine("4. 궁수");
            Console.WriteLine();
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(1, 4);

            if (input % 4 == 1)
            {
                Program.player1 = new Warrior(nickname);
                Program.player1.Inventory.ItemSetting();
            }
            //else if (input % 4 == 2)
            //{
            //    Program.player1 = new Mage(nickname);
            //}
            //else if (input == 3)
            //{
            //    Program.player1 = new Thief(nickname);
            //}
            //else
            //{
            //    Program.player1 = new Archer(nickname);
            //}
        }


        public string SetNickname()
        {
            string nickname;
            do
            {
                Console.WriteLine("닉네임을 입력하세요:");
                nickname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nickname))
                {
                    Console.Clear();
                    Console.WriteLine("닉네임이 올바르지 않습니다. 다시 시도하세요.");
                }

            } while (string.IsNullOrWhiteSpace(nickname));

            return nickname;
        }

        private static PlayerManager _instance;
    }
}

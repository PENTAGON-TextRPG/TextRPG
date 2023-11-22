using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class GameManager
    {
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;   
            }
        }
        public void GameDataSetting()
        {
            DataManager.Instance.InitializeMonsterDict();
            store = new Store();
            store.StoreSetting();
            dungeon = new Dungeon();
        }
        public void GameStart()
        {
            GameMain();
            string nicname = PlayerManager.Instance.SetNickname();
            PlayerManager.Instance.ChoiceJob(nicname);
            DisplayGameIntro();
        }
        public void GameMain()
        {
            Console.Clear();
            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(20, 5 + i);
                if (i % 7 == 0)
                    Console.WriteLine("_/// _//////_////////_//      _//_/// _////// _///////    _///////     _////   ");
                else if (i % 7 == 1)
                    Console.WriteLine("     _//    _//       _//   _//       _//     _//    _//  _//    _// _/    _// ");
                else if (i % 7 == 2)
                    Console.WriteLine("     _//    _//        _// _//        _//     _//    _//  _//    _//_//        ");
                else if (i % 7 == 3)
                    Console.WriteLine("     _//    _//////      _//          _//     _/ _//      _///////  _//        ");
                else if (i % 7 == 4)
                    Console.WriteLine("     _//    _//        _// _//        _//     _//  _//    _//       _//   _////");
                else if (i % 7 == 5)
                    Console.WriteLine("     _//    _//       _//   _//       _//     _//    _//  _//        _//    _/ ");
                else
                    Console.WriteLine("     _//    _////////_//      _//     _//     _//      _//_//         _/////   ");
                Thread.Sleep(200);
            }
            Console.SetCursorPosition(50, 20);
            Console.WriteLine("게임 시작");
            Console.SetCursorPosition(40, 21);
            Console.WriteLine("시작하려면 아무 키나 누르세요 . . .");
            Console.ReadKey();
        }
        public void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    Program.player1.DisplayMyInfo();
                    break;
                case 2:
                    Program.player1.Inventory.DispayInventoryMain();
                    break;
                case 3:
                    store.StoreMain();
                    break;
                case 4:
                    dungeon.DisplayDungeonIntro(Program.player1);
                    break;
            }
        }

        public int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                Console.Write(">>");
            }
        }

        public static GameManager _instance;
        public Store store;
        public Dungeon dungeon;
    }
}

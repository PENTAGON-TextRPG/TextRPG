using Newtonsoft.Json;
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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = path + @"\PENTAGON\data\player.json"; //player 정보 저장 위치
            GameMain();
            if(File.Exists(filePath))//플레이어 저장 위치에 파일이 있을 때
            {
                DataManager.Instance.LoadPlayerData();
            }
            else
            {
                string nicname = PlayerManager.Instance.SetNickname();
                PlayerManager.Instance.ChoiceJob(nicname);
            }
            DisplayGameIntro();
        }
        public void GameMain()
        {
            Console.Clear();
            for (int i = 0; i < 7; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(15, 5 + i);
                if (i % 7 == 0)
                    Console.WriteLine(":::::::::  :::::::::: ::::    ::: :::::::::::     :::      ::::::::   ::::::::  ::::    ::: ");
                else if (i % 7 == 1)
                    Console.WriteLine(":+:    :+: :+:        :+:+:   :+:     :+:       :+: :+:   :+:    :+: :+:    :+: :+:+:   :+:");
                else if (i % 7 == 2)
                    Console.WriteLine("+:+    +:+ +:+        :+:+:+  +:+     +:+      +:+   +:+  +:+        +:+    +:+ :+:+:+  +:+ ");
                else if (i % 7 == 3)
                    Console.WriteLine("+#++:++#+  +#++:++#   +#+ +:+ +#+     +#+     +#++:++#++: :#:        +#+    +:+ +#+ +:+ +#+ ");
                else if (i % 7 == 4)
                    Console.WriteLine("+#+        +#+        +#+  +#+#+#     +#+     +#+     +#+ +#+   +#+# +#+    +#+ +#+  +#+#+# ");
                else if (i % 7 == 5)
                    Console.WriteLine("#+#        #+#        #+#   #+#+#     #+#     #+#     #+# #+#    #+# #+#    #+# #+#   #+#+# ");
                else
                    Console.WriteLine("###        ########## ###    ####     ###     ###     ###  ########   ########  ###    #### ");
                Thread.Sleep(200);
            }
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                        제작 : Unity_2기 TEAM PENTAGON");
            Console.ResetColor();
            Console.WriteLine("                                      최성재 정채운 유건희 하승권 변정민");
            Console.WriteLine();
            Console.SetCursorPosition(50, 16);
            Console.WriteLine("게임 시작");
            Console.SetCursorPosition(40, 17);
            Console.WriteLine("시작하려면 아무 키나 누르세요 . . .");
            Console.ReadKey();
        }
        public void DisplayGameIntro()
        {
            Console.Clear();

            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(40, 0 + i);
                if (i % 5 == 0)
                    Console.WriteLine("|　　|　 | 　 |     |　   |　 |");
                else if (i % 5 == 1)
                    Console.WriteLine("|　　|　 | 　☆     |　   |　 |");
                else if (i % 5 == 2)
                    Console.WriteLine("|　　|　 * 　 　    *     |　 |");
                else if (i % 5 == 3)
                    Console.WriteLine("| 　★ 　　　 　 　   　 ★　 |");
                else
                    Console.WriteLine("☆ 　　 　　　 　 　  　　   ☆");
                Thread.Sleep(200);
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(30, 6);
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. >ㅅ< ♡♡ °˚");
            Console.SetCursorPosition(30, 7);
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.ResetColor();

            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(25, 9 + i);
                if (i == 0)
                    Console.WriteLine("                         ♡ ♡ ∩ ∩ ♡ ♡");
                else if (i == 1)
                    Console.WriteLine("                       + ♡ ( *′-′*)  ♡ +");
                else if (i == 2)
                    Console.WriteLine("   ┏━ ♡ ━━━━━━━━━━━━━━━━━━━━━━ U U ━━━━━━━━━━━━━━━━━━━━━━ ♡━┓");
                else if (i == 3)
                    Console.WriteLine("   ♡                 [슬기로운 던전생활 TIP]                 ♡");
                else if (i == 4)
                    Console.WriteLine("   ♡                                                         ♡");
                else if (i == 5)
                    Console.WriteLine("   ♡    상점에서 레벨에 맞는 장비를 구매하고 장착해보세요!   ♡");
                else if (i == 6)
                    Console.WriteLine("   ♡  물약은 상점에서 구매할 수 있고, 던전에서도 드랍됩니다! ♡");
                else if (i == 7)
                    Console.WriteLine("   ♡       좋은 장비와 넉넉한 물약은 모험가에게 필수~        ♡");
                else
                    Console.WriteLine("   ┗━ ♡━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ ♡━┛");
            }

            Console.SetCursorPosition(20, 19);
            Console.WriteLine("1. 상태보기          2. 인벤토리         3. 상점           4. 던전 입장          5. 저장");
            //Console.WriteLine("2. 인벤토리");
            //Console.WriteLine("3. 상점");
            //Console.WriteLine("4. 던전 입장");
            Console.WriteLine();
            Console.SetCursorPosition(40, 22);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(50, 23);
            Console.Write(">>");

            int input = CheckValidInput(1, 5);
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
                case 5:
                    Console.WriteLine("플레이어 정보를 저장합니다.");
                    DataManager.Instance.SavePlayerData();
                    Console.ReadKey();
                    DisplayGameIntro();
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

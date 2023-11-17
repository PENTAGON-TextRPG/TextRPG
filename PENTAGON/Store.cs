using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace PENTAGON
{
    internal class Store
    {
        private static List<Item> StoreArmor = new List<Item>(); // 방어구
        private static List<Item> StoreWeapon = new List<Item>(); // 무기 
        private static List<Item> StorePotion = new List<Item>(); // 포션 
        private static List<Item> StoreItem = new List<Item>(); // 모든 아이템

        static int CheckValidInput(int min, int max)
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

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void StoreSetting()
        {
            Item thornmail = new Item("가시갑옷(A)", 2, Job.Null, 0, 10, 0, "방어력 +10", "날카로운 가시들의 부드러운 춤.", 500, false);
            Item thunderstorm = new Item("번개질주(A)", 4, Job.Null, 0, 15, 0, "방어력 +15", "누구보다 빛나고 싶은 자들의 우상.", 700, false);
            Item goldenplate = new Item("황금갑옷(A)", 6, Job.Null, 0, 20, 0, "방어력 +20", "번뜩이는 흉갑에 적의 눈동자가 스칩니다.", 900, false);

            Item pinkvenom = new Item("핑크 베놈(W)", 3, Job.Mage, 6, 0, 0, "공격력 +6", "진분홍 살모사의 독이 서린 스태프입니다.", 500, false);
            Item bloodyspear = new Item("핏빛 창(W)", 3, Job.Warrior, 6, 0, 0, "공격력 +6", "전장의 핏물이 이룬 살기로 가득합니다.", 500, false);
            Item lunarblade = new Item("월식(W)", 3, Job.Archer, 6, 0, 0, "공격력 +6", "황혼이 머문 자리에 깃든 만월의 축복.", 500, false);

            StoreArmor.Add(thornmail);
            StoreArmor.Add(thunderstorm);
            StoreArmor.Add(goldenplate);

            StoreWeapon.Add(pinkvenom);
            StoreWeapon.Add(bloodyspear);
            StoreWeapon.Add(lunarblade);

            for (int i = 0; i < StoreArmor.Count; i++)
            {
                StoreItem.Add(StoreArmor[i]);
            }
            for (int i = 0; i < StoreWeapon.Count; i++)
            {
                StoreItem.Add(StoreWeapon[i]);
            }

        }


        static void StoreMain()
        {
            Console.Clear();
            ShowHighlightedText("상점");
            Console.WriteLine("어서오세요~ 없는 것 빼고 다 있는 상점입니다! :D");
            Console.WriteLine("여기서 아이템을 구매 또는 판매할 수 있습니다.");
            Console.WriteLine();
            ShowHighlightedText("1. 아이템 구매");
            ShowHighlightedText("2. 아이템 판매");
            ShowHighlightedText("0. 메인 화면");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Program.DisplayGameIntro();
                    break;

                case 1:
                    StoreBuy();
                    break;

                case 2:
                    StoreSell();
                    break;
            }
        }

        static void StoreBuy()
        {
            Console.Clear();
            ShowHighlightedText("상점 - 구매");
            Console.WriteLine("[상점 주인 아만다] : 우리 물건이 제일 좋다구!");
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "효과", "설명", "Gold");
            for (int i = 0; i < StoreItem.Count; i++)
            {
                if (Inventory.xx(StoreItem[i]))  // 인벤토리에 아이템이 있는지 없는지 확인할 메서드가 필요
                {
                    table.AddRow(StoreItem[i].Name, StoreItem[i].Effect, StoreItem[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreItem[i].Name, StoreItem[i].Effect, StoreItem[i].Explanation, StoreItem[i].Price);
                }
            }

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    Program.DisplayGameIntro();
                    break;
                //case 1:
                //    StoreWeapon(); 미구현
                //    break;
                //case 2:
                //    StoreArmor();
                //    break;
                //case 3:
                //    StorePotion();
                //    break;
            }

            table.Write();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 상점");
            Console.WriteLine("0. 방어구 상점");
            Console.WriteLine("0. 물약 상점");



            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, StoreItem.Count);
                if (input == 0)
                {
                    Program.DisplayGameIntro();
                }
                else
                {
                    if (Inventory.xx(StoreItem[input - 1])) // 인벤토리에 아이템이 있는지 없는지 확인할 메서드가 필요
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= StoreItem[input - 1].Gold)  // 인벤토리에 골드 메서드 필요
                    {
                        player.Gold -= StoreItem[input - 1].Gold;
                        Inventory.Add(StoreItem[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        StoreMain();
                    }
                    else if (player.Gold < StoreItem[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }

        static void StoreSell()
        {
            Console.Clear();
            ShowHighlightedText("상점 - 판매");
            Console.WriteLine("[상점 주인 아만다] : 어디 보자.. 쓸만 한 게 있을까?");
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "효과", "설명", "Gold");


            table.Write();
            Console.WriteLine("0. 나가기");
        }
    }
}

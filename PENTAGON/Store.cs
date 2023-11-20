using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using EnumsNamespace;

namespace PENTAGON
{
    public class Store
    {
        private static List<Item> StoreWeapon = new List<Item>(); // 무기 
        private static List<Item> StoreArmor = new List<Item>(); // 방어구
        private static List<Item> StorePotion = new List<Item>(); // 포션 

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

        public void StoreSetting()
        {
            // 무기 목록 (능력치와 골드는 임시값)
            WeaponItem w3 = new WeaponItem("전사3(W)", 3, JobType.JT_Warrior, 3, "공격력 +3", "3레벨 전사 무기.", 300, false);
            WeaponItem bloodyspear = new WeaponItem("핏빛 창(W)", 3, JobType.JT_Warrior, 6, "공격력 +6", "전장의 핏물이 이룬 살기로 가득합니다.", 600, false);
            WeaponItem w9 = new WeaponItem("전사9(W)", 3, JobType.JT_Warrior, 9, "공격력 +9", "9레벨 전사 무기.", 900, false);

            WeaponItem m3 = new WeaponItem("마법사3(W)", 3, JobType.JT_Mage, 3, "공격력 +3", "3레벨 마법사 무기.", 300, false);
            WeaponItem pinkvenom = new WeaponItem("핑크 베놈(W)", 3, JobType.JT_Mage, 6, "공격력 +6", "진분홍 살모사의 독이 서린 스태프입니다.", 600, false);
            WeaponItem m9 = new WeaponItem("마법사9(W)", 3, JobType.JT_Mage, 9, "공격력 +9", "9레벨 마법사 무기.", 900, false);

            WeaponItem t3 = new WeaponItem("도적3(W)", 3, JobType.JT_Thief, 3, "공격력 +3", "3레벨 도적 무기.", 300, false);
            WeaponItem t6 = new WeaponItem("도적6(W)", 3, JobType.JT_Thief, 6, "공격력 +6", "6레벨 도적 무기.", 600, false);
            WeaponItem lunarblade = new WeaponItem("월식(W)", 3, JobType.JT_Thief, 9, "공격력 +9", "황혼이 머문 자리에 깃든 만월의 축복.", 900, false);

            WeaponItem a3 = new WeaponItem("궁수3(W)", 3, JobType.JT_Archer, 3, "공격력 +3", "3레벨 궁수 무기.", 300, false);
            WeaponItem a6 = new WeaponItem("궁수6(W)", 3, JobType.JT_Archer, 6, "공격력 +6", "6레벨 궁수 무기.", 600, false);
            WeaponItem a9 = new WeaponItem("궁수9(W)", 3, JobType.JT_Archer, 9, "공격력 +9", "9레벨 궁수 무기.", 900, false);

            // 방어구 목록 (능력치와 골드는 임시값)
            ArmorItem thornmail = new ArmorItem("가시갑옷(A)", 5, JobType.JT_Warrior, 5, 10, "방어력 +5, 체력 +10", "날카로운 가시들의 부드러운 춤.", 500, false);
            ArmorItem goldenplate = new ArmorItem("황금갑옷(A)", 10, JobType.JT_Warrior, 10, 20, "방어력 +10, 체력 +20", "번뜩이는 흉갑에 적의 눈동자가 스칩니다.", 1000, false);

            ArmorItem m5 = new ArmorItem("마법사5(A)", 5, JobType.JT_Mage, 5, 10, "방어력 +5, 체력 +10", "5레벨 마법사 방어구.", 500, false);
            ArmorItem m10 = new ArmorItem("마법사10(A)", 10, JobType.JT_Mage, 10, 20, "방어력 +10, 체력 +20", "10레벨 마법사 방어구.", 1000, false);

            ArmorItem t5 = new ArmorItem("도적5(A)", 5, JobType.JT_Thief, 5, 10, "방어력 +5, 체력 +10", "5레벨 도적 방어구.", 500, false);
            ArmorItem t10 = new ArmorItem("도적10(A)", 10, JobType.JT_Thief, 10, 20, "방어력 +10, 체력 +20", "10레벨 도적 방어구.", 1000, false);

            ArmorItem a5 = new ArmorItem("궁수5(A)", 5, JobType.JT_Archer, 5, 10, "방어력 +5, 체력 +10", "5레벨 궁수 방어구.", 500, false);
            ArmorItem thunderdash = new ArmorItem("번개질주(A)", 10, JobType.JT_Archer, 10, 20, "방어력 +10, 체력 +20", "누구보다 빛나고 싶은 자들의 우상.", 1000, false);

            // 포션 목록 (능력치와 골드는 임시값)
            PotionItem redpotion = new PotionItem("빨간 물약", 30, 0, 0, "HP +10.", "사용 시 HP를 30 회복합니다.", 30);
            PotionItem bluepotion = new PotionItem("파란 물약", 0, 30, 0, "MP +30.", "사용 시 MP를 30 회복합니다.", 30);



            // 무기 Add
            // Warrior
            StoreWeapon.Add(w3);
            StoreWeapon.Add(bloodyspear);
            StoreWeapon.Add(w9);
            // Mage
            StoreWeapon.Add(m3);
            StoreWeapon.Add(pinkvenom);
            StoreWeapon.Add(m9);
            // Thief
            StoreWeapon.Add(t3);
            StoreWeapon.Add(t6);
            StoreWeapon.Add(lunarblade);
            // Archer
            StoreWeapon.Add(a3);
            StoreWeapon.Add(a6);
            StoreWeapon.Add(a9);

            // 방어구 Add 
            // Warrior
            StoreArmor.Add(thornmail);
            StoreArmor.Add(goldenplate);
            // Mage
            StoreArmor.Add(m5);
            StoreArmor.Add(m10);
            // Thief
            StoreArmor.Add(t5);
            StoreArmor.Add(t10);
            // Archer
            StoreArmor.Add(a5);
            StoreArmor.Add(thunderdash);

            // 포션 Add
            StorePotion.Add(redpotion);
            StorePotion.Add(bluepotion);


        }
        public void StoreMain()
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
                    GameManager.Instance.DisplayGameIntro();
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
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 구매");
            Console.WriteLine("2. 방어구 구매");
            Console.WriteLine("3. 물약 구매");

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
                case 1:
                    StoreBuyWeapon();
                    break;
                case 2:
                    StoreBuyArmor();
                    break;
                case 3:
                    StoreBuyPotion();
                    break;
            }
        }

        static void StoreBuyWeapon()
        {
            Console.Clear();
            ShowHighlightedText("무기 구매");
            Console.WriteLine("[상점 주인 아만다] : 말보다 무기로 기선제압! 우리 스타일 알지? ");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();

            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("무기", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < StoreWeapon.Count; i++)
            {
                if (Program.player1.Inventory.weaponItem.Contains(StoreWeapon[i]))  // 인벤토리에 아이템이 있는지 확인 
                {
                    table.AddRow(StoreWeapon[i].Name, StoreWeapon[i].Level, StoreWeapon[i].JobType, StoreWeapon[i].Effect, StoreWeapon[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreWeapon[i].Name, StoreWeapon[i].Level, StoreWeapon[i].JobType, StoreWeapon[i].Effect, StoreWeapon[i].Explanation, StoreWeapon[i].Gold);
                }
            }

            table.Write();
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, StoreWeapon.Count);
                if (input == 0)
                {
                    StoreBuy();
                }
                else
                {
                    if (Program.player1.Inventory.weaponItem.Contains(StoreWeapon[input - 1])) // 인벤토리에 아이템이 있는지 확인
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (Program.player1.Gold >= StoreWeapon[input - 1].Gold)
                    {
                        Program.player1.Gold -= StoreWeapon[input - 1].Gold;
                        Program.player1.Inventory.weaponItem.Add((WeaponItem)StoreWeapon[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        StoreBuyWeapon();
                    }
                    else if (Program.player1.Gold < StoreWeapon[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }


        static void StoreBuyArmor()
        {
            Console.Clear();
            ShowHighlightedText("방어구 구매");
            Console.WriteLine("[상점 주인 아만다] : 잘 막고 버텨야 때릴 시간도 생기는 법이지. ");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();

            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("방어구", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < StoreArmor.Count; i++)
            {
                if (Program.player1.Inventory.armorItem.Contains(StoreArmor[i]))  // 인벤토리에 아이템이 있는지 확인 
                {
                    table.AddRow(StoreArmor[i].Name, StoreArmor[i].Level, StoreArmor[i].JobType, StoreArmor[i].Effect, StoreArmor[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreArmor[i].Name, StoreArmor[i].Level, StoreArmor[i].JobType, StoreArmor[i].Effect, StoreArmor[i].Explanation, StoreArmor[i].Gold);
                }
            }

            table.Write();
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, StoreArmor.Count);
                if (input == 0)
                {
                    StoreBuy();
                }
                else
                {
                    if (Program.player1.Inventory.armorItem.Contains(StoreArmor[input - 1])) // 인벤토리에 아이템이 있는지 확인
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (Program.player1.Gold >= StoreArmor[input - 1].Gold)
                    {
                        Program.player1.Gold -= StoreArmor[input - 1].Gold;
                        Program.player1.Inventory.armorItem.Add((ArmorItem)StoreArmor[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        StoreBuyArmor();
                    }
                    else if (Program.player1.Gold < StoreArmor[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }

        static void StoreBuyPotion()
        {
            Console.Clear();
            ShowHighlightedText("물약 구매");
            Console.WriteLine("[상점 주인 아만다] : 방금 만든 따끈따끈한 물약 어때?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();

            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("물약", "설명", "Gold");
            for (int i = 0; i < StorePotion.Count; i++)
            {
                table.AddRow(i + 1 + ". " + StorePotion[i].Name, StorePotion[i].Explanation, StorePotion[i].Gold);
            }

            table.Write();
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, StorePotion.Count);
                if (input == 0)
                {
                    StoreBuy();
                }
                else
                {
                    if (Program.player1.Gold >= StorePotion[input - 1].Gold)
                    {
                        Program.player1.Gold -= StorePotion[input - 1].Gold;
                        Program.player1.Inventory.potionItem.Add((PotionItem)StorePotion[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        StoreBuyPotion();
                    }
                    else if (Program.player1.Gold < StorePotion[input - 1].Gold)
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
            Console.WriteLine("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 판매");
            Console.WriteLine("2. 방어구 판매");
            Console.WriteLine("3. 물약 판매");

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
                case 1:
                    StoreSellWeapon();
                    break;
                case 2:
                    StoreSellArmor();
                    break;
                case 3:
                    StoreSellPotion();
                    break;
            }
        }

        static void StoreSellWeapon()
        {
            Console.Clear();
            ShowHighlightedText("무기 판매");
            Console.WriteLine("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < Program.player1.Inventory.weaponItem.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.weaponItem[i].Name, Program.player1.Inventory.weaponItem[i].Level, Program.player1.Inventory.weaponItem[i].JobType, Program.player1.Inventory.weaponItem[i].Effect, Program.player1.Inventory.weaponItem[i].Explanation, Program.player1.Inventory.weaponItem[i].Gold);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (true) //판매할 아이템 선택
            {
                Console.WriteLine();
                Console.WriteLine("판매할 아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Program.player1.Inventory.weaponItem.Count);
                if (input == 0)
                {
                    StoreSell();
                }
                else
                {
                    Program.player1.Gold += Program.player1.Inventory.weaponItem[input - 1].Gold * 70 / 100;
                    StoreWeapon.Add(Program.player1.Inventory.weaponItem[input - 1]);
                    Program.player1.Inventory.weaponItem.Remove(Program.player1.Inventory.weaponItem[input - 1]);
                    Console.WriteLine("무기를 판매했습니다.");
                    Thread.Sleep(1000);
                    StoreSellWeapon();
                }
            }
        }

        static void StoreSellArmor()
        {
            Console.Clear();
            ShowHighlightedText("무기 판매");
            Console.WriteLine("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < Program.player1.Inventory.armorItem.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.armorItem[i].Name, Program.player1.Inventory.armorItem[i].Level, Program.player1.Inventory.armorItem[i].JobType, Program.player1.Inventory.armorItem[i].Effect, Program.player1.Inventory.armorItem[i].Explanation, Program.player1.Inventory.armorItem[i].Gold);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (true) //판매할 아이템 선택
            {
                Console.WriteLine();
                Console.WriteLine("판매할 아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Program.player1.Inventory.armorItem.Count);
                if (input == 0)
                {
                    StoreSell();
                }
                else
                {
                    Program.player1.Gold += Program.player1.Inventory.armorItem[input - 1].Gold * 70 / 100;
                    StoreWeapon.Add(Program.player1.Inventory.armorItem[input - 1]);
                    Program.player1.Inventory.armorItem.Remove(Program.player1.Inventory.armorItem[input - 1]);
                    Console.WriteLine("방어구를 판매했습니다.");
                    Thread.Sleep(1000);
                    StoreSellArmor();
                }
            }
        }

        static void StoreSellPotion()
        {
            Console.Clear();
            ShowHighlightedText("무기 판매");
            Console.WriteLine("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "설명", "Gold");
            for (int i = 0; i < Program.player1.Inventory.potionItem.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.potionItem[i].Name, Program.player1.Inventory.potionItem[i].Explanation, Program.player1.Inventory.potionItem[i].Gold);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (true) //판매할 아이템 선택
            {
                Console.WriteLine();
                Console.WriteLine("판매할 아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Program.player1.Inventory.armorItem.Count);
                if (input == 0)
                {
                    StoreSell();
                }
                else
                {
                    Program.player1.Gold += Program.player1.Inventory.potionItem[input - 1].Gold * 70 / 100;
                    StoreWeapon.Add(Program.player1.Inventory.potionItem[input - 1]);
                    Program.player1.Inventory.potionItem.Remove(Program.player1.Inventory.potionItem[input - 1]);
                    Console.WriteLine("아이템을 판매했습니다.");
                    Thread.Sleep(1000);
                    StoreSellPotion();
                }
            }
        }
    }
}

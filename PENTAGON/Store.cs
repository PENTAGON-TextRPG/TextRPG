﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using EnumsNamespace;

namespace PENTAGON
{
    internal class Store
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

        static void StoreSetting()
        {
            // 무기 목록 (능력치와 골드는 임시값)
            ArmorItem w3 = new ArmorItem("전사3(W)", 3, JobType.JT_Warrior, 3, 0, 0, "공격력 +3", "3레벨 전사 무기.", 300, false);
            EquipItem bloodyspear = new EquipItem("핏빛 창(W)", 3, JobType.JT_Warrior, 6, 0, 0, "공격력 +6", "전장의 핏물이 이룬 살기로 가득합니다.", 600, false);
            EquipItem w9 = new EquipItem("전사9(W)", 3, JobType.JT_Warrior, 9, 0, 0, "공격력 +9", "9레벨 전사 무기.", 900, false);

            EquipItem m3 = new EquipItem("마법사3(W)", 3, JobType.JT_Mage, 3, 0, 0, "공격력 +3", "3레벨 마법사 무기.", 300, false);
            EquipItem pinkvenom = new EquipItem("핑크 베놈(W)", 3, JobType.JT_Mage, 6, 0, 0, "공격력 +6", "진분홍 살모사의 독이 서린 스태프입니다.", 600, false);
            EquipItem m9 = new EquipItem("마법사9(W)", 3, JobType.JT_Mage, 9, 0, 0, "공격력 +9", "9레벨 마법사 무기.", 900, false);

            EquipItem t3 = new EquipItem("도적3(W)", 3, JobType.JT_Thief, 3, 0, 0, "공격력 +3", "3레벨 도적 무기.", 300, false);
            EquipItem t6 = new EquipItem("도적6(W)", 3, JobType.JT_Thief, 6, 0, 0, "공격력 +6", "6레벨 도적 무기.", 600, false);
            EquipItem lunarblade = new EquipItem("월식(W)", 3, JobType.JT_Thief, 9, 0, 0, "공격력 +9", "황혼이 머문 자리에 깃든 만월의 축복.", 900, false);

            EquipItem a3 = new EquipItem("궁수3(W)", 3, JobType.JT_Archer, 3, 0, 0, "공격력 +3", "3레벨 궁수 무기.", 300, false);
            EquipItem a6 = new EquipItem("궁수6(W)", 3, JobType.JT_Archer, 6, 0, 0, "공격력 +6", "6레벨 궁수 무기.", 600, false);
            EquipItem a9 = new EquipItem("궁수9(W)", 3, JobType.JT_Archer, 9, 0, 0, "공격력 +9", "9레벨 궁수 무기.", 900, false);

            // 방어구 목록 (능력치와 골드는 임시값)
            EquipItem thornmail = new EquipItem("가시갑옷(A)", 5, JobType.JT_Warrior, 0, 5, 0, "방어력 +5", "날카로운 가시들의 부드러운 춤.", 500, false);
            EquipItem goldenplate = new EquipItem("황금갑옷(A)", 10, JobType.JT_Warrior, 0, 10, 0, "방어력 +10", "번뜩이는 흉갑에 적의 눈동자가 스칩니다.", 1000, false);

            EquipItem m5 = new EquipItem("마법사5(A)", 5, JobType.JT_Mage, 0, 5, 0, "방어력 +5", "5레벨 마법사 방어구.", 500, false);
            EquipItem m10 = new EquipItem("마법사10(A)", 10, JobType.JT_Mage, 0, 10, 0, "방어력 +10", "10레벨 마법사 방어구.", 1000, false);

            EquipItem t5 = new EquipItem("도적5(A)", 5, JobType.JT_Thief, 0, 5, 0, "방어력 +5", "5레벨 도적 방어구.", 500, false);
            EquipItem t10 = new EquipItem("도적10(A)", 10, JobType.JT_Thief, 0, 10, 0, "방어력 +10", "10레벨 도적 방어구.", 1000, false);

            EquipItem a5 = new EquipItem("궁수5(A)", 5, JobType.JT_Archer, 0, 5, 0, "방어력 +5", "5레벨 궁수 방어구.", 500, false);
            EquipItem thunderdash = new EquipItem("번개질주(A)", 10, JobType.JT_Archer, 0, 10, 0, "방어력 +10", "누구보다 빛나고 싶은 자들의 우상.", 1000, false);

            // 포션 목록 (능력치와 골드는 임시값)
            PotionItem redpotion = new PotionItem("빨간 물약", 1, 0, 0, 10, "HP +10.", "사용 시 HP를 10 회복합니다.", 30, false);
            PotionItem bluepotion = new PotionItem("파란 물약", 5, 0, 0, 10, "HP +30.", "사용 시 MP를 10 회복합니다.", 30, false);



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
            // Null
            StoreArmor.Add(null3);
            StoreArmor.Add(null7);
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
            StorePotion.Add(whitepotion);


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
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
                case 1:
                    DisplayStoreWeapon();
                    break;
                case 2:
                    DisplayStoreArmor();
                    break;
                case 3:
                    DisplayStorePotion();
                    break;
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 상점");
            Console.WriteLine("2. 방어구 상점");
            Console.WriteLine("3. 물약 상점");
        }

        static void DisplayStoreWeapon()
        {
            Console.Clear();
            ShowHighlightedText("무기 상점");
            Console.WriteLine("[상점 주인 아만다] : 말보다 무기로 기선제압! 우리 스타일 알지? ");
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();

            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < StoreWeapon.Count; i++)
            {
                if (Inventory.player(StoreWeapon[i]))  // 인벤토리에 아이템이 있는지 없는지 확인할 메서드가 필요
                {
                    table.AddRow(StoreWeapon[i].Name, StoreWeapon[i].Level , StoreWeapon[i].Job , StoreWeapon[i].Effect, StoreWeapon[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreWeapon[i].Name, StoreWeapon[i].Level, StoreWeapon[i].Job, StoreWeapon[i].Effect, StoreWeapon[i].Explanation, StoreWeapon[i].Price);
                }
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, StoreWeapon.Count);
                if (input == 0)
                {
                    GameManager.Instance.DisplayGameIntro();
                }
                else
                {
                    if (Inventory.xx(StoreWeapon[input - 1])) // 인벤토리에 아이템이 있는지 없는지 확인할 메서드가 필요
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= StoreWeapon[input - 1].Gold)  // 인벤토리에 골드 메서드 필요
                    {
                        player.Gold -= StoreWeapon[input - 1].Gold;
                        Inventory.Add(StoreWeapon[input - 1]);
                        Console.WriteLine("구매하는 중.. 잠시만 기다려주세요.");
                        Thread.Sleep(1000);
                        StoreMain();
                    }
                    else if (player.Gold < StoreWeapon[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }

        static void DisplayStoreArmor()
        {
            Console.Clear();
            ShowHighlightedText("방어구 상점");
            Console.WriteLine("[상점 주인 아만다] : 잘 막고 버텨야 때릴 시간도 생기는 법이지. ");
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();
        }

        static void DisplayStorePotion()
        {
            Console.Clear();
            ShowHighlightedText("물약 상점");
            Console.WriteLine("[상점 주인 아만다] : 방금 만든 따끈따끈한 물약 어때?");
            Console.WriteLine("[" + player.Name + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();
        }

        static void StoreSell()
        {
            Console.Clear();
            ShowHighlightedText("상점 - 판매");
            Console.WriteLine("[상점 주인 아만다] : 어디 보자.. 쓸만 한 게 있을까?");
            Console.WriteLine("[" + PlayerManager.nickname + "의 Gold]" + " : " + player.Gold + " G\n"); // 플레이어 메서드 가져오기
            Console.WriteLine();
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "효과", "설명", "Gold");


            table.Write();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 무기 판매");
            Console.WriteLine("2. 방어구 판매");
            Console.WriteLine("3. 물약 판매");
        }
    }
}

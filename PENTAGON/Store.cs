﻿using System;
using System.Collections.Generic;
using System.Data;
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
        private static List<Item> StoreWeapon = new List<Item>(); 
        private static List<Item> StoreArmor = new List<Item>(); 
        private static List<Item> StorePotion = new List<Item>();

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

        private static void ShowHighlightedText1(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static void ShowHighlightedText2(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        //private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        //{
        //    Console.Write(s1);
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.Write(s2);
        //    Console.ResetColor();
        //    Console.Write(s3);
        //}

        public void StoreSetting()
        {
            //// 1레벨 아이템 
            //WeaponItem oldsword = new WeaponItem("낡은 대검", 1, 0, JobType.JT_Warrior, 1, "공격력 +1", "빛을 잃은 검입니다.", 500, false);
            //WeaponItem woodwand = new WeaponItem("우드 완드", 1, 0, JobType.JT_Mage, 1, "공격력 +1", "마력이 아주 희미하게 남아있습니다.", 500, false);
            //WeaponItem dagger = new WeaponItem("단검", 1, 0, JobType.JT_Thief, 1, "공격력 +1", "산토끼라도 잡을 수 있을까요?", 500, false);
            //WeaponItem shortbow = new WeaponItem("숏보우", 1, 0, JobType.JT_Archer, 1, "공격력 +1", "나뭇가지를 모아 만들었습니다.", 500, false);

            //ArmorItem ironarmor = new ArmorItem("무쇠 갑옷", 1, 0, JobType.JT_Warrior, 2, 0, "방어력 +2", "추위를 겨우 막아내는 갑옷입니다.", 500, false);
            //ArmorItem oldrobe = new ArmorItem("덧댄 로브", 1, 0, JobType.JT_Mage, 2, 0, "방어력 +2", "세월의 흔적이 드러나는 로브.", 500, false);
            //ArmorItem oldsuit = new ArmorItem("허름한 도복", 1, 0, JobType.JT_Thief, 2, 0, "방어력 +2", "초급 닌자에게 어울립니다.", 500, false);
            //ArmorItem brownvest = new ArmorItem("갈색 조끼", 1, 0, JobType.JT_Archer, 2, 0, "방어력 +2", "오랜 사냥으로 해져서 펄럭입니다.", 500, false);

            //StoreWeapon.Add(oldsword);
            //StoreWeapon.Add(woodwand);
            //StoreWeapon.Add(dagger);
            //StoreWeapon.Add(shortbow);

            //StoreArmor.Add(ironarmor);
            //StoreArmor.Add(oldrobe);
            //StoreArmor.Add(oldsuit);
            //StoreArmor.Add(brownvest);


            // 무기 목록
            WeaponItem tuna = new WeaponItem("냉동참치", 2, 0, JobType.JT_Warrior, 3, "공격력 +3", "존재만으로 든든하지만 배를 채워주지는 못합니다.", 1000, false);
            WeaponItem ignis = new WeaponItem("이그니스", 2, 0, JobType.JT_Mage, 4, "공격력 +4", "불꽃 정령의 이름이 새겨진 정교한 지팡이.", 1000, false);
            WeaponItem dokata = new WeaponItem("노가다 목장갑", 2, 0, JobType.JT_Thief, 3, "공격력 +3", "수련이 필요한 자에게 안성맞춤.", 1000, false);
            WeaponItem deadshot = new WeaponItem("필살", 2, 0, JobType.JT_Archer, 3, "공격력 +3", "예리한 화살과 가벼운 활시위의 조화", 1000, false);

            WeaponItem goredrinker = new WeaponItem("선혈포식자", 4, 0, JobType.JT_Warrior, 6, "공격력 +6", "전장의 핏물이 이룬 살기로 가득합니다.", 2000, false);
            WeaponItem pinkvenom = new WeaponItem("핑크 베놈", 4, 0, JobType.JT_Mage, 7, "공격력 +7", "진분홍 살모사의 독이 서린 스태프.", 2000, false);
            WeaponItem asura = new WeaponItem("아수라", 4, 0, JobType.JT_Thief, 6, "공격력 +6", "영혼까지 저리게 만드는 날렵한 귀검.", 2000, false);
            WeaponItem windbreaker = new WeaponItem("윈드브레이커", 4, 0, JobType.JT_Archer, 6, "공격력 +6", "바람의 힘을 마음껏 부리세요.", 2000, false);

            WeaponItem lathander = new WeaponItem("라샌더의 광채", 5, 0, JobType.JT_Warrior, 9, "공격력 +9", "성스러운 광휘가 굽이치는 전설적인 창.", 3000, false);
            WeaponItem luden = new WeaponItem("루덴의 폭풍", 5, 0, JobType.JT_Mage, 10, "공격력 +10", "눈부신 메아리에 홀린 자들이 울부짖습니다.", 3000, false);
            WeaponItem eclipse = new WeaponItem("월식", 5, 0, JobType.JT_Thief, 9, "공격력 +9", "황혼이 머문 자리에 깃든 만월의 축복.", 3000, false);
            WeaponItem parkunas = new WeaponItem("천벌 파르쿠나스", 5, 0, JobType.JT_Archer, 9, "공격력 +9", "루페온이시여, 우리를 지켜주세요.", 3000, false);

            // 무기 Add
            StoreWeapon.Add(tuna);
            StoreWeapon.Add(ignis);
            StoreWeapon.Add(dokata);
            StoreWeapon.Add(deadshot);
            StoreWeapon.Add(goredrinker);
            StoreWeapon.Add(pinkvenom);
            StoreWeapon.Add(asura);
            StoreWeapon.Add(windbreaker);
            StoreWeapon.Add(lathander);
            StoreWeapon.Add(luden);
            StoreWeapon.Add(eclipse);
            StoreWeapon.Add(parkunas);

            // 방어구 목록
            ArmorItem thornmail = new ArmorItem("가시갑옷", 2, 0, JobType.JT_Warrior, 5, 15, "방어력 +5, 체력 +15", "날카로운 가시들의 부드러운 춤.", 1000, false);
            ArmorItem seraph = new ArmorItem("대천사의 포옹", 2, 0, JobType.JT_Mage, 5, 10, "방어력 +5, 체력 +10", "따뜻한 빛으로 끌어안아 정신까지 맑아집니다.", 1000, false);
            ArmorItem blackmist = new ArmorItem("블랙 미스트", 2, 0, JobType.JT_Thief, 5, 10, "방어력 +5, 체력 +10", "서늘한 그림자 속에서 취하는 달콤한 휴식.", 1000, false);
            ArmorItem thunderdash = new ArmorItem("번개질주", 2, 0, JobType.JT_Archer, 5, 10, "방어력 +5, 체력 +10", "누구보다 빛나고 싶은 자들의 우상.", 1000, false);

            ArmorItem goldenplate = new ArmorItem("황금갑옷", 4, 0, JobType.JT_Warrior, 10, 25, "방어력 +10, 체력 +25", "번뜩이는 흉갑에 적의 눈동자가 스칩니다.", 2000, false);
            ArmorItem symphonia = new ArmorItem("심포니아", 4, 0, JobType.JT_Mage, 10, 20, "방어력 +10, 체력 +20", "천상의 목소리에 담긴 찬란한 의지.", 2000, false);
            ArmorItem macabre = new ArmorItem("죽음의 무도", 4, 0, JobType.JT_Thief, 10, 20, "방어력 +10, 체력 +20", "칼바람도 멎게 하는 칠흑같은 적막.", 2000, false);
            ArmorItem omerta = new ArmorItem("오메르타", 4, 0, JobType.JT_Archer, 10, 20, "방어력 +10, 체력 +20", "새까만 하늘 아래 우아하게 피어난 성위.", 2000, false);

            // 방어구 Add 
            StoreArmor.Add(thornmail);
            StoreArmor.Add(seraph);
            StoreArmor.Add(blackmist);
            StoreArmor.Add(thunderdash);
            StoreArmor.Add(goldenplate);
            StoreArmor.Add(symphonia);
            StoreArmor.Add(macabre);
            StoreArmor.Add(omerta);

            // 포션 목록 (능력치와 골드는 임시값)
            PotionItem HpPotion = new PotionItem("Hp물약", 20, 0, 0, "HP +20", "사용 시 HP를 20 회복합니다.", 100);
            PotionItem MpPotion = new PotionItem("Mp물약", 0, 20, 0, "MP +20", "사용 시 MP를 20 회복합니다.", 100);

            // 포션 Add
            StorePotion.Add(HpPotion);
            StorePotion.Add(MpPotion);
        }
        
        // 상점 메인 화면
        public void StoreMain()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점");
            Console.WriteLine();
            Console.WriteLine("                         ∩__∩");
            Console.WriteLine("                         (^ㅅ^)");
            Console.WriteLine("                       ⊂l▶◀l⊃");
            Console.WriteLine("                         l    l");

            Console.SetCursorPosition(20, 6);
            ShowHighlightedText2("[상점 주인 아만다] : 어서오세요~ 없는 것 빼고 다 있는 상점입니다!");
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(25, 10);
            ShowHighlightedText1("1. 아이템 구매");
            Console.SetCursorPosition(25, 11);
            ShowHighlightedText1("2. 아이템 판매");
            Console.SetCursorPosition(25, 12);
            ShowHighlightedText1("0. 메인 화면");
            Console.WriteLine();
            Console.SetCursorPosition(25, 14);
            Console.WriteLine("여기서 아이템을 구매 또는 판매할 수 있습니다.");
            Console.SetCursorPosition(25, 15);
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

        // 상점 - 구매 화면
        static void StoreBuy()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 구매");
            Console.WriteLine();
            Console.WriteLine("                         ∩__∩");
            Console.WriteLine("                         (^ㅅ^)");
            Console.WriteLine("                       ⊂l▶◀l⊃");
            Console.WriteLine("                         l    l");
            Console.SetCursorPosition(20, 6);
            ShowHighlightedText2("[상점 주인 아만다] : 우리 물건이 제일 좋다구!");
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.SetCursorPosition(25, 10);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(25, 11);
            Console.WriteLine("1. 무기 구매");
            Console.SetCursorPosition(25, 12);
            Console.WriteLine("2. 방어구 구매");
            Console.SetCursorPosition(25, 13);
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

        // 상점 - 구매 - 무기 구매
        static void StoreBuyWeapon()
        {

            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 무기 구매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");
            ShowHighlightedText2("[상점 주인 아만다] : 말보다 무기로 기선제압! 우리 스타일 알지? ");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("무기", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < StoreWeapon.Count; i++)
            {
                string job = "전사";
                switch (StoreWeapon[i].JobType)
                {
                    case JobType.JT_Warrior:
                        job = "전사";
                        break;
                    case JobType.JT_Mage:
                        job = "마법사";
                        break;
                    case JobType.JT_Thief:
                        job = "도적";
                        break;
                    case JobType.JT_Archer:
                        job = "궁수";
                        break;
                }
                if (Program.player1.Inventory.weaponItem.Contains(StoreWeapon[i]))  // 인벤토리에 아이템이 있는지 확인 
                {
                    table.AddRow(StoreWeapon[i].Name, StoreWeapon[i].Level, job, StoreWeapon[i].Effect, StoreWeapon[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreWeapon[i].Name, StoreWeapon[i].Level, job, StoreWeapon[i].Effect, StoreWeapon[i].Explanation, StoreWeapon[i].Gold);
                }
            }
            table.Options.EnableCount = false;
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

        // 상점 - 구매 - 방어구 구매
        static void StoreBuyArmor()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 방어구 구매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");
            ShowHighlightedText2("[상점 주인 아만다] : 잘 막고 버텨야 때릴 시간도 생기는 법이지. ");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("방어구", "레벨", "직업", "효과", "설명", "Gold");
            for (int i = 0; i < StoreArmor.Count; i++)
            {
                string job = "전사";
                switch (StoreArmor[i].JobType)
                {
                    case JobType.JT_Warrior:
                        job = "전사";
                        break;
                    case JobType.JT_Mage:
                        job = "마법사";
                        break;
                    case JobType.JT_Thief:
                        job = "도적";
                        break;
                    case JobType.JT_Archer:
                        job = "궁수";
                        break;
                }
                if (Program.player1.Inventory.armorItem.Contains(StoreArmor[i]))  // 인벤토리에 아이템이 있는지 확인 
                {
                    table.AddRow(StoreArmor[i].Name, StoreArmor[i].Level, job, StoreArmor[i].Effect, StoreArmor[i].Explanation, "구매 완료");
                }
                else
                {
                    table.AddRow(i + 1 + ". " + StoreArmor[i].Name, StoreArmor[i].Level, job, StoreArmor[i].Effect, StoreArmor[i].Explanation, StoreArmor[i].Gold);
                }
            }

            table.Options.EnableCount = false;
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

        // 상점 - 구매 - 물약 구매
        static void StoreBuyPotion()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 물약 구매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");

            ShowHighlightedText2("[상점 주인 아만다] : 방금 만든 따끈따끈한 물약 어때?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("물약", "설명", "Gold");
            for (int i = 0; i < StorePotion.Count; i++)
            {
                table.AddRow(i + 1 + ". " + StorePotion[i].Name, StorePotion[i].Explanation, StorePotion[i].Gold);
            }

            table.Options.EnableCount = false;
            table.Write();
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, 2);
                if (input == 0)
                {
                    StoreBuy();
                }
                else
                {
                    if (Program.player1.Gold >= StorePotion[input - 1].Gold)
                    {
                        Program.player1.Gold -= StorePotion[input - 1].Gold;
                        Program.player1.Inventory.potionItem[input - 1].Count++;
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

        // 상점 - 판매 화면
        static void StoreSell()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 판매");
            Console.WriteLine();
            Console.WriteLine("                         ∩__∩");
            Console.WriteLine("                         (^ㅅ^)");
            Console.WriteLine("                       ⊂l▶◀l⊃");
            Console.WriteLine("                         l    l");
            Console.SetCursorPosition(20, 6);
            ShowHighlightedText2("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine();
            Console.SetCursorPosition(25, 10);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(25, 11);
            Console.WriteLine("1. 무기 판매");
            Console.SetCursorPosition(25, 12);
            Console.WriteLine("2. 방어구 판매");
            Console.SetCursorPosition(25, 13);
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

        // 상점 - 판매 - 무기 판매
        static void StoreSellWeapon()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 무기 판매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");
            ShowHighlightedText2("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "직업", "효과", "설명", "판매가");
            for (int i = 0; i < Program.player1.Inventory.weaponItem.Count; i++)
            {
                string job = "전사";
                switch (Program.player1.Inventory.weaponItem[i].JobType)
                {
                    case JobType.JT_Warrior:
                        job = "전사";
                        break;
                    case JobType.JT_Mage:
                        job = "마법사";
                        break;
                    case JobType.JT_Thief:
                        job = "도적";
                        break;
                    case JobType.JT_Archer:
                        job = "궁수";
                        break;
                }
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.weaponItem[i].Name, Program.player1.Inventory.weaponItem[i].Level, job, Program.player1.Inventory.weaponItem[i].Effect, Program.player1.Inventory.weaponItem[i].Explanation, 0.7*(Program.player1.Inventory.weaponItem[i].Gold));
            }

            table.Options.EnableCount = false;
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
                    if (Program.player1.Inventory.weaponItem[input - 1].IsEquip)
                    {
                        Program.player1.AttackDamage -= Program.player1.Inventory.weaponItem[input - 1].Atk;
                        Program.player1.Inventory.weaponItem[input - 1].IsEquip = false;
                    }
                    Program.player1.Inventory.weaponItem.Remove(Program.player1.Inventory.weaponItem[input - 1]);
                    Console.WriteLine("무기를 판매했습니다.");
                    Thread.Sleep(1000);
                    StoreSellWeapon();
                }
            }
        }

        // 상점 - 판매 - 방어구 판매
        static void StoreSellArmor()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 방어구 판매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");
            ShowHighlightedText2("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "레벨", "직업", "효과", "설명", "판매가");
            for (int i = 0; i < Program.player1.Inventory.armorItem.Count; i++)
            {
                string job = "전사";
                switch (Program.player1.Inventory.armorItem[i].JobType)
                {
                    case JobType.JT_Warrior:
                        job = "전사";
                        break;
                    case JobType.JT_Mage:
                        job = "마법사";
                        break;
                    case JobType.JT_Thief:
                        job = "도적";
                        break;
                    case JobType.JT_Archer:
                        job = "궁수";
                        break;
                }
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.armorItem[i].Name, Program.player1.Inventory.armorItem[i].Level, job, Program.player1.Inventory.armorItem[i].Effect, Program.player1.Inventory.armorItem[i].Explanation, 0.7*(Program.player1.Inventory.armorItem[i].Gold));
            }
            table.Options.EnableCount = false;
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
                    if (Program.player1.Inventory.armorItem[input - 1].IsEquip)
                    {
                        //Program.player1.Defence -= Program.player1.Inventory.equipArmorItem[input - 1].Def;
                        Program.player1.Defence -= Program.player1.Inventory.armorItem[input - 1].Def;
                        Program.player1.MaxHp -= Program.player1.Inventory.armorItem[input - 1].MaxHp;
                        Program.player1.Inventory.armorItem[input - 1].IsEquip = false;
                    }
                    Program.player1.Inventory.armorItem.Remove(Program.player1.Inventory.armorItem[input - 1]);
                    Console.WriteLine("방어구를 판매했습니다.");
                    Thread.Sleep(1000);
                    StoreSellArmor();
                }
            }
        }

        // 상점 - 판매 - 물약 판매
        static void StoreSellPotion()
        {
            Console.Clear();
            Console.SetCursorPosition(47, 0);
            ShowHighlightedText1("상점 - 물약 판매");
            Console.WriteLine();
            Console.WriteLine("      ∩__∩");
            Console.WriteLine("      (^ㅁ^)");
            Console.WriteLine("    ⊂l▶◀l⊃");
            Console.WriteLine("      l    l");
            ShowHighlightedText2("[상점 주인 아만다] : 흠.. 쓸만한 게 있나 볼까?");
            Console.WriteLine("[" + Program.player1.Name + "의 Gold]" + " : " + Program.player1.Gold + " G\n");
            Console.WriteLine("\n[아이템 목록]");
            ConsoleTable table = new ConsoleTable("아이템 이름", "설명", "판매가");
            for (int i = 0; i < Program.player1.Inventory.potionItem.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Program.player1.Inventory.potionItem[i].Name + " x" + Program.player1.Inventory.potionItem[i].Count, Program.player1.Inventory.potionItem[i].Explanation, 0.7*(Program.player1.Inventory.potionItem[i].Gold));
            }
            table.Options.EnableCount = false;
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (true) //판매할 아이템 선택
            {
                Console.WriteLine();
                Console.WriteLine("판매할 아이템을 선택해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Program.player1.Inventory.potionItem.Count);
                if (input == 0)
                {
                    StoreSell();
                }
                else
                {
                    if (Program.player1.Inventory.potionItem[input - 1].Count != 0)
                    {
                        Program.player1.Gold += Program.player1.Inventory.potionItem[input - 1].Gold * 70 / 100;
                        Program.player1.Inventory.potionItem[input - 1].Count--;
                        Console.WriteLine("아이템을 판매했습니다.");
                    }
                    else if (Program.player1.Inventory.potionItem[input - 1].Count == 0)
                    {
                        Console.WriteLine("판매할 수 없습니다.");
                    }
                    Thread.Sleep(1000);
                    StoreSellPotion();
                }
            }
        }
    }
}

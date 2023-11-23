using ConsoleTables;
using EnumsNamespace;
using Newtonsoft.Json;
using PENTAGON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Inventory
    {

        Random rand = new Random();
        //List<Item> inventory = new List<Item>();
        public List<WeaponItem> weaponItem = new List<WeaponItem>();
        public List<ArmorItem> armorItem = new List<ArmorItem>();
        public List<PotionItem> potionItem = new List<PotionItem>();
        public List<WeaponItem> equipWeaponItem = new List<WeaponItem>();
        public List<ArmorItem> equipArmorItem = new List<ArmorItem>();

        int forceReset = 0;
        //InventorySetting
        //weapon
        //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
        //armor
        //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
        //potion
        //이름, 힐, MP, 효과, 설명, 골드
        public void ItemSetting()
        {
            // 주석 해제하면 기본 아이템 제공
            switch (Program.player1.JobType)
            {
                case JobType.JT_Warrior:
                    WeaponItem oldSword = new WeaponItem("낡은 대검", 1, 0, JobType.JT_Warrior, 1, "공격력 +1", "빛을 잃은 검입니다.", 100, false);
                    weaponItem.Add(oldSword);
                    ArmorItem ironArmor = new ArmorItem("무쇠 갑옷", 1, 0, JobType.JT_Warrior, 2, 0, "방어력 +2", "추위를 겨우 막아내는 갑옷입니다.", 100, false);
                    armorItem.Add(ironArmor);
                    break;
                case JobType.JT_Mage:
                    WeaponItem woodenStick = new WeaponItem("우드 완드", 1, 0, JobType.JT_Mage, 1, "공격력 +1", "마력이 아주 희미하게 남아있습니다.", 100, false);
                    weaponItem.Add(woodenStick);
                    ArmorItem shabbyClothes = new ArmorItem("덧댄 로브", 1, 0, JobType.JT_Mage, 2, 0, "방어력 +2", "세월의 흔적이 드러나는 로브.", 100, false);
                    armorItem.Add(shabbyClothes);
                    break;
                case JobType.JT_Thief:
                    WeaponItem dagger = new WeaponItem("단검", 1, 0, JobType.JT_Thief, 1, "공격력 +1", "산토끼라도 잡을 수 있을까요?", 100, false);
                    weaponItem.Add(dagger);
                    ArmorItem ShabbyNinjaClothes = new ArmorItem("허름한 도복", 1, 0, JobType.JT_Thief, 2, 0, "방어력 +2", "초급 닌자에게 어울립니다.", 100, false);
                    armorItem.Add(ShabbyNinjaClothes);
                    break;
                case JobType.JT_Archer:
                    WeaponItem woodenBow = new WeaponItem("숏보우", 1, 0, JobType.JT_Archer, 1, "공격력 +1", "나뭇가지를 모아 만들었습니다.", 100, false);
                    weaponItem.Add(woodenBow);
                    ArmorItem oldHunterClothes = new ArmorItem("갈색 조끼", 1, 0, JobType.JT_Archer, 2, 0, "방어력 +2", "오랜 사냥으로 해져서 펄럭입니다.", 100, false);
                    armorItem.Add(oldHunterClothes);
                    break;
            }
            //string name, int gold, string explanation, int heal
            PotionItem HpPotion = new PotionItem("Hp물약", 20, 0, 2, "Hp +20", "물약을 먹으면 Hp가 회복됩니다.", 100);
            potionItem.Add(HpPotion);

            PotionItem MpPotion = new PotionItem("Mp물약", 0, 20, 2, "Mp +20", "물약을 먹으면 Mp가 회복됩니다.", 100);
            potionItem.Add(MpPotion);
        }

        //인벤토리 메인
        public void DispayInventoryMain()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.SetCursorPosition(43, 2);
            Console.WriteLine("1. 무기 인벤토리");
            Console.SetCursorPosition(43, 3);
            Console.WriteLine("2. 방어구 인벤토리");
            Console.SetCursorPosition(43, 4);
            Console.WriteLine("3. 포션 인벤토리");
            Console.SetCursorPosition(43, 5);
            Console.WriteLine("4. 무기 강화");
            Console.SetCursorPosition(43, 6);
            Console.WriteLine("5. 방어구 강화");
            Console.SetCursorPosition(43, 7);
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(40, 11);
            Console.Write(">>");

            int input = CheckValidInput(0, 5);
            switch (input)
            {
                case 0:
                    //0. 나가기 - 메인화면
                    GameManager.Instance.DisplayGameIntro();
                    break;
                case 1:
                    //1. 무기 인벤토리
                    WeaponInventory1(weaponItem);
                    break;
                case 2:
                    //2. 방어구 인벤토리
                    ArmorInventory1(armorItem);
                    break;
                case 3:
                    //3. 기타 인벤토리
                    ETCInventory();
                    break;
                case 4:
                    WeaponItemEnhancement();
                    break;
                case 5:
                    ArmorItemEnhancement();
                    break;
            }
        }

        //무기 인벤토리
        public void WeaponInventory1(List<WeaponItem> weaponItem)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < weaponItem.Count; i++)
            {
                string job = "전사";
                switch (weaponItem[i].JobType)
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
                if (weaponItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{job}", $"공격력 +{weaponItem[i].Atk}", $"{weaponItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{job}", $"공격력{weaponItem[i].Atk}", $"{weaponItem[i].Explanation}");
                }
            }
            table.Write();

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 능력치 높은 순으로 정렬");
            Console.WriteLine();
            for (int i = 0; i < weaponItem.Count; i++)
            {
                Console.WriteLine($"{i + 2}. {weaponItem[i].Name} 장착/해제");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, weaponItem.Count + 1);
            if (input == 0)
            {
                //인벤토리 메인
                DispayInventoryMain();
            }
            else if (input == 1)
            {
                //방어력 높은 순으로 정렬
                List<WeaponItem> weaponItemSort = weaponItem.OrderByDescending(x => x.Def).ToList();
                WeaponInventory1(weaponItemSort);
            }
            else
            {
                if ((weaponItem[input - 2].Level <= Program.player1.Level) && (Program.player1.JobType == weaponItem[input - 2].JobType))
                {
                    if (weaponItem[input - 2].IsEquip == false)
                    {
                        weaponItem[input - 2].IsEquip = true;
                        if (equipWeaponItem.Count == 0)
                        {
                            equipWeaponItem.Add(weaponItem[input - 2]);
                        }
                        if (equipWeaponItem[0] != weaponItem[input - 2])
                        {
                            if (equipWeaponItem[0].IsEquip)
                            {
                                Program.player1.AttackDamage -= equipWeaponItem[0].Atk;
                            }
                            equipWeaponItem[0].IsEquip = false;
                            equipWeaponItem.RemoveAt(0);
                            equipWeaponItem.Add(weaponItem[input - 2]);
                        }
                        Program.player1.AttackDamage += equipWeaponItem[0].Atk;
                    }
                    else
                    {
                        equipWeaponItem[0].IsEquip = false;
                        weaponItem[input - 2].IsEquip = false;

                        Program.player1.AttackDamage -= equipWeaponItem[0].Atk;
                        equipWeaponItem.RemoveAt(0);
                    }
                }
                else if (Program.player1.JobType != weaponItem[input - 2].JobType)
                {
                    Console.WriteLine($"직업이 맞지 않습니다.");
                }
                else if (weaponItem[input - 2].Level >= Program.player1.Level)
                {
                    Console.WriteLine($"레벨이 낮습니다.");
                }
                Thread.Sleep(1000);
                WeaponInventory1(weaponItem);
            }
        }

        // 방어구 인벤토리 장착 및 정렬
        public void ArmorInventory1(List<ArmorItem> armorItem)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < armorItem.Count; i++)
            {
                string job = "전사";
                switch (armorItem[i].JobType)
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
                if (armorItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {armorItem[i].Name} ", $"{armorItem[i].Level}", $"{job}", $"방어력 +{armorItem[i].Def}, 체력 + {armorItem[i].MaxHp}", $"{armorItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"{armorItem[i].Level}", $"{job}", $"방어력 +{armorItem[i].Def}, 체력 + {armorItem[i].MaxHp}", $"{armorItem[i].Explanation}");
                }
            }
            table.Write();

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 능력치 높은 순으로 정렬");
            Console.WriteLine();
            for (int i = 0; i < armorItem.Count; i++)
            {
                Console.WriteLine($"{i + 2}. {armorItem[i].Name} 장착/해제");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, armorItem.Count + 1);
            if (input == 0)
            {
                //인벤토리 메인
                DispayInventoryMain();
            }
            else if (input == 1)
            {
                //방어력 높은 순으로 정렬
                List<ArmorItem> armorItemSort = armorItem.OrderByDescending(x => x.Def).ToList();
                ArmorInventory1(armorItemSort);
            }
            else
            {
                if ((armorItem[input - 2].Level <= Program.player1.Level) && (Program.player1.JobType == armorItem[input - 2].JobType))
                {
                    if (armorItem[input - 2].IsEquip == false)
                    {
                        armorItem[input - 2].IsEquip = true;
                        if (equipArmorItem.Count == 0)
                        {
                            equipArmorItem.Add(armorItem[input - 2]);
                        }
                        if (equipArmorItem[0] != armorItem[input - 2])
                        {
                            if (equipArmorItem[0].IsEquip)
                            {
                                Program.player1.Defence -= equipArmorItem[0].Def;
                                Program.player1.MaxHp -= equipArmorItem[0].MaxHp;
                            }
                            equipArmorItem[0].IsEquip = false;
                            equipArmorItem.RemoveAt(0);
                            equipArmorItem.Add(armorItem[input - 2]);
                        }
                        Program.player1.Defence += equipArmorItem[0].Def;
                        Program.player1.MaxHp += equipArmorItem[0].MaxHp;
                    }
                    else
                    {
                        equipArmorItem[0].IsEquip = false;
                        armorItem[input - 2].IsEquip = false;

                        Program.player1.Defence -= equipArmorItem[0].Def;
                        Program.player1.MaxHp -= equipArmorItem[0].MaxHp;
                        equipArmorItem.RemoveAt(0);
                    }
                }
                else if (Program.player1.JobType != armorItem[input - 2].JobType)
                {
                    Console.WriteLine($"직업이 맞지 않습니다.");
                }
                else if (armorItem[input - 2].Level >= Program.player1.Level)
                {
                    Console.WriteLine($"레벨이 낮습니다.");
                }
                Thread.Sleep(1000);
                ArmorInventory1(armorItem);
            }
        }

        //기타 인벤토리 - 물약
        public void ETCInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리/기타 아이템");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < potionItem.Count; i++)
            {
                if (potionItem[i].Count > 0)
                {
                    table.AddRow($"{potionItem[i].Name} x{potionItem[i].Count} ", $"{potionItem[i].Effect}", $"{potionItem[i].Explanation}");
                }
            }
            table.Write();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            for (int i = 0; i < potionItem.Count; i++)
            {
                if (potionItem[i].Count > 0)
                {
                    Console.WriteLine($"{i + 1}. {potionItem[i].Name} 먹기");
                }

            }

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, potionItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                EatPotion(potionItem[input - 1]);
                Console.WriteLine($"{potionItem[input - 1].Name} 먹었습니다");
                Thread.Sleep(1000);
                ETCInventory();
            }
        }

        //무기 강화
        public void WeaponItemEnhancement()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리/무기 강화");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명", "강화 비용");
            table.Options.EnableCount = false;

            for (int i = 0; i < weaponItem.Count; i++)
            {
                string job = "전사";
                switch (weaponItem[i].JobType)
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
                if (weaponItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{job}", $"공격력 +{weaponItem[i].Atk}", $"{weaponItem[i].Explanation}", $"{weaponItem[i].Gold / 2}G");
                }
                else
                {
                    table.AddRow($"{weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{job}", $"공격력 +{weaponItem[i].Atk}", $"{weaponItem[i].Explanation}", $"{weaponItem[i].Gold / 2}G");
                }
            }
            table.Write();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("※주의※ 실패시 아이템이 파괴되어 없어집니다.");
            Console.ResetColor();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            for (int i = 0; i < weaponItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weaponItem[i].Name} 강화하기");
            }
            Console.WriteLine();
            Console.WriteLine($"Gold: {Program.player1.Gold}G");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, weaponItem.Count + 1);

            if (input == 0)
            {
                //인벤토리 메인
                DispayInventoryMain();
            }
            else
            {
                //if (equipWeaponItem[0] != weaponItem[input - 1] && !weaponItem[input - 1].IsEquip)

                if (!weaponItem[input - 1].IsEquip)
                {
                    if (Program.player1.Gold >= weaponItem[input - 1].Gold * 0.5)
                    {
                        Program.player1.Gold -= weaponItem[input - 1].Gold / 2;
                        int randValue = rand.Next(4); //50%
                        if (randValue == 0)  //파괴
                        {
                            string weaponName = weaponItem[input - 1].Name;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"강화에 실패하여 {weaponItem[input - 1].Name}이(가) 파괴되었습니다.");
                            Console.ResetColor();
                            for (int i = 0; i < weaponItem[input - 1].Force; i++)
                            {
                                //weaponItem[input - 1].Name = weaponItem[input + 1].Name.Replace("★", "");
                                //weaponName = weaponName.Replace("★", "");
                                weaponItem[input - 1].Atk -= weaponItem[input - 1].Level;
                            }
                            StringBuilder modifiedStringBuilder = new StringBuilder();
                            foreach (char c in weaponItem[input - 1].Name)
                            {
                                if (c != '★')
                                {
                                    modifiedStringBuilder.Append(c);
                                }
                            }
                            weaponItem[input - 1].Name = modifiedStringBuilder.ToString();
                            weaponItem[input - 1].Force = forceReset;
                            weaponItem.Remove(weaponItem[input - 1]);
                        }
                        else if (randValue == 1) // 실패
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{weaponItem[input - 1].Name} 강화에 실패하였습니다.");
                            Console.ResetColor();
                        }
                        else // 50% 성공
                        {
                            Console.WriteLine("강화성공!!");
                            weaponItem[input - 1].Force++;

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"기존 공격력: +{weaponItem[input - 1].Atk}");
                            weaponItem[input - 1].Atk += weaponItem[input - 1].Level;
                            Console.Write($" -> 현재 공격력: +{weaponItem[input - 1].Atk}");
                            Console.ResetColor();
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            weaponItem[input - 1].Name = weaponItem[input - 1].Name + "★";
                            Console.WriteLine($"{weaponItem[input - 1].Name}을 얻으셨습니다.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine($"{weaponItem[input - 1].Name}을 장착 해제 해주세요");
                }
                Thread.Sleep(1000);
                WeaponItemEnhancement();
            }
        }
        // 방어구 강화
        public void ArmorItemEnhancement()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 0);
            Console.WriteLine("인벤토리/방어구 강화");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명", "강화 비용");
            table.Options.EnableCount = false;

            for (int i = 0; i < armorItem.Count; i++)
            {
                string job = "전사";
                switch (armorItem[i].JobType)
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
                if (armorItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {armorItem[i].Name} ", $"{armorItem[i].Level}", $"{job}", $"방어력 +{armorItem[i].Def}, 체력 +{armorItem[i].MaxHp}", $"{armorItem[i].Explanation}", $"{armorItem[i].Gold / 2}G");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"{armorItem[i].Level}", $"{job}", $"방어력 +{armorItem[i].Def}, 체력 +{armorItem[i].MaxHp}", $"{armorItem[i].Explanation}", $"{armorItem[i].Gold / 2}G");
                }
            }
            table.Write();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("※주의※ 실패시 아이템이 파괴되어 없어집니다.");
            Console.ResetColor();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            for (int i = 0; i < armorItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armorItem[i].Name} 강화하기");
            }
            Console.WriteLine();
            Console.WriteLine($"Gold: {Program.player1.Gold}G");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, armorItem.Count + 1);

            if (input == 0)
            {
                //인벤토리 메인
                DispayInventoryMain();
            }
            else
            {
                //if (equipArmorItem[0] != armorItem[input - 1] && !armorItem[input - 1].IsEquip)
                if (!armorItem[input - 1].IsEquip)
                {
                    if (Program.player1.Gold >= armorItem[input - 1].Gold * 0.5)
                    {
                        Program.player1.Gold -= armorItem[input - 1].Gold / 2;
                        int randValue = rand.Next(2); //50%
                        if (randValue == 0)  //파괴
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"강화에 실패하여 {armorItem[input - 1].Name}이(가) 파괴되었습니다.");
                            Console.ResetColor();
                            for (int i = 0; i < armorItem[input - 1].Force; i++)
                            {
                                //weaponItem[input - 1].Name = weaponItem[input + 1].Name.Replace("★", "");
                                //weaponName = weaponName.Replace("★", "");
                                armorItem[input - 1].Def -= armorItem[input - 1].Level;
                                armorItem[input - 1].MaxHp -= armorItem[input - 1].Level;
                            }
                            StringBuilder modifiedStringBuilder = new StringBuilder();
                            foreach (char c in armorItem[input - 1].Name)
                            {
                                if (c != '★')
                                {
                                    modifiedStringBuilder.Append(c);
                                }
                            }
                            armorItem[input - 1].Name = modifiedStringBuilder.ToString();
                            armorItem[input - 1].Force = forceReset;
                            armorItem.Remove(armorItem[input - 1]);
                        }
                        else if (randValue == 1) // 실패
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{armorItem[input - 1].Name} 강화에 실패하였습니다.");
                            Console.ResetColor();
                        }
                        else // 50% 성공
                        {
                            Console.WriteLine("강화성공!!");
                            armorItem[input - 1].Force++;

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"기존 공격력: +{armorItem[input - 1].Def}");
                            armorItem[input - 1].Def += armorItem[input - 1].Level;
                            Console.Write($" -> 현재 공격력: +{armorItem[input - 1].Def}");
                            Console.ResetColor();
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"기존 공격력: +{armorItem[input - 1].MaxHp}");
                            armorItem[input - 1].MaxHp += armorItem[input - 1].Level;
                            Console.Write($" -> 현재 공격력: +{armorItem[input - 1].MaxHp}");
                            Console.ResetColor();
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            armorItem[input - 1].Name = armorItem[input - 1].Name + "★";
                            Console.WriteLine($"{armorItem[input - 1].Name}을 얻으셨습니다.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine($"{armorItem[input - 1].Name}을 장착 해제 해주세요");
                }
                //골드가 있는지?

                Thread.Sleep(1000);
                ArmorItemEnhancement();
            }
        }
        //포션 먹기
        public void EatPotion(PotionItem potion)
        {
            if (potion.Count >= 1)//포션이 1개 이상이면 Count--;
            {
                if (potion.Heal != 0) //Hp를 회복해주면 ~
                {
                    Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
                    Console.WriteLine($"{potion.Name}을  사용했습니다.");
                    Console.WriteLine($"현재 체력 : {Program.player1.Hp} / {Program.player1.MaxHp}");
                }
                else if (potion.Mp != 0) //Mp를 회복해주면 ~
                {
                    Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
                    Console.WriteLine($"{potion.Name}을  사용했습니다.");
                    Console.WriteLine($"현재 Mp : {Program.player1.Mp} / {Program.player1.MaxMp}");
                }
                potion.Count--;
            }
            else if (potion.Count == 0)
            {
                Console.WriteLine("물약이 없습니다.");
            }
        }

        //입력값 확인
        public static int CheckValidInput(int min, int max)
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
    }
}

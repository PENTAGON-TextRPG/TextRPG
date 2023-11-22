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
        //List<Item> inventory = new List<Item>();
        public List<WeaponItem> weaponItem = new List<WeaponItem>();
        public List<ArmorItem> armorItem = new List<ArmorItem>();
        public List<PotionItem> potionItem = new List<PotionItem>();
        public List<WeaponItem> equipWeaponItem = new List<WeaponItem>();
        public List<ArmorItem> equipArmorItem = new List<ArmorItem>();

        //InventorySetting
        //weapon
        //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
        //armor
        //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
        //potion
        //이름, 힐, MP, 효과, 설명, 골드
        public void ItemSetting()
        {
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
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. 무기 인벤토리");
            Console.WriteLine("2. 방어구 인벤토리");
            Console.WriteLine("3. 포션 인벤토리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 3);
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
                    //무기 정렬
                    ArmorInventory1(armorItem);
                    break;
                case 3:
                    //2. 방어구 인벤토리
                    ETCInventory();
                    break;
            }
        }

        //무기 인벤토리
        public void WeaponInventory1(List<WeaponItem> weaponItem)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < weaponItem.Count; i++)
            {
                if (weaponItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{weaponItem[i].JobType}", $"{weaponItem[i].Effect}", $"{weaponItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{weaponItem[i].Name} ", $"{weaponItem[i].Level}", $"{weaponItem[i].JobType}", $"{weaponItem[i].Effect}", $"{weaponItem[i].Explanation}");
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
                                Program.player1.Defence -= equipWeaponItem[0].Def;
                                Program.player1.MaxHp -= equipWeaponItem[0].MaxHp;
                            }
                            equipWeaponItem[0].IsEquip = false;
                            equipWeaponItem.RemoveAt(0);
                            equipWeaponItem.Add(weaponItem[input - 2]);
                        }
                        Program.player1.Defence += equipWeaponItem[0].Def;
                        Program.player1.MaxHp += equipWeaponItem[0].MaxHp;
                    }
                    else
                    {
                        equipWeaponItem[0].IsEquip = false;
                        weaponItem[input - 2].IsEquip = false;

                        Program.player1.Defence -= equipWeaponItem[0].Def;
                        Program.player1.MaxHp -= equipWeaponItem[0].MaxHp;
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
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "레벨", "직업", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < armorItem.Count; i++)
            {
                if (armorItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {armorItem[i].Name} ", $"{armorItem[i].Level}", $"{armorItem[i].JobType}", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"{armorItem[i].Level}", $"{armorItem[i].JobType}", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
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

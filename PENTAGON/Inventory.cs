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
            switch (Program.player1.JobType)
            {
                case JobType.JT_Warrior:
                    WeaponItem oldSword = new WeaponItem("낡은 검", 0, JobType.JT_Warrior, 1, "공격력 +1", "빛을 잃은 검입니다.", 100, false);
                    weaponItem.Add(oldSword);

                    ArmorItem ironArmor = new ArmorItem("무쇠 갑옷", 0, JobType.JT_Warrior, 2, 0, "방어력 +2", "추위를 겨우 막아내는 갑옷입니다.", 100, false);
                    armorItem.Add(ironArmor);
                    
                    WeaponItem testWeaponItem = new WeaponItem("Test WeaponItem", 0, JobType.JT_Warrior, 50, "공격력 +50", "TestItem 입니다.", 100000, false);
                    weaponItem.Add(testWeaponItem);

                    ArmorItem testArmorItem = new ArmorItem("Test ArmorItem", 0, JobType.JT_Warrior, 50, 50, "방어력 +50, cpfur +50", "TestItem 입니다.", 100000, false);
                    armorItem.Add(testArmorItem);
                    break;
                case JobType.JT_Mage:
                    WeaponItem woodenStick = new WeaponItem("나무 막대기", 0, JobType.JT_Mage, 1, "공격력 +1", "마력이 아주 희미한 지팡이입니다.", 100, false);
                    weaponItem.Add(woodenStick);

                    ArmorItem shabbyClothes = new ArmorItem("허름한 옷", 0, JobType.JT_Mage, 2, 0, "방어력 +2", "허름한 옷입니다.", 100, false);
                    armorItem.Add(shabbyClothes);
                    break;
                case JobType.JT_Thief:
                    WeaponItem dagger = new WeaponItem("단검", 0, JobType.JT_Thief, 1, "공격력 +1", "흔히 볼 수 있는 단검입니다.", 100, false);
                    weaponItem.Add(dagger);

                    ArmorItem ShabbyNinjaClothes = new ArmorItem("허름한 닌자 옷", 0, JobType.JT_Thief, 2, 0, "방어력 +2", "초급 닌자에게 어울리는 옷입니다.", 100, false);
                    armorItem.Add(ShabbyNinjaClothes);
                    break;
                case JobType.JT_Archer:
                    WeaponItem woodenBow = new WeaponItem("나무 활", 0, JobType.JT_Archer, 1, "공격력 +1", "산에서 주워온 나뭇가지로 만들었습니다.", 100, false);
                    weaponItem.Add(woodenBow);

                    ArmorItem oldHunterClothes = new ArmorItem("낡은 사냥꾼 옷", 0, JobType.JT_Archer, 2, 0, "방어력 +2", "오랜 사냥으로 해져서 펄럭입니다.", 100, false);
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
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. 무기 인벤토리");
            Console.WriteLine("2. 무기 인벤토리 정렬");
            Console.WriteLine("3. 방어구 인벤토리");
            Console.WriteLine("4. 방어구 인벤토리 정렬");
            Console.WriteLine("5. 포션 인벤토리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
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
                    WeaponInventory();
                    break;
                case 2:
                    //무기 정렬
                    WeaponInventorySort(weaponItem);
                    break;
                case 3:
                    //2. 방어구 인벤토리
                    ArmorInventory();
                    break;
                case 4:
                    //방어구 정렬
                    ArmorInventorySort(armorItem);
                    break;
                case 5:
                    ETCInventory();
                    break;
            }
        }

        //weaponInventory 화면 출력
        public void DisplayWeaponInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/무기");
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
        }

        //무기 인벤토리 - 무기 장착 및 해제
        public void WeaponInventory()
        {
            DisplayWeaponInventory();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            for (int i = 0; i < weaponItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weaponItem[i].Name} 장착/해제");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, weaponItem.Count);
            if (input == 0)
            {
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                if ((weaponItem[input - 1].Level <= Program.player1.Level) && (Program.player1.JobType == weaponItem[input - 1].JobType))
                {
                    if (weaponItem[input - 1].IsEquip == false)
                    {
                        weaponItem[input - 1].IsEquip = true;
                        if (equipWeaponItem.Count == 0)
                        {
                            equipWeaponItem.Add(weaponItem[input - 1]);
                        }

                        if (equipWeaponItem[0] != weaponItem[input - 1])
                        {
                            if (equipWeaponItem[0].IsEquip)
                            {
                                Program.player1.AttackDamage -= equipWeaponItem[0].Atk;
                            }
                            equipWeaponItem[0].IsEquip = false;
                            equipWeaponItem.RemoveAt(0);
                            equipWeaponItem.Add(weaponItem[input - 1]);
                        }
                        Program.player1.AttackDamage += equipWeaponItem[0].Atk;
                    }
                    else
                    {
                        equipWeaponItem[0].IsEquip = false;
                        weaponItem[input - 1].IsEquip = false;

                        Program.player1.AttackDamage -= equipWeaponItem[0].Atk;
                        equipWeaponItem.RemoveAt(0);
                    }
                    //IsEquipWeaponItem(weaponItem, equipWeaponItem, input);
                }
                else if (Program.player1.JobType != weaponItem[input - 1].JobType)
                {
                    Console.WriteLine($"직업이 맞지 않습니다.");
                }
                else if (weaponItem[input - 1].Level >= Program.player1.Level)
                {
                    Console.WriteLine($"레벨이 낮습니다.");
                }
                Thread.Sleep(1000);
                WeaponInventory();
            }
        }
        //public void IsEquipWeaponItem(List<WeaponItem> _weaponItem, List<WeaponItem> _equipWeaponItem, int Index)
        //{
        //    if (_weaponItem[Index - 1].IsEquip == false)
        //    {
        //        _weaponItem[Index - 1].IsEquip = true;
        //        if (_equipWeaponItem.Count == 0)
        //        {
        //            _equipWeaponItem.Add(_weaponItem[Index - 1]);
        //        }

        //        if (_equipWeaponItem[0] != _weaponItem[Index - 1])
        //        {
        //            Program.player1.AttackDamage -= _equipWeaponItem[0].Atk;
        //            _equipWeaponItem[0].IsEquip = false;
        //            _equipWeaponItem.RemoveAt(0);
        //            _equipWeaponItem.Add(_weaponItem[Index - 1]);
        //        }
        //        Program.player1.AttackDamage += _equipWeaponItem[0].Atk;
        //    }
        //    else
        //    {
        //        _equipWeaponItem[0].IsEquip = false;
        //        _weaponItem[Index - 1].IsEquip = false;

        //        Program.player1.AttackDamage -= _equipWeaponItem[0].Atk;
        //        _equipWeaponItem.RemoveAt(0);
        //    }
        //}

        //Store.cs - 430, 472
        //  430
        //if (Program.player1.Inventory.armorItem[input - 1].IsEquip)
        //{
        //    Program.player1.Defence -= Program.player1.Inventory.armorItem[input - 1].Def;
        //}
        // 변경사항
        //if (Program.player1.Inventory.equipWeaponItem.Count != 0)
        //{
        //    Program.player1.AttackDamage -= Program.player1.Inventory.equipWeaponItem[0].Atk;
        //    Program.player1.Inventory.equipWeaponItem.RemoveAt(0);
        //}
        //  472
        //if (Program.player1.Inventory.armorItem[input - 1].IsEquip)
        //{
        //    Program.player1.Defence -= Program.player1.Inventory.armorItem[input - 1].Def;
        //}
        // 변경사항
        //if (Program.player1.Inventory.equipWeaponItem.Count != 0)
        //{
        //    Program.player1.AttackDamage -= Program.player1.Inventory.equipWeaponItem[0].Atk;
        //    Program.player1.Inventory.equipWeaponItem.RemoveAt(0);
        //}
        //if (Program.player1.Inventory.armorItem[input - 1].IsEquip)
        //{
        //    Program.player1.Defence -= Program.player1.Inventory.armorItem[input - 1].Def;
        //    Program.player1.Inventory.armorItem[input - 1].IsEquip = false;
        //}

    //ArmorInventory 화면 출력
    public void DisplayArmorInventory()
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
        }

        //인벤토리 정렬
        public void WeaponInventorySort(List<WeaponItem> weaponItem)
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
            Console.WriteLine();
            Console.WriteLine("1. 공격력 높은 순으로 정렬");
            Console.WriteLine("2. 공격력 낮은 순으로 정렬");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //공격력 높은 순으로 정렬
                    List<WeaponItem> weaponItemSort1 = weaponItem.OrderByDescending(x => x.Atk).ToList();
                    WeaponInventorySort(weaponItemSort1);
                    break;
                case 2:
                    //공격력 낮은 순으로 정렬
                    List<WeaponItem> weaponItemSort2 = weaponItem.OrderBy(x => x.Atk).ToList();
                    WeaponInventorySort(weaponItemSort2);
                    break;
            }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
        }

        //방어구 인벤토리 - 방어구 장착 및 해제
        public void ArmorInventory()
        {
            DisplayArmorInventory();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            for (int i = 0; i < armorItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armorItem[i].Name} 장착/해제");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, armorItem.Count);
            if (input == 0)
            {
                //인벤토리 메인
                DispayInventoryMain();
            }
            else
            {
                if ((armorItem[input - 1].Level <= Program.player1.Level) && (Program.player1.JobType == armorItem[input - 1].JobType))
                {
                    if (armorItem[input - 1].IsEquip == false)
                    {
                        armorItem[input - 1].IsEquip = true;
                        if (equipArmorItem.Count == 0)
                        {
                            equipArmorItem.Add(armorItem[input - 1]);
                        }
                        if (equipArmorItem[0] != armorItem[input - 1])
                        {
                            if (equipArmorItem[0].IsEquip)
                            {
                                Program.player1.Defence -= equipArmorItem[0].Def;
                                Program.player1.MaxHp -= equipArmorItem[0].MaxHp;
                            }
                            equipArmorItem[0].IsEquip = false;
                            equipArmorItem.RemoveAt(0);
                            equipArmorItem.Add(armorItem[input - 1]);
                        }
                        Program.player1.Defence += equipArmorItem[0].Def;
                        Program.player1.MaxHp += equipArmorItem[0].MaxHp;
                    }
                    else
                    {
                        equipArmorItem[0].IsEquip = false;
                        armorItem[input - 1].IsEquip = false;

                        Program.player1.Defence -= equipArmorItem[0].Def;
                        Program.player1.MaxHp -= equipArmorItem[0].MaxHp;
                        equipArmorItem.RemoveAt(0);
                    }
                }
                else if (Program.player1.JobType != armorItem[input - 1].JobType)
                {
                    Console.WriteLine($"직업이 맞지 않습니다.");
                }
                else if (armorItem[input - 1].Level >= Program.player1.Level)
                {
                    Console.WriteLine($"레벨이 낮습니다.");
                }
                Thread.Sleep(1000);
                ArmorInventory();
            }
        }

        //방어구 정렬
        public void ArmorInventorySort(List<ArmorItem> armorItem)
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
            Console.WriteLine();
            Console.WriteLine("1. 공격력 높은 순으로 정렬");
            Console.WriteLine("2. 공격력 낮은 순으로 정렬");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    //나가기
                    DispayInventoryMain();
                    break;
                case 1:
                    //방어력 높은 순으로 정렬
                    List<ArmorItem> armorItemSort1 = armorItem.OrderByDescending(x => x.Def).ToList();
                    ArmorInventorySort(armorItemSort1);
                    break;
                case 2:
                    //방어력 낮은 순으로 정렬
                    List<ArmorItem> armorItemSort2 = armorItem.OrderBy(x => x.Def).ToList();
                    ArmorInventorySort(armorItemSort2);
                    break;
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
            //potionItem 2개 이상 일때 count--;, potionItem이 1개 일때 Remove
            //if (potion.Count == 0) // 포션이 1개이면 Remove
            //{
            //    //Remove
            //    Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
            //    Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
            //    potionItem.Remove(potion);
            //}
            if (potion.Count >= 1)//포션이 1개 이상이면 Count--;
            {
                if (potion.Heal != 0) //Hp를 회복해주면 ~
                {
                    Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
                    Console.WriteLine($"{potion.Name}을  사용했습니다.");
                    Console.WriteLine($"현재 체력 : {Program.player1.Hp} / {Program.player1.MaxHp}");
                    Thread.Sleep(1000);
                }
                else if (potion.Mp != 0) //Mp를 회복해주면 ~
                {
                    Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
                    Console.WriteLine($"{potion.Name}을  사용했습니다.");
                    Console.WriteLine($"현재 Mp : {Program.player1.Mp} / {Program.player1.MaxMp}");
                }
                potion.Count--;
                if (potion.Count == 0 )
                {
                    potionItem.Remove(potion);
                }
            }


            //if (potionItem.Count == 0)
            //{
            //    Console.WriteLine($"{potion.Name}이 없습니다.");
            //    Thread.Sleep (1000);
            //}
            //else if (potion.Count >= 1)
            //{
            //    Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
            //    Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
            //    potion.Count--;
            //    if (potionItem.Count == 0)
            //    {
            //        potionItem.Remove(potion);
            //    }
            //}
        }

        //Dungeon.cs - 172
        //Console.WriteLine($"1. Hp 포션 {player.Inventory.potionItem[0].Count}개");
        //Console.WriteLine($"2. Mp 포션 {player.Inventory.potionItem[1].Count}개");
        //Console.Write(">>");
        //int potion = GameManager.Instance.CheckValidInput(1, 2);
        //변경 사항
        //for (int i = 0; i < player.Inventory.potionItem.Count; i++)
        //{
        //    Console.WriteLine($"{i + 1}. {player.Inventory.potionItem[i].Name} {player.Inventory.potionItem[i].Count}");
        //}
        //Console.Write(">>");
        //int potion = GameManager.Instance.CheckValidInput(1, player.Inventory.potionItem.Count);

        //for (int i = 0; i<player.Inventory.potionItem.Count; i++)
        //{
        //    Console.WriteLine($"{i + 1}. {player.Inventory.potionItem[i].Name} {player.Inventory.potionItem[i].Count}개");
        //}
        //Console.Write(">>");
        //int potion = GameManager.Instance.CheckValidInput(1, player.Inventory.potionItem.Count);
        //if (potion == 0)
        //{
        //    //back
        //}
        //else
        //{
        //    player.Inventory.EatPotion(player.Inventory.potionItem[potion - 1]);
        //}


//public void EatPotion1(List<PotionItem> potions, PotionItem potion)
//{
//    //potionItem 2개 이상 일때 count--;, potionItem이 1개 일때 Remove
//    if (potions.Count != 0) // 포션이 1개이면 Remove
//    {
//        //Remove
//        Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
//        Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
//        potionItem.Remove(potion);
//    }
//    else if (potions.Count >= 1)//포션이 2개 이상이면 Count--;
//    {
//        Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
//        Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
//        potion.Count--;
//    }
//}

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

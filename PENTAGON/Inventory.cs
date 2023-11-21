using ConsoleTables;
using EnumsNamespace;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


        //InventorySetting
        //weapon
        //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
        //armor
        //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
        //potion
        //이름, 힐, MP, 효과, 설명, 골드
        public void ItemSetting()
        {
            //static string GetJobString(JobType jobType)
            //{
            //    Dictionary<JobType, string> jobTypeToString = new Dictionary<JobType, string>
            //    {
            //        { JobType.JT_Warrior, "전사" },
            //        { JobType.JT_Mage, "마법사" },
            //        { JobType.JT_Thief, "도적" },
            //        { JobType.JT_Archer, "궁수" }
            //    };

            //    // Dictionary에서 해당하는 문자열을 찾아 반환
            //    if (jobTypeToString.TryGetValue(jobType, out string jobString))
            //    {
            //        return jobString;
            //    }
            //    else
            //    {
            //        // 지정되지 않은 직업이라면 기본값 반환
            //        return "알 수 없는 직업";
            //    }
            //}
            switch (Program.player1.JobType)
            {
                case JobType.JT_Warrior:
                    WeaponItem oldSword = new WeaponItem("낡은 검", 0, JobType.JT_Warrior, 1, "공격력 +1", "빛을 잃은 검입니다.", 100, false);
                    weaponItem.Add(oldSword);

                    ArmorItem ironArmor = new ArmorItem("무쇠 갑옷", 0, JobType.JT_Warrior, 2, 0, "방어력 +2", "추위를 겨우 막아내는 갑옷입니다.", 100, false);
                    armorItem.Add(ironArmor);
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
                    WeaponInventorySort();
                    break;
                case 3:
                    //2. 방어구 인벤토리
                    ArmorInventory();
                    break;
                case 4:
                    //방어구 정렬
                    ArmorInventorySort();
                    break;
                case 5:
                    //3. 기타 인벤토리(물약)
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

                //if (weaponItem[i].Name.Contains("[E]"))
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
                    //if (player._equipmentWeaponArray == null)
                    if (weaponItem[input - 1].IsEquip == false)
                    {
                        //Item에서 구현 ㄱㄱ
                        weaponItem[input - 1].IsEquip = true;
                        //_equipmentWeaponArray.Add(weaponItem[input - 1]);
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.AttackDamage += weaponItem[input - 1].Atk;
                    }
                    else
                    {
                        //해제 IsEquip = false;
                        //장착 IsEquip = true;
                        //플레이어 += weapon.atk;
                        //플레이어 += weapon.def;
                        //플레이어 += weapon.hp;
                        //if (player._equipmentWeaponArray != null)
                        //{
                        //    weaponItem.Add(player._equipmentWeaponArray);
                        //}
                        weaponItem[input - 1].IsEquip = false;
                        Program.player1.AttackDamage -= weaponItem[input - 1].Atk;
                    }
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
        //ArmorInventory 화면 출력
        public void DisplayArmorInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리/방어구");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            table.Options.EnableCount = false;

            for (int i = 0; i < armorItem.Count; i++)
            {
                //if (armorItem[i].Name.Contains("[E]"))
                if (armorItem[i].IsEquip == true)
                {
                    table.AddRow($"[E] {armorItem[i].Name} ", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
                }
                else
                {
                    table.AddRow($"{armorItem[i].Name} ", $"{armorItem[i].Effect}", $"{armorItem[i].Explanation}");
                }
            }
            table.Write();
        }

        //인벤토리 정렬
        public void WeaponInventorySort()
        {
            DisplayWeaponInventory();
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
                    List<WeaponItem> weaponItemSort = weaponItem.OrderBy(x => x.Atk).Reverse().ToList();
                    WeaponInventorySort();
                    break;
                case 2:
                    //공격력 낮은 순으로 정렬
                    List<WeaponItem> weaponItemSort2 = weaponItem.OrderBy(x => x.Atk).ToList();
                    WeaponInventorySort();
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
                //InveroyMain
                DispayInventoryMain();
            }
            else
            {
                //장착/해제 구현
                //일단 armorItem중 장착된 armorItem이 있는지 확인
                //if (armorItem[input - 1].IsEquip == false)

                if ((armorItem[input - 1].Level <= Program.player1.Level) && (Program.player1.JobType == armorItem[input - 1].JobType))
                {
                    if (armorItem[input - 1].IsEquip == false)
                    {
                        armorItem[input - 1].IsEquip = true;
                        //_equipmentArmorArray.Add(armorItem[input - 1]);

                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Defence += armorItem[input - 1].Def;
                        Program.player1.MaxHp += armorItem[input - 1].MaxHp;
                    }
                    else
                    {

                        //if (player._equipmentWeaponArray != null)
                        //{
                        //    weaponItem.Add(player._equipmentWeaponArray);
                        //}
                        armorItem[input - 1].IsEquip = false;
                        //player._equipmentWeaponArray.Add(weaponItem[input - 1]);
                        Program.player1.Defence -= armorItem[input - 1].Def;
                        Program.player1.MaxHp -= armorItem[input - 1].MaxHp;
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
        public void ArmorInventorySort()
        {
            DisplayArmorInventory();

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
                    List<ArmorItem> armorItemSort = armorItem.OrderBy(x => x.Atk).Reverse().ToList();
                    ArmorInventorySort();
                    break;
                case 2:
                    //방어력 낮은 순으로 정렬
                    List<ArmorItem> armorItemSort2 = armorItem.OrderBy(x => x.Atk).ToList();
                    ArmorInventorySort();
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

            //포션의 개수를 표기추가하자
            for (int i = 0; i < potionItem.Count; i++)
            {
                table.AddRow($"{potionItem[i].Name} x{potionItem[i].Count} ", $"{potionItem[i].Effect}", $"{potionItem[i].Explanation}");
            }
            table.Write();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            for (int i = 0; i < potionItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potionItem[i].Name} 먹기");
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
                ETCInventory();
            }
        }

        //포션 먹기
        public void EatPotion(PotionItem potion)
        {
            //potionItem 2개 이상 일때 count--;, potionItem이 1개 일때 Remove
            if (potion.Count == 1) // 포션이 1개이면 Remove
            {
                //Remove
                Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
                Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
                potionItem.Remove(potion);

                //Console.WriteLine($"HP: {Program.player1.Hp}/{Program.player1.MaxHp}");
                //Console.WriteLine($"MP: {Program.player1.Mp}/{Program.player1.MaxMp}");
                //HP: Hp/MaxHp
                //MP: Mp/MaxMp
            }
            else if (potion.Count >= 2)//포션이 2개 이상이면 Count--;
            {
                Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
                Program.player1.Mp = Math.Min(Program.player1.Mp + potion.Mp, Program.player1.MaxMp);
                potion.Count--;

                //Console.WriteLine($"HP: {Program.player1.Hp}/{Program.player1.MaxHp}");
                //Console.WriteLine($"MP: {Program.player1.Mp}/{Program.player1.MaxMp}");

            }
        }

        //포션 획득 - 공사중..
        //public void GetPotion(List<PotionItem> potionItem, PotionItem potion)
        //{
        //    if (potion == null) //Potion이 없으면 .Add
        //    {
        //        potionItem.Add(potion);
        //    }
        //    else if (potion != null) //Potion이 있으면 Count++;
        //    {
        //        potion.Count++;
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

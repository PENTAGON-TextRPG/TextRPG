using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Inventory
    {
        List<Item> itemList = new List<Item>();
        List<Item> inventory = new List<Item>();
        List<Item> weaponItem = new List<Item>();
        List<Item> armorItem = new List<Item>();
        //List<Item> potionItem = new List<Item>();
        List<PotionItem> potionItem = new List<PotionItem>();

        public void ItemSetting()
        {
            Item ironArmor = new Item("무쇠 갑옷", 0, 0, 5, "흔히 볼 수 있는 갑옷입니다.", Job.Null, false);
            armorItem.Add(ironArmor);

            Item oldSword = new Item("낡은 검", 0, 5, 0, "흔히 볼 수 있는 검입니다.", Job.Null, false);
            weaponItem.Add(oldSword);

            PotionItem potion = new PotionItem("물약", 50, "물약을 먹으면 HP가 회복됩니다.");
            potionItem.Add(potion);
        }

        public void InventoryMain()
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
        }
        //인벤토리 화면..
        public void DisplayInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            var table = new ConsoleTable("이름", "능력치", "설명");
            for (int i = 0; i < inventory.Count; i++)
            {
                //if (inventory[i].Equip == true)
                if (inventory[i].Name.Contains("[E]"))
                {
                    table.AddRow($"{inventory[i].Name} ", $"공격력:{inventory[i].Atk} 방어력:{inventory[i].Atk}", $"{inventory[i].Explanation}");
                }
                else
                {
                    table.AddRow($"[E] {inventory[i].Name} ", $"공격력:{inventory[i].Atk} 방어력:{inventory[i].Atk}", $"{inventory[i].Explanation}");
                }
            }
            table.Write();
            //Console.WriteLine();
            //Console.WriteLine("1. 무기 인벤토리");
            //Console.WriteLine("2. 방어구 인벤토리");
            //Console.WriteLine("3. 포션 인벤토리");
            //Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    //나가기
                    break;
                case 1:
                    //1. 아이템 장착 -> 무기, 방어구, 물약을 따로 테이블 생성
                    InventoryItemEquip();
                    break;
                case 2:
                    //아이템 정렬 -> 무기, 방어구, 물약을 따로 테이블 생성
                    InventorySort();
                    break;
                case 3:
                    //아이템 정렬
                    InventorySort();
                    break;
                case 4:
                    //아이템 정렬
                    InventorySort();
                    break;
            }
        }

        public void InventoryInfo()
        {
            DisplayInventory();

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    //나가기
                    break;
                case 1:
                    //1. 인벤토리 아이템 장착
                    InventoryItemEquip();
                    break;
                case 2:
                    //2. 인벤토리 정렬
                    InventorySort();
                    break;
            }
        }

        //인벤토리 아이템 창작
        public void InventoryItemEquip()
        {
            DisplayInventory();

            int input = CheckValidInput(0, inventory.Count);
            if (input == 0)
            {
                //나가기
            }
            else
            {

            }
        }


        //인벤토리 정렬
        public void InventorySort()
        {
            DisplayInventory();

            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    //나가기
                    break;
                case 1:
                    //공격력 높은 순으로 정렬
                    break;
                case 2:
                    //방어력 높은 순으로 정렬
                    break;
                case 3:
                    //포션을 상단으로 정렬
                    break;
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




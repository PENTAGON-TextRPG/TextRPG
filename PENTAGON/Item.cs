using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EnumsNamespace;

namespace PENTAGON
{
    
    //아이템 - 이름, 레벨, 공격력, 방어력, 체력, 골드, 설명, 직업
    public class Item
    {
        public string Name { get; }
        public int Level { get; }
        public JobType JobType { get; }
        public int Atk { get; }
        public int Def { get; }
        public int MaxHp { get; }
        public string Effect { get; }
        public string Explanation { get; }

        public int Gold { get; }



        public Item(string name, int level, JobType job, int atk, int def, int maxhp, string effect, string explanation, int gold)
        {
            Name = name;
            Level = level;
            JobType = job;
            Effect = effect;
            Atk = atk;
            Def = def;
            MaxHp = maxhp;
            Explanation = explanation;
            Gold = gold;
        }
    }

    public class EquipItem : Item
    {
        public bool IsEquip { get; set; }

        public EquipItem(string name, int level, JobType job, int atk, int def, int maxhp, string effect, string explanation, int gold, bool isEquip)
        : base(name, level, job, atk, def, maxhp, effect, explanation, gold)
        {
            IsEquip = isEquip;
        }
        //아이템이 작착이 되었는지?

        //같은 종류의 아이템이면 교체
        //public static void Equip1()
        //{
        //    //장착 IsEquip = true;
        //    //플레이어 += weapon.atk;
        //    //플레이어 += weapon.def;
        //    //플레이어 += weapon.hp;
        //    //Inventory.List<Item>weaponItem[input - 1].IsEquip = true;
        //    ////player._equipmentWeaponArray.Add(weaponItem[input - 1]);
        //    //player.Damage += weaponItem[input - 1].Atk;
        //    //player.Defence += weaponItem[input - 1].Def;
        //    //player.MaxHp += weaponItem[input - 1].Hp;
        //    ////player.MaxMp += weaponItem[input - 1].Mp;
        //}
    }
    //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
    public class WeaponItem : EquipItem
    {
        public WeaponItem(string name, int level, JobType job, int atk, string effect, string explanation, int gold, bool isEquip)
        : base(name, level, job, atk, 0, 0, effect, explanation, gold, isEquip)
        {

        }
    }


    //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
    public class ArmorItem : EquipItem
    {
        public ArmorItem(string name, int level, JobType job, int def, int maxhp, string effect, string explanation, int gold, bool isEquip)
            : base(name, level, job, 0, def, maxhp, effect, explanation, gold, isEquip)
        {

        }
    }
    //이름, 힐, MP, 개수, 효과, 설명, 골드
    public class PotionItem : Item
    {
        public int Heal { get; }
        public int MP { get; }
        public int Count { get; set; }
        public PotionItem(string name, int heal, int mp, int count, string effect, string explanation, int gold)
        : base(name, 0, 0, 0, 0, 0, effect, explanation, gold)
        {
            Heal = heal;
            MP = mp;
            Count = count;
        }

        // 물약 먹기
        public void EatPosion(PotionItem potion)
        {
            //포션을 먹었을 때 Hp를 증가시키지만 MaxHp를 넘지 않도록 함
            Program.player1.Hp = Math.Min(Program.player1.Hp + potion.Heal, Program.player1.MaxHp);
            Count--;
            //Console.WriteLine("eating potion");
            //Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");
        }
    }
}
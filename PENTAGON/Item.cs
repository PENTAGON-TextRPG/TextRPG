using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EnumsNamespace;

namespace PENTAGON
{
    //public enum Job
    //{
    //    Null,
    //    Warrior,
    //    Mage,
    //    Archer,
    //    Thief
    //}
    //public enum Type
    //{
    //    Null,
    //    Weapon,
    //    Armor
    //}
    //아이템 - 이름, 레벨
    public class Item
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }
        public string Explanation { get; }
        public JobType JobType {  get; }

        public Item(string name, int level, int atk, int def, int hp, int gold, string explanation, JobType job)
        {
            Name = name;
            Level = level;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            Explanation = explanation;
            JobType = job;
        }
    }

    public class EquipItem : Item
    {
        public bool IsEquip { get; set; }

        public EquipItem(string name, int level, int atk, int def, int hp, int gold, string explanation, JobType job, bool isEquip)
        : base(name, level, atk, def, hp, gold, explanation, job)
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

        //같은 종류의 아이템이면 교체


    

    public class WeaponItem : EquipItem
    {
        public WeaponItem(string name, int level, int atk, int def, int hp, int gold, string explanation, JobType job, bool isEquip)
        : base(name, level, atk, def, hp, gold, explanation, job, isEquip)
        {

        }

    }

    public class ArmorItem : EquipItem
    {
        public ArmorItem(string name, int level, int atk, int def, int hp, int gold, string explanation, JobType job, bool isEquip) 
            : base(name, level, atk, def, hp, gold, explanation, job, isEquip)
        {

        }
    }

    public class PotionItem : Item
    {
        public int Heal { get; }
        public PotionItem(string name, int gold, string explanation, int heal)
        : base(name, 0, 0, 0, 0, gold, explanation, 0)
        {
            Heal = heal;
        }

        // 물약 먹기
        public void EatPosion(PotionItem potion)
        {
            //포션을 먹었을 때 Hp를 증가시키지만 MaxHp를 넘지 않도록 함
            //player.Hp = Math.Min(player.Hp + potion.Heal, player.MaxHp);
            //Console.WriteLine("eating potion");
            //Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");
        }
    }
    //public class PotionItem
    //{
    //    Player player;
    //    public string Name { get; }
    //    public int Heal { get; }
    //    public int Gold {  get; }
    //    public string Explanation { get; }
    //    public PotionItem(string name, int heal, int gold, string explanation)
    //    {
    //        Name = name;
    //        Heal = heal;
    //        Gold = gold;
    //        Explanation = explanation;
    //    }

    //    public void EatPotion()
    //    {
    //        //포션을 먹었을 때 Hp를 증가시키지만 MaxHp를 넘지 않도록 함
    //        player.Hp = Math.Min(player.Hp + Heal, player.MaxHp);
    //        Console.WriteLine("eating potion");
    //        Console.WriteLine($"HP: {player.Hp}/{player.MaxHp}");
    //    }
    //}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public enum Job
    {
        Null,
        Warrior,
        Mage,
        Archer,
        Thief
    }
    public enum Type
    {
        Null,
        Weapon,
        Armor
    }
    //아이템 - 이름, 레벨
    public class Item
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public string Explanation { get; }
        public Job Job { get; }
        public bool IsEquip { get; set; }

        public Item(string name, int level, int atk, int def, string explanation, Job job, bool isEquip)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Def = def;
            Explanation = explanation;
            Job = job;
            IsEquip = isEquip;
        }
    }

    public class EquipItem : Item
    {
        public EquipItem(string name, int level, int atk, int def, string explanation, Job job, bool isEquip)
        : base(name, level, atk, def, explanation, job, isEquip)
        {

        }
        //아이템이 작착이 되었는지?

        //같은 종류의 아이템이면 교체

    }

    public class WeaponItem : EquipItem
    {
        public WeaponItem(string name, int level, int atk, int def, string explanation, Job job, bool isEquip)
        : base(name, level, atk, def, explanation, job, isEquip)
        {

        }
    }

    public class ArmorItem : EquipItem
    {
        public ArmorItem(string name, int level, int atk, int def, string explanation, Job job, bool isEquip)
        : base(name, level, atk, def, explanation, job, isEquip)
        {

        }
    }

    //public class PotionItem : Item
    //{
    //    public int Heel { get; }
    //    public PotionItem(string name, int level, int atk, int def, string explanation, Job job, bool isEquip, int heel )
    //    : base(name, level, atk, def, explanation, job, isEquip)
    //    {
    //        Heel = heel;
    //    }

    //    // 물약 먹기
    //    public void EatPosion()
    //    {

    //    }
    //}
    public class PotionItem
    {
        public string Name { get; }
        public int Heel { get; }
        public string Explanation { get; }
        public PotionItem(string name, int heel, string explanation)
        {
            Name = name;
            Heel = heel;
            Explanation = explanation;
        }

        // 물약 먹기
        public void EatPosion()
        {
            //포션을 먹으면 플레이어.Hp += Posion.Heel
        }
    }
}


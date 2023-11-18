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
        public Job Job { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public string Effect { get; }
        public string Explanation { get; }
        public int Price { get; }
        public bool IsEquip { get; set; }

        public Item(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
        {
            Name = name;
            Level = level;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Effect = effect;
            Explanation = explanation;
            Price = price;
            IsEquip = isEquip;
        }
    }

    public class EquipItem : Item
    {

        public EquipItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
        : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
        { }
            //아이템이 장착이 되었는지?

            //같은 종류의 아이템이면 교체
          public class WeaponItem : EquipItem
        {

            public WeaponItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
            : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
            {
            }
        }

        public class ArmorItem : EquipItem
        {

            public ArmorItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
            : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
            {
            }
        }
    }

    public class PotionItem 
         {
        public PotionItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
         }
        
    }
}

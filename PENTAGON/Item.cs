using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
<<<<<<< Updated upstream
    public class Item
    {

=======
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
>>>>>>> Stashed changes
    }

    public class EquipItem : Item
    {
<<<<<<< Updated upstream
=======
        public EquipItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
        : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
        {

        }
        //아이템이 작착이 되었는지?

        //같은 종류의 아이템이면 교체
>>>>>>> Stashed changes

    }

    public class WeaponItem : EquipItem
    {
<<<<<<< Updated upstream
=======
        public WeaponItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
        : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
        {
>>>>>>> Stashed changes

    }

    public class ArmorItem : EquipItem
    {
<<<<<<< Updated upstream
=======
        public ArmorItem(string name, int level, Job job, int atk, int def, int hp, string effect, string explanation, int price, bool isEquip)
        : base(name, level, job, atk, def, hp, effect, explanation, price, isEquip)
        {
>>>>>>> Stashed changes

    }

    public class PotionItem : Item
    {

    }
}

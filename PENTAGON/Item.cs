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
        public string Name { get; set; }
        public int Level { get; }
        public int Force { get; set; }
        public JobType JobType { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int MaxHp { get; set; }
        public string Effect { get; }
        public string Explanation { get; }
        public int Gold { get; set; }

        public Item(string name, int level, int force, JobType job, int atk, int def, int maxhp, string effect, string explanation, int gold)
        {
            Name = name;
            Level = level;
            Force = force;
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

        public EquipItem(string name, int level, int force, JobType job, int atk, int def, int maxhp, string effect, string explanation, int gold, bool isEquip)
        : base(name, level, force, job, atk, def, maxhp, effect, explanation, gold)
        {
            IsEquip = isEquip;
        }
    }
    //이름, 레벨, 직업, 공격력, 효과, 설명, 골드, 장착유무
    public class WeaponItem : EquipItem
    {
        public WeaponItem(string name, int level, int force, JobType job, int atk, string effect, string explanation, int gold, bool isEquip)
        : base(name, level, force, job, atk, 0, 0, effect, explanation, gold, isEquip)
        {

        }
    }

    //이름, 레벨, 직업, 방어력, 체력, 효과, 설명, 골드, 장착유무
    public class ArmorItem : EquipItem
    {
        public ArmorItem(string name, int level, int force, JobType job, int def, int maxhp, string effect, string explanation, int gold, bool isEquip)
            : base(name, level, force, job, 0, def, maxhp, effect, explanation, gold, isEquip)
        {

        }
    }

    //이름, 힐, MP, 개수, 효과, 설명, 골드
    public class PotionItem : Item
    {
        public int Heal { get; }
        public int Mp { get; }
        public int Count { get; set; }
        public PotionItem(string name, int heal, int mp, int count, string effect, string explanation, int gold)
        : base(name, 0, 0, 0, 0, 0, 0, effect, explanation, gold)
        {
            Heal = heal;
            Mp = mp;
            Count = count;
        }
    }
}
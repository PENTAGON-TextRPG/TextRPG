using EnumsNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace PENTAGON
{
    public abstract class Monster : Character
    {
        public List<Monster> GetMonsterOfStage(StageType stageType)
        {

        }
    }
    //***************************
    //          Stage1           
    //***************************
    public class Slime : Monster
    {
        public Slime()
        {
            Hp = 10;
            MaxHp = 10;
            Damage = 3;
            Defence = 1;
            Exp = 1;
            Gold = 50;
        }

        public override bool IsDie()
        {
            if (Hp == 0)
            {
                return true;
            }

            return false;
        }

        public override bool ReceiveDamage(int damage, DamageType damageType)
        {
            bool isReceiveDamage = true;

            if(damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = _random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        public override void Attack(Character target)
        {
            //15퍼 확률로 크리티컬...
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Damage / 10.0f));

            int minDamage = Damage - damageErrorRange;
            int maxDamage = Damage + damageErrorRange;

            int randomDamage = _random.Next(minDamage, maxDamage +1);
            //sdkjfhnsdkl
            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }

        private void ApplyDamage(int damage)
        {
            if (damage <= Defence) damage = 1;
            else damage -= Defence;

            Hp -= damage;

            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        private Random _random = new Random();
    }

    //public class RatRider : Monster
    //{
    //    public RatRider()
    //    {
    //        Hp = 15;
    //        MaxHp = 15;
    //        Damage = 4;
    //        Defence = 2;
    //        Exp = 2;
    //        Gold = 75;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    //public class Goblin : Monster
    //{
    //    public Goblin()
    //    {
    //        Hp = 20;
    //        MaxHp = 20;
    //        Damage = 5;
    //        Defence = 3;
    //        Exp = 3;
    //        Gold = 100;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    //public class Skeleton : Monster
    //{
    //    public Skeleton()
    //    {
    //        Hp = 25;
    //        MaxHp = 25;
    //        Damage = 6;
    //        Defence = 4;
    //        Exp = 4;
    //        Gold = 125;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    ////***************************
    ////          Stage2           
    ////***************************
    //public class Golem : Monster
    //{
    //    public Golem()
    //    {
    //        Hp = 35;
    //        MaxHp = 35;
    //        Damage = 8;
    //        Defence = 6;
    //        Exp = 6;
    //        Gold = 175;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Crocodile : Monster
    //{
    //    public Crocodile()
    //    {
    //        Hp = 45;
    //        MaxHp = 45;
    //        Damage = 10;
    //        Defence = 7;
    //        Exp = 7;
    //        Gold = 225;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Orc : Monster
    //{
    //    public Orc()
    //    {
    //        Hp = 55;
    //        MaxHp = 55;
    //        Damage = 12;
    //        Defence = 8;
    //        Exp = 8;
    //        Gold = 275;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Sorcerer : Monster
    //{
    //    public Sorcerer()
    //    {
    //        Hp = 65;
    //        MaxHp = 65;
    //        Damage = 14;
    //        Defence = 9;
    //        Exp = 9;
    //        Gold = 325;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    ////***************************
    ////          Stage3           
    ////***************************
    //public class FlameElemental : Monster
    //{
    //    public FlameElemental()
    //    {
    //        Hp = 80;
    //        MaxHp = 80;
    //        Damage = 17;
    //        Defence = 11;
    //        Exp = 11;
    //        Gold = 400;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Minotaur : Monster
    //{
    //    public Minotaur()
    //    {
    //        Hp = 95;
    //        MaxHp = 95;
    //        Damage = 20;
    //        Defence = 13;
    //        Exp = 13;
    //        Gold = 475;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class DarkKnight : Monster
    //{
    //    public DarkKnight()
    //    {
    //        Hp = 110;
    //        MaxHp = 110;
    //        Damage = 23;
    //        Defence = 15;
    //        Exp = 15;
    //        Gold = 550;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    //public class Dragon : Monster
    //{
    //    public Dragon()
    //    {
    //        Hp = 125;
    //        MaxHp = 125;
    //        Damage = 26;
    //        Defence = 17;
    //        Exp = 17;
    //        Gold = 625;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}

    ////***************************
    ////          Stage4           
    ////***************************
    //public class Phoenix : Monster
    //{
    //    public Phoenix()
    //    {
    //        Hp = 145;
    //        MaxHp = 145;
    //        Damage = 30;
    //        Defence = 20;
    //        Exp = 20;
    //        Gold = 725;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class GiantBear : Monster
    //{
    //    public GiantBear()
    //    {
    //        Hp = 165;
    //        MaxHp = 165;
    //        Damage = 34;
    //        Defence = 23;
    //        Exp = 23;
    //        Gold = 825;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class MysticalWizard : Monster
    //{
    //    public MysticalWizard()
    //    {
    //        Hp = 185;
    //        MaxHp = 185;
    //        Damage = 38;
    //        Defence = 26;
    //        Exp = 26;
    //        Gold = 925;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Demon : Monster
    //{
    //    public Demon()
    //    {
    //        Hp = 205;
    //        MaxHp = 205;
    //        Damage = 42;
    //        Defence = 29;
    //        Exp = 29;
    //        Gold = 1025;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    ////***************************
    ////          Stage5           // 30 6 4 4 150
    ////***************************
    //public class Queen : Monster
    //{
    //    public Queen()
    //    {
    //        Hp = 235;
    //        MaxHp = 235;
    //        Damage = 48;
    //        Defence = 33;
    //        Exp = 33;
    //        Gold = 1175;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class VerdantShadow : Monster
    //{
    //    public VerdantShadow()
    //    {
    //        Hp = 265;
    //        MaxHp = 265;
    //        Damage = 54;
    //        Defence = 37;
    //        Exp = 37;
    //        Gold = 1325;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class InfernoOverlord : Monster
    //{
    //    public InfernoOverlord()
    //    {
    //        Hp = 295;
    //        MaxHp = 295;
    //        Damage = 60;
    //        Defence = 41;
    //        Exp = 41;
    //        Gold = 1475;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
    //public class Wraith : Monster
    //{
    //    public Wraith()
    //    {
    //        Hp = 325;
    //        MaxHp = 325;
    //        Damage = 66;
    //        Defence = 45;
    //        Exp = 45;
    //        Gold = 1625;
    //    }

    //    public override void ReceiveDamage(int damage)
    //    {

    //    }

    //    public override void Attack(Character target, int damage)
    //    {

    //    }
    //}
}

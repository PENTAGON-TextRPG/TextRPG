using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public abstract class Character
    {
        //공격 하는 메서드
        public virtual int Attack(Character target)
        {
            int damage = Damage;
            bool isCritical = false;
            //15퍼 확률로 크리티컬 데미지
            int randomValue = _random.Next(1, 101);
            if (randomValue <= 15) isCritical = true;

            if (isCritical)
            {
                Console.WriteLine("치명타 발동!!");
                damage = Convert.ToInt32(Math.Ceiling(damage * 1.6f));
            }

            int damageErrorRange = Convert.ToInt32(Math.Ceiling(damage / 10.0f));

            int minDamage = damage - damageErrorRange;
            int maxDamage = damage + damageErrorRange;

            int randomDamage = _random.Next(minDamage, maxDamage + 1);

            if (target.ReceiveDamage(randomDamage, DamageType.DT_Normal, target.Defence))
            {
                return ReturnDamage(randomDamage, target.Defence);
            }
            else
            {
                Console.WriteLine("회피했습니다.");
                return 0;
            }
        }
        //죽었는지 아닌지 판별하는 메서드
        public virtual bool IsDie()
        {
            if (Hp > 0)
            {
                return false;
            }

            return true;
        }
        //데미지 받는 메서드
        public virtual bool ReceiveDamage(int damage, DamageType damageType, int targetDefence)
        {
            bool isReceiveDamage = true;

            // 10% 확률로 몬스터의 기본 공격 회피
            if (damageType == DamageType.DT_Normal)
            {
                isReceiveDamage = _random.Next(1, 11) != 1;
            }

            if (isReceiveDamage) ApplyDamage(damage);

            return isReceiveDamage;
        }

        public int ReturnDamage(int damage, int targetDefence)
        {
            if (damage <= targetDefence) damage = 1;
            else damage -= targetDefence;

            return damage;
        }
        

        private int ApplyDamage(int damage)
        {
            if (damage <= Defence) damage = 1;
            else damage -= Defence;

            Hp -= damage;

            if (Hp < 0)
            {
                Hp = 0;
            }

            return damage;
        }


        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public int MaxHp
        {
            get { return _maxHp; }
            set { _maxHp = value; }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold  = value; }
        }

        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _hp;
        private int _maxHp;
        private int _damage;
        private int _defence;
        private int _gold;
        private int _exp;
        public string _name = "";
        private Random _random = new Random();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Character
    {
        //몬스터와, 플레이어의 부모

        //메서드
        //데미지 받는 메서드
        //공격 하는 메서드


        //필드
        //hp, maxHp, attack, defence, gold, exp,name
        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public int MaxHp
        {
            get { return _maxHp; }
        }

        public int Attack
        {
            get { return _attack; }
        }

        public int Defence
        {
            get { return _defence; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold  = value; }
        }

        public int Exp
        {
            get { return _exp; }
        }

        public string Name
        {
            get { return _name; }
        }

        private int _hp;
        private int _maxHp;
        private int _attack;
        private int _defence;
        private int _gold;
        private int _exp;
        private string _name = "";
    }
}

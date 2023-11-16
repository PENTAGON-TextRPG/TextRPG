using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PENTAGON
{
    public class Player : Character
    {
        // 스킬 관련 필드
        public string _fSkillName;
        public string _sSkillName;
        public int _fSkillMp;
        public int _sSkillMp;
        public string _fSkillInfo;
        public string _sSkillInfo;
        public int _fSkillDamage;
        public float _sSkillDamage;

        public Player(string name)
        {
            _name = name;
        }

        //inventory 생성
        //인벤토리
        public void DisplayInventory()
        {

        }

        //장착 관리
        public void DisplayEquipManage()
        {

        }

        //스킬 사용
        public void UseSkill()
        {
            Console.Clear();
            Console.WriteLine($"1. {_fSkillName} - MP {_fSkillMp}");
            Console.WriteLine($"{_fSkillInfo}");
            Console.WriteLine($"2. {_sSkillName} - MP {_sSkillMp}");
            Console.WriteLine($"{_sSkillInfo}");
            Console.WriteLine("사용할 스킬을 고르세요.");

            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    FirstSkill();
                    break;
                case 2:
                    SecondSkill();
                    break;
            }
        }

        public void FirstSkill()
        {
            // 현재 스테이지의 살아있는 몬스터 랜덤하게 선택
            Random random = new Random();
            int randomMonsterIndex = random.Next(Monster.monsterList.Count);
            Monster selectedMonster = Monster.monsterList[randomMonsterIndex];

            // 플레이어가 몬스터 공격
            int damage = _fSkillDamage;
            selectedMonster.ReceiveDamage(damage);

            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 {_fSkillName}을(를) 사용하여 {damage}의 데미지를 입혔습니다.");

            // 몬스터의 체력 감소
            
            

        }

        public void SecondSkill()
        {

        }

        
        //Exp 획득
        public void GainExp()
        {
            // 플레이어가 전투를 끝마치면 xp 획득
        }


        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int Mp
        {
            get { return _mp; }
            set { _mp = value; }
        }

        public int MaxMp
        {
            get { return _maxMp; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        //public JobType JobType
        //{
        //    get { return _job; }
        //    set { _job = value; }
        //}
        public Item[] EquipmentWeaponArray
        {
            get { return _equipmentWeaponArray; }
            set { }
        }

        private int _level = 1;
        private int _mp;
        private int _maxMp;
        private Inventory _inventory = new Inventory();
        private JobType _job;
        private Item[] _equipmentWeaponArray = new Item[2];
        private Item[] _equipmentArmorArray = new Item[5];

        public enum JobType
        {
            JT_Warrior,
            JT_Mage,
            JT_Thief,
            JT_Archer,
        }
    }



    public class Warrior : Player
    {
        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;

            // Warrior의 스킬 설정
            _fSkillName = "전사 스킬 1";
            _sSkillName = "전사 스킬 2";
            _fSkillMp = 15;
            _sSkillMp = 25;
            _fSkillInfo = "전사의 스킬 1입니다.";
            _sSkillInfo = "전사의 스킬 2입니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;
        }

        private const int _initialAttack = 15;
        private const int _initialDefence = 15;
        private int _hp = 40;
        private int _maxHp = 40;
        private int _mp = 20;
        private int _maxMp = 20;
        private int _attack = 15;
        private int _defence = 15;

        
    }


    public class Mage : Player
    {
        public Mage(string name)
            : base(name)
        {
            JobType = JobType.JT_Mage;
        }

        private const int _initialAttack = 10;
        private const int _initialDefence = 5;
        private int _hp = 20;
        private int _maxHp = 20;
        private int _mp = 50;
        private int _maxMp = 50;
        private int _attack = 10;
        private int _defence = 5;

    }

    public class Thief : Player
    {
        public Thief(string name)
            : base(name)
        {
            JobType = JobType.JT_Thief;
        }

        private const int _initialAttack = 25;
        private const int _initialDefence = 2;
        private int _hp = 30;
        private int _maxHp = 30;
        private int _mp = 30;
        private int _maxMp = 30;
        private int _attack = 25;
        private int _defence = 2;

    }

    public class Archer : Player
    {
        public Archer(string name)
            : base(name)
        {
            JobType = JobType.JT_Archer;
        }

        private const int _initialAttack = 20;
        private const int _initialDefence = 5;
        private int _hp = 30;
        private int _maxHp = 30;
        private int _mp = 20;
        private int _maxMp = 20;
        private int _attack = 20;
        private int _defence = 5;

    }
}

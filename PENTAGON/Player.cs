﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnumsNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace PENTAGON
{
    public abstract class Player : Character
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

        // Attack() 메서드에서 10% 오차를 이용해 계산되는 최종 공격 데미지
        public int randomDamage;
        // 치명타 확률에 대한 상수(15%)
        //public const int CriticalHitChance = 15;

        public int _level = 1;
        public int _mp;
        public int _maxMp;
        public int _attack;
        public int _defence;
        public Inventory _inventory = new Inventory();
        private JobType _job;
        private Item[] _equipmentWeaponArray = new Item[2];
        //private Item[] _equipmentArmorArray = new Item[5];

        // 몬스터 리스트
        //Monster monster;
        //List<Monster> monsters;
        MonsterManager monsterManager = new MonsterManager();
        List<Monster> monsters = new List<Monster>();

        Random random = new Random();
        Dungeon dungeon = new Dungeon();
        //Program program = new Program();

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
            set { _maxMp = value; }
        }
        public int AttackDamage
        {
            get { return _attack; }
            set { _attack = value; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public JobType JobType
        {
            get { return _job; }
            set { _job = value; }
        }
        public Item[] EquipmentWeaponArray
        {
            get { return _equipmentWeaponArray; }
            set { }
        }


        public Player(string name)
        {
            _name = name;
            _mp = 30;
            // 랜덤 데미지 초기화(안 적으면 기본 공격력으로 고정됨)
            randomDamage = 0;

            // 생성자에서 초기화
            //monsters = Monster.GetMonstersOfStage();
        }

        // 몬스터 불러오기
        //private void InitializeMonsters()
        //{
        //    //monster = new Slime();
        //    //monsters = MonsterManager.GetMonstersOfStage();
        //    monsters = Monster.GetMonstersOfStage();
        //}

        public abstract void DisplayMyInfo();

        //inventory 생성
        

        // 번호로 몬스터를 선택하면 기본 공격(평타)
        public void BasicAttack(Monster selectedMonster)
        {
            // 플레이어가 선택한 몬스터 공격
            Attack(selectedMonster);

            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 기본 공격을 사용하여 {Program.player1.randomDamage}의 데미지를 입혔습니다.\n");

            // 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster.IsDie())
            {
                int monsterExp = selectedMonster.Exp;
                int monsterGold = selectedMonster.Gold;
                Console.WriteLine($"{selectedMonster.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp}\n획득한 골드 : {monsterGold}\n");
                Program.player1.Gold += monsterGold;
                GainExp(monsterExp);
                GetPosionItems();
            }
            else // 몬스터가 죽지 않으면 경험치, 골드, 포션 미획득
            {
                Console.WriteLine($"하지만 {selectedMonster.Name}은(는) 살아남았네요 . . .\n");
            }
            Console.WriteLine($"현재 경험치 : {Exp}\n");

            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            //GameManager.Instance.DisplayGameIntro();
            //UseSkill();
        }


        // 스킬 사용
        public bool UseSkill(List<Monster> stageMonsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // 몬스터 정보
            // 스테이지 몬스터, List<Monster> 매개변수로
            for (int i = 0; i < stageMonsters.Count; i++)
            {
                Console.WriteLine($"{stageMonsters[i].Name} Hp {stageMonsters[i].Hp}");
            }

            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Level} {_name} ({Program.player1._job})");
            Console.WriteLine($"HP {Program.player1.Hp}/{Program.player1.MaxHp}");
            Console.WriteLine($"MP {Program.player1.Mp}/{Program.player1.MaxMp}\n");
            Console.WriteLine($"1. {Program.player1._fSkillName} - MP {Program.player1._fSkillMp}");
            Console.WriteLine($"{Program.player1._fSkillInfo}");
            Console.WriteLine($"2. {Program.player1._sSkillName} - MP {Program.player1._sSkillMp}");
            Console.WriteLine($"{Program.player1._sSkillInfo}");
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("원하는 행동을 입력하세요.");
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 2);

            // 스킬 사용을 취소했을 때 몬스터 턴으로 넘어가지 않도록 false 반환
            if (input == 0)
            {
                // 전투 화면으로 돌아가기
                //dungeon.Battle(Program.player1, stage);
                //dungeon.DisplayStage();
                return false;
            }

            // 플레이어의 MP가 선택한 스킬의 소모 MP보다 적은지 확인
            if ((input == 1 && Program.player1.Mp < Program.player1._fSkillMp) || (input == 2 && Program.player1.Mp < Program.player1._sSkillMp))
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                Thread.Sleep(3000);
                return false;
            }
            else
            {
                switch (input)
                {
                    case 1:
                        FirstSkill(stageMonsters);
                        break;
                    case 2:
                        SecondSkill(stageMonsters);
                        break;
                }

                return true;
            }
        }


        public void FirstSkill(List<Monster> stageMonsters)
        {
            // 현재 스테이지의 살아있는 몬스터 선택
            //Random random = new Random();
            int randomMonsterIndex = random.Next(stageMonsters.Count);
            Monster selectedMonster = stageMonsters[randomMonsterIndex];

            // 플레이어가 몬스터 공격
            int damage = Program.player1._fSkillDamage;
            selectedMonster.ReceiveDamage(damage, DamageType.DT_Skill);


            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 {Program.player1._fSkillName}을(를) 사용하여 {damage}의 데미지를 입혔습니다.\n");

            // 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster.IsDie())
            {
                int monsterExp = selectedMonster.Exp;
                int monsterGold = selectedMonster.Gold;
                Console.WriteLine($"{selectedMonster.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp}\n획득한 골드 : {monsterGold}\n");
                Program.player1.Gold += monsterGold;
                GainExp(monsterExp);
                GetPosionItems();
            }
            else // 몬스터가 죽지 않으면 경험치, 골드, 포션 미획득
            {
                Console.WriteLine($"하지만 {selectedMonster.Name}은(는) 살아남았네요 . . .\n");
            }

            Console.WriteLine($"남은 MP : {Program.player1.Mp - Program.player1._fSkillMp}\n");
            Program.player1.Mp -= Program.player1._fSkillMp;
            Console.WriteLine($"현재 경험치 : {Exp}\n");

            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            //GameManager.Instance.DisplayGameIntro();
            //UseSkill();
        }

        public void SecondSkill(List<Monster> stageMonsters)
        {
            // 현재 스테이지의 살아있는 몬스터 중에서 랜덤하게 두 몬스터 선택
            List<int> availableMonster = Enumerable.Range(0, stageMonsters.Count).ToList();
            //Random random = new Random();

            // 첫 번째 몬스터 선택
            int randomMonsterIndex1 = availableMonster[random.Next(availableMonster.Count)];
            availableMonster.Remove(randomMonsterIndex1); // 중복 방지
            Monster selectedMonster1 = stageMonsters[randomMonsterIndex1];

            // 두 번째 몬스터 선택
            int randomMonsterIndex2 = availableMonster[random.Next(availableMonster.Count)];
            Monster selectedMonster2 = stageMonsters[randomMonsterIndex2];

            // 플레이어가 몬스터들에게 공격
            int damage1 = Convert.ToInt32(Program.player1._sSkillDamage);
            int damage2 = Convert.ToInt32(Program.player1._sSkillDamage);

            selectedMonster1.ReceiveDamage(damage1, DamageType.DT_Skill);
            selectedMonster2.ReceiveDamage(damage2, DamageType.DT_Skill);

            Console.Clear();
            Console.WriteLine($"{_name}이(가) {selectedMonster1.Name}와 {selectedMonster2.Name}에게 {Program.player1._sSkillName}을(를) 사용하여 각각 {damage1}의 데미지를 입혔습니다.\n");

            // 각 몬스터를 죽여 경험치, 골드, 포션 획득
            if (selectedMonster1.IsDie())
            {
                int monsterExp1 = selectedMonster1.Exp;
                int monsterGold1 = selectedMonster1.Gold;
                Console.WriteLine($"{selectedMonster1.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp1}\n획득한 골드 : {monsterGold1}\n");
                Program.player1.Gold += monsterGold1;
                GainExp(monsterExp1);
                GetPosionItems();
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster1.Name}은(는) 살아남았네요 . . .\n");
            }

            if (selectedMonster2.IsDie())
            {
                int monsterExp2 = selectedMonster2.Exp;
                int monsterGold2 = selectedMonster2.Gold;
                Console.WriteLine($"{selectedMonster2.Name}을(를) 죽였습니다!\n획득한 경험치 : {monsterExp2}\n획득한 골드 : {monsterGold2}");
                Program.player1.Gold += monsterGold2;
                GainExp(monsterExp2);
                GetPosionItems();
            }
            else
            {
                Console.WriteLine($"하지만 {selectedMonster2.Name}은(는) 살아남았네요 . . .\n");
            }

            Console.WriteLine($"남은 MP : {Program.player1.Mp - Program.player1._sSkillMp}\n");
            Program.player1.Mp -= Program.player1._sSkillMp;
            Thread.Sleep(5000);
            //전투 화면으로 돌아가기
            //GameManager.Instance.DisplayGameIntro();
            //UseSkill();
        }


        // 플레이어가 전투를 마치면 몬스터에게서 서로 다른 경험치 획득
        public void GainExp(int monsterExp)
        {
            Exp += monsterExp;

            // 경험치가 다음 레벨업에 필요한 양보다 많을 경우
            while (Exp >= GetRequiredExpForNextLevel())
            {
                int excessExp = Exp - GetRequiredExpForNextLevel();
                LevelUp();
                Exp = excessExp; // 초과된 경험치를 다음 레벨에 사용
            }
        }

        // 레벨업 메서드
        private void LevelUp()
        {
            Level++;
            Exp = 0; // 레벨업 후 경험치 초기화
            Program.player1.AttackDamage += 1; // 기본 공격력 1 증가
            Program.player1.Defence += 1; // 기본 방어력 1 증가

            Console.WriteLine($"{_name}이(가) Lv.{Level}로 레벨업했습니다!");
        }

        // 다음 레벨까지 필요한 경험치
        private int GetRequiredExpForNextLevel()
        {
            switch (Level)
            {
                case 1:
                    return 10;
                case 2:
                    return 35;
                case 3:
                    return 65;
                case 4:
                    return 100;
                default:
                    return 0;
            }
        }

        public void GetPosionItems()
        {
            // 몬스터 사망 시 10% 확률로 포션을 얻음
            if (random.Next(1, 11) == 1)
            {
                int potionType = random.Next(2); // 0은 HpPotion, 1은 MpPotion
                if (potionType == 0)
                {
                    //HpPotionCount++; // 보유 중인 HpPotion 개수 증가
                    Console.WriteLine($"운 좋게 Hp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Hp포션 개수 : {HpPotionCount}\n");
                }
                else
                {
                    // MpPotionCount++; // 보유 중인 MpPotion 개수 증가
                    Console.WriteLine($"운 좋게 Mp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Mp포션 개수 : {MpPotionCount}\n");
                }
            }
        }

    }



    public class Warrior : Player
    {
        private const int _initialAttack = 15;
        private const int _initialDefence = 15;
        // 치명타 확률에 대한 상수(15%)
        public const int CriticalHitChance = 15;

        public int _hp = 40;
        private int _maxHp = 40;
        public int _mp = 30;
        private int _maxMp = 30;
        public int _attack = 15;
        public int _defence = 15;

        Program program = new Program();
        Random random = new Random();

        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;

            // Warrior의 스킬 설정
            _fSkillName = "전사 스킬 1";
            _sSkillName = "전사 스킬 2";
            _fSkillMp = 10;
            _sSkillMp = 20;
            _fSkillInfo = "전사의 스킬 1입니다.";
            _sSkillInfo = "전사의 스킬 2입니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 15;
            Defence = 15;
            Hp = 40;
            MaxHp = 40;
            Mp = 30;
            MaxMp = 30;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name} ( 전사 )");
            int addAttack = Program.player1.AttackDamage - _initialAttack;
            Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = Program.player1.Defence - _initialDefence;
            Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
            Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }

        // 데미지 받는 메서드


        // 데미지 계산



        // 공격하는 메서드
        public override void Attack(Character target)
        {
            Random random = new Random();

            // 15% 확률로 치명타 여부 확인
            bool isCriticalHit = random.Next(1, 101) <= CriticalHitChance;

            // 10%의 오차 범위 내에서 기본 공격력 계산
            int damageErrorRange = Convert.ToInt32(Math.Ceiling(Program.player1.AttackDamage / 10.0f));

            int minDamage = Program.player1.AttackDamage - damageErrorRange;
            int maxDamage = Program.player1.AttackDamage + damageErrorRange;

            randomDamage = random.Next(minDamage, maxDamage);

            // 치명타인 경우 데미지를 정상 데미지의 160%로 계산
            if (isCriticalHit)
            {
                randomDamage = (int)(randomDamage * 1.6f);
                Console.Clear();
                Console.WriteLine("치명타 발동!!!!!!\n");
                Thread.Sleep(1500);
            }

            target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
        }
    }
}

//public class Mage : Player
//{
//    private const int _initialAttack = 10;
//    private const int _initialDefence = 5;
//    // 치명타 확률에 대한 상수(10%)
//    public const int CriticalHitChance = 10;

//    public int _hp = 20;
//    private int _maxHp = 20;
//    public int _mp = 50;
//    private int _maxMp = 50;
//    public int _attack = 10;
//    public int _defence = 5;

//    public Mage(string name)
//        : base(name)
//    {
//        JobType = JobType.JT_Mage;

//        // Mage의 스킬 설정
//        _fSkillName = "마법사 스킬 1";
//        _sSkillName = "마법사 스킬 2";
//        _fSkillMp = 10;
//        _sSkillMp = 15;
//        _fSkillInfo = "마법사의 스킬 1입니다.";
//        _sSkillInfo = "마법사의 스킬 2입니다.";
//        _fSkillDamage = _attack * 2;
//        _sSkillDamage = _attack * 1.5f;

//        AttackDamage = 10;
//        Defence = 5;
//        Hp = 20;
//        MaxHp = 20;
//        Mp = 50;
//        MaxMp = 50;
//    }

//    public override void DisplayMyInfo()
//    {
//        Console.Clear();

//        Console.WriteLine("상태보기");
//        Console.WriteLine("캐릭터의 정보를 표시합니다.");
//        Console.WriteLine();
//        Console.WriteLine($"Lv.{Level}");
//        Console.WriteLine($"{Name} ( 마법사 )");
//        int addAttack = Program.player1.AttackDamage - _initialAttack;
//        Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
//        int addDefence = Program.player1.Defence - _initialDefence;
//        Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
//        Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
//        Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
//        Console.WriteLine($"Gold : {Gold} G");
//        Console.WriteLine();
//        Console.WriteLine("0. 나가기");
//        Console.WriteLine("원하시는 행동을 입력해주세요.");
//        Console.Write(">>");

//        int input = GameManager.Instance.CheckValidInput(0, 0);
//        switch (input)
//        {
//            case 0:
//                GameManager.Instance.DisplayGameIntro();
//                break;
//        }
//    }

//    // 공격하는 메서드
//    public override void Attack(Character target)
//    {
//        Random random = new Random();

//        // 10% 확률로 치명타 여부 확인
//        bool isCriticalHit = random.Next(1, 101) <= CriticalHitChance;

//        // 10%의 오차 범위 내에서 기본 공격력 계산
//        int damageErrorRange = Convert.ToInt32(Math.Ceiling(Program.player1.AttackDamage / 10.0f));

//        int minDamage = Program.player1.AttackDamage - damageErrorRange;
//        int maxDamage = Program.player1.AttackDamage + damageErrorRange;

//        randomDamage = random.Next(minDamage, maxDamage);

//        // 치명타인 경우 데미지를 정상 데미지의 160%로 계산
//        if (isCriticalHit)
//        {
//            randomDamage = (int)(randomDamage * 1.6f);
//            Console.Clear();
//            Console.WriteLine("치명타 발동!!!!!!\n");
//            Thread.Sleep(1500);
//        }

//        target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
//    }



//public class Thief : Player
//{
//    private const int _initialAttack = 20;
//    private const int _initialDefence = 2;
//    // 치명타 확률에 대한 상수(15%)
//    public const int CriticalHitChance = 15;
//    public int _hp = 30;
//    private int _maxHp = 30;
//    public int _mp = 30;
//    private int _maxMp = 30;
//    public int _attack = 20;
//    public int _defence = 2;

//    public Thief(string name)
//        : base(name)
//    {
//        JobType = JobType.JT_Thief;

//        // Thief의 스킬 설정
//        _fSkillName = "도적 스킬 1";
//        _sSkillName = "도적 스킬 2";
//        _fSkillMp = 10;
//        _sSkillMp = 20;
//        _fSkillInfo = "도적의 스킬 1입니다.";
//        _sSkillInfo = "도적의 스킬 2입니다.";
//        _fSkillDamage = _attack * 2;
//        _sSkillDamage = _attack * 1.5f;

//        AttackDamage = 20;
//        Defence = 2;
//        Hp = 30;
//        MaxHp = 30;
//        Mp = 30;
//        MaxMp = 30;
//    }
//    public override void DisplayMyInfo()
//    {
//        Console.Clear();

//        Console.WriteLine("상태보기");
//        Console.WriteLine("캐릭터의 정보를 표시합니다.");
//        Console.WriteLine();
//        Console.WriteLine($"Lv.{Level}");
//        Console.WriteLine($"{Name} ( 도적 )");
//        int addAttack = Program.player1.AttackDamage - _initialAttack;
//        Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
//        int addDefence = Program.player1.Defence - _initialDefence;
//        Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
//        Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
//        Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
//        Console.WriteLine($"Gold : {Gold} G");
//        Console.WriteLine();
//        Console.WriteLine("0. 나가기");
//        Console.WriteLine("원하시는 행동을 입력해주세요.");
//        Console.Write(">>");

//        int input = GameManager.Instance.CheckValidInput(0, 0);
//        switch (input)
//        {
//            case 0:
//                GameManager.Instance.DisplayGameIntro();
//                break;
//        }
//    }

//    // 공격하는 메서드
//    public override void Attack(Character target)
//    {
//        Random random = new Random();

//        // 15% 확률로 치명타 여부 확인
//        bool isCriticalHit = random.Next(1, 101) <= CriticalHitChance;

//        // 10%의 오차 범위 내에서 기본 공격력 계산
//        int damageErrorRange = Convert.ToInt32(Math.Ceiling(Program.player1.AttackDamage / 10.0f));

//        int minDamage = Program.player1.AttackDamage - damageErrorRange;
//        int maxDamage = Program.player1.AttackDamage + damageErrorRange;

//        randomDamage = random.Next(minDamage, maxDamage);

//        // 치명타인 경우 데미지를 정상 데미지의 160%로 계산
//        if (isCriticalHit)
//        {
//            randomDamage = (int)(randomDamage * 1.6f);
//            Console.Clear();
//            Console.WriteLine("치명타 발동!!!!!!\n");
//            Thread.Sleep(1500);
//        }

//        target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
//    }

//}

//public class Archer : Player
//{
//    private const int _initialAttack = 20;
//    private const int _initialDefence = 4;
//    // 치명타 확률에 대한 상수(20%)
//    public const int CriticalHitChance = 20;

//    public int _hp = 30;
//    private int _maxHp = 30;
//    public int _mp = 20;
//    private int _maxMp = 20;
//    public int _attack = 20;
//    public int _defence = 4;

//    public Archer(string name)
//        : base(name)
//    {
//        JobType = JobType.JT_Archer;

//        // Archer의 스킬 설정
//        _fSkillName = "궁수 스킬 1";
//        _sSkillName = "궁수 스킬 2";
//        _fSkillMp = 10;
//        _sSkillMp = 20;
//        _fSkillInfo = "궁수의 스킬 1입니다.";
//        _sSkillInfo = "궁수의 스킬 2입니다.";
//        _fSkillDamage = _attack * 2;
//        _sSkillDamage = _attack * 1.5f;

//        AttackDamage = 20;
//        Defence = 4;
//        Hp = 30;
//        MaxHp = 30;
//        Mp = 20;
//        MaxMp = 20;
//    }

//    public override void DisplayMyInfo()
//    {
//        Console.Clear();

//        Console.WriteLine("상태보기");
//        Console.WriteLine("캐릭터의 정보를 표시합니다.");
//        Console.WriteLine();
//        Console.WriteLine($"Lv.{Level}");
//        Console.WriteLine($"{Name} ( 궁수 )");
//        int addAttack = Program.player1.AttackDamage - _initialAttack;
//        Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
//        int addDefence = Program.player1.Defence - _initialDefence;
//        Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
//        Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
//        Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
//        Console.WriteLine($"Gold : {Gold} G");
//        Console.WriteLine();
//        Console.WriteLine("0. 나가기");
//        Console.WriteLine("원하시는 행동을 입력해주세요.");
//        Console.Write(">>");

//        int input = GameManager.Instance.CheckValidInput(0, 0);
//        switch (input)
//        {
//            case 0:
//                GameManager.Instance.DisplayGameIntro();
//                break;
//        }
//    }

//    // 공격하는 메서드
//    public override void Attack(Character target)
//    {
//        Random random = new Random();

//        // 20% 확률로 치명타 여부 확인
//        bool isCriticalHit = random.Next(1, 101) <= CriticalHitChance;

//        // 10%의 오차 범위 내에서 기본 공격력 계산
//        int damageErrorRange = Convert.ToInt32(Math.Ceiling(Program.player1.AttackDamage / 10.0f));

//        int minDamage = Program.player1.AttackDamage - damageErrorRange;
//        int maxDamage = Program.player1.AttackDamage + damageErrorRange;

//        randomDamage = random.Next(minDamage, maxDamage);

//        // 치명타인 경우 데미지를 정상 데미지의 160%로 계산
//        if (isCriticalHit)
//        {
//            randomDamage = (int)(randomDamage * 1.6f);
//            Console.Clear();
//            Console.WriteLine("치명타 발동!!!!!!\n");
//            Thread.Sleep(1500);
//        }

//        target.ReceiveDamage(randomDamage, DamageType.DT_Normal);
//    }
//}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
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
        }

        public abstract void DisplayMyInfo();

        //inventory 생성


        // 번호로 몬스터를 선택하면 기본 공격(평타)
        public void BasicAttack(Monster selectedMonster)
        {
            // 플레이어가 선택한 몬스터 공격
            Console.Clear();
            int attackDamage = Attack(selectedMonster);
            int inflictedDamage = 0;
            
            if (attackDamage != 0)
            {
                inflictedDamage = Program.player1.randomDamage <= selectedMonster.Defence ? 1 : Program.player1.randomDamage - selectedMonster.Defence;

                Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 기본 공격을 사용하여 {inflictedDamage}의 데미지를 입혔습니다.\n");

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
            }
            
            // 몬스터의 방어력을 고려한 데미지 계산
            

            

            // 몬스터를 죽여 경험치, 골드, 포션 획득
            
            //Console.WriteLine($"현재 경험치 : {Exp}\n");

            //전투 화면으로 돌아가기
            Console.WriteLine("계속하려면 아무 키나 누르세요 . . .");
            Console.ReadKey();
        }


        // 스킬 사용
        public bool UseSkill(List<Monster> aliveMonsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // 몬스터 정보
            // 스테이지 몬스터, List<Monster> 매개변수로
            for (int i = 0; i < aliveMonsters.Count; i++)
            {
                Console.WriteLine($"{aliveMonsters[i].Name} Hp {aliveMonsters[i].Hp} / {aliveMonsters[i].MaxHp}");
            }

            string Job = "전사";
            switch (Program.player1.JobType)
            {
                case JobType.JT_Warrior:
                    Job = "전사";
                    break;
                case JobType.JT_Mage:
                    Job = "마법사";
                    break;
                case JobType.JT_Thief:
                    Job = "도적";
                    break;
                case JobType.JT_Archer:
                    Job = "궁수";
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Level} {_name} ({Job})");
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
                return false;
            }

            // 플레이어의 MP가 선택한 스킬의 소모 MP보다 적은지 확인
            if ((input == 1 && Program.player1.Mp < Program.player1._fSkillMp) || (input == 2 && Program.player1.Mp < Program.player1._sSkillMp))
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                //전투 화면으로 돌아가기
                Console.WriteLine("계속하려면 아무 키나 누르세요 . . .");
                Console.ReadKey();
                return false;
            }
            else
            {
                switch (input)
                {
                    case 1:
                        FirstSkill(aliveMonsters);
                        break;
                    case 2:
                        // 두 번째 스킬 사용 전에 살아있는 몬스터의 수 확인
                        if (aliveMonsters.Count > 1)
                        {
                            SecondSkill(aliveMonsters);
                        }
                        else
                        {
                            Console.WriteLine("살아있는 몬스터가 1마리 이하이므로 두 번째 스킬을 사용할 수 없습니다.");
                            Console.WriteLine("계속하려면 아무 키나 누르세요 . . .");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                }

                return true;
            }
        }


        public void FirstSkill(List<Monster> stageMonsters)
        {
            // 현재 스테이지의 살아있는 몬스터 선택
            int randomMonsterIndex = random.Next(stageMonsters.Count);
            Monster selectedMonster = stageMonsters[randomMonsterIndex];

            // 플레이어가 몬스터 공격
            int damage = Program.player1._fSkillDamage;
            selectedMonster.ReceiveDamage(damage, DamageType.DT_Skill, selectedMonster.Defence);


            Console.Clear();
            // 몬스터의 방어력을 고려한 데미지 계산
            int inflictedDamage = damage <= selectedMonster.Defence ? 1 : damage - selectedMonster.Defence;
            Console.WriteLine($"{_name}이(가) {selectedMonster.Name}에게 {Program.player1._fSkillName}을(를) 사용하여 {inflictedDamage}의 데미지를 입혔습니다.\n");

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
            //Console.WriteLine($"현재 경험치 : {Exp}\n");

            //전투 화면으로 돌아가기
            Console.WriteLine("계속하려면 아무 키나 누르세요 . . .");
            Console.ReadKey();
        }

        public void SecondSkill(List<Monster> stageMonsters)
        {
            // 현재 스테이지의 살아있는 몬스터 중에서 랜덤하게 두 몬스터 선택
            List<int> availableMonster = Enumerable.Range(0, stageMonsters.Count).ToList();

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

            selectedMonster1.ReceiveDamage(damage1, DamageType.DT_Skill, selectedMonster1.Defence);
            selectedMonster2.ReceiveDamage(damage2, DamageType.DT_Skill, selectedMonster2.Defence);

            Console.Clear();
            // 몬스터의 방어력을 고려한 데미지 계산
            int inflictedDamage1 = damage1 <= selectedMonster1.Defence ? 1 : damage1 - selectedMonster1.Defence;
            int inflictedDamage2 = damage2 <= selectedMonster2.Defence ? 1 : damage2 - selectedMonster2.Defence;
            Console.WriteLine($"{_name}이(가) {selectedMonster1.Name}와 {selectedMonster2.Name}에게 {Program.player1._sSkillName}을(를) 사용했고,");
            Console.WriteLine($"각각 {inflictedDamage1}, {inflictedDamage2}의 데미지를 입혔습니다.\n");

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

            //전투 화면으로 돌아가기
            Console.WriteLine("계속하려면 아무 키나 누르세요 . . .");
            Console.ReadKey();
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
            Program.player1.AttackDamage += 2; // 기본 공격력 2 증가
            Program.player1.Defence += 2; // 기본 방어력 2 증가

            Console.WriteLine($"{_name}이(가) Lv.{Level}로 레벨업했습니다!");
        }

        // 다음 레벨까지 필요한 경험치
        public int GetRequiredExpForNextLevel()
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
                    return (Level * 300);
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
                    // 보유 중인 HpPotion 개수 증가
                    Inventory.potionItem[0].Count++;
                    Console.WriteLine($"운 좋게 Hp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Hp포션 개수 : {Inventory.potionItem[0].Count}\n");
                }
                else
                {
                    // 보유 중인 MpPotion 개수 증가
                    Inventory.potionItem[1].Count++;
                    Console.WriteLine($"운 좋게 Mp포션을 1개 획득했습니다!");
                    //Console.WriteLine($"보유 중인 Mp포션 개수 : {Inventory.potionItem[1].Count}\n");
                }
            }
        }
    }



    public class Warrior : Player
    {
        private const int _initialAttack = 11;
        private const int _initialDefence = 7;
        // 치명타 확률에 대한 상수(15%)
        public const int CriticalHitChance = 15;

        public int _hp = 40;
        private int _maxHp = 40;
        public int _mp = 40;
        private int _maxMp = 40;
        public int _attack = 11;
        public int _defence = 7;

        Program program = new Program();
        Random random = new Random();

        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;

            // Warrior의 스킬 설정
            _fSkillName = "스트라이크";
            _sSkillName = "샤이닝 피어스";
            _fSkillMp = 10;
            _sSkillMp = 20;
            _fSkillInfo = "검에 용기를 담아 적군 하나를 내려칩니다.";
            _sSkillInfo = "빛의 힘으로 적군 둘을 날카롭게 찌릅니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 11;
            Defence = 7;
            Hp = 40;
            MaxHp = 40;
            Mp = 40;
            MaxMp = 40;
            Gold = 1500;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();

            int addAttack = Program.player1.AttackDamage - _initialAttack;
            int addDefence = Program.player1.Defence - _initialDefence;

            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(38, 4 + i);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }
                else if (i == 1)
                    Console.WriteLine($"Lv.{Level}");
                else if (i == 2)
                    Console.WriteLine($"현재 경험치: {Exp} / {GetRequiredExpForNextLevel()}\n");
                else if (i == 3)
                    Console.WriteLine();
                else if (i == 4)
                    Console.WriteLine($"{Name} ( 전사 )");
                else if (i == 5)
                    Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
                else if (i == 6)
                    Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
                else if (i == 7)
                    Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
                else if (i == 8)
                    Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
                else if (i == 9)
                    Console.WriteLine($"Gold : {Gold} G");
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("_________________________________");
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.SetCursorPosition(50, 17);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(40, 18);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(50, 19);
            Console.Write(">>");


            int input = GameManager.Instance.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }

        // 공격하는 메서드
        public override int Attack(Character target)
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

            if (target.ReceiveDamage(randomDamage, DamageType.DT_Normal, target.Defence))
            {
                return ReturnDamage(randomDamage, target.Defence);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("그건 제 잔상입니다만,,");
                Console.WriteLine("   -= ∧＿∧");
                Console.WriteLine("-= と(´QωQ`)  \"느려\"");
                Console.WriteLine("   -=/ と_ノ");
                Console.WriteLine("-= _ /_∧_/");


                Console.WriteLine($"{target.Name}이(가) 회피했습니다.");
                return 0;
            }
        }
    }


    public class Mage : Player
    {
        private const int _initialAttack = 13;
        private const int _initialDefence = 5;
        // 치명타 확률에 대한 상수(10%)
        public const int CriticalHitChance = 10;

        public int _hp = 30;
        private int _maxHp = 30;
        public int _mp = 60;
        private int _maxMp = 60;
        public int _attack = 13;
        public int _defence = 5;

        public Mage(string name)
            : base(name)
        {
            JobType = JobType.JT_Mage;

            // Mage의 스킬 설정
            _fSkillName = "익스플로전";
            _sSkillName = "최후의 섬광";
            _fSkillMp = 10;
            _sSkillMp = 15;
            _fSkillInfo = "강력한 화염의 구로 적 하나를 조준합니다.";
            _sSkillInfo = "눈부신 광선을 발사해 적군 둘을 공격합니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 13;
            Defence = 5;
            Hp = 30;
            MaxHp = 30;
            Mp = 60;
            MaxMp = 60;
            Gold = 1500;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();

            int addAttack = Program.player1.AttackDamage - _initialAttack;
            int addDefence = Program.player1.Defence - _initialDefence;

            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(38, 4 + i);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }
                else if (i == 1)
                    Console.WriteLine($"Lv.{Level}");
                else if (i == 2)
                    Console.WriteLine($"현재 경험치: {Exp} / {GetRequiredExpForNextLevel()}\n");
                else if (i == 3)
                    Console.WriteLine();
                else if (i == 4)
                    Console.WriteLine($"{Name} ( 마법사 )");
                else if (i == 5)
                    Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
                else if (i == 6)
                    Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
                else if (i == 7)
                    Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
                else if (i == 8)
                    Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
                else if (i == 9)
                    Console.WriteLine($"Gold : {Gold} G");
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("_________________________________");
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.SetCursorPosition(50, 17);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(40, 18);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(50, 19);
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }

        // 공격하는 메서드
        public override int Attack(Character target)
        {
            Random random = new Random();

            // 10% 확률로 치명타 여부 확인
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

            if (target.ReceiveDamage(randomDamage, DamageType.DT_Normal, target.Defence))
            {
                return ReturnDamage(randomDamage, target.Defence);
            }
            else
            {
                Console.WriteLine($"{target.Name}가 회피했습니다.");
                return 0;
            }
        }
    }


    public class Thief : Player
    {
        private const int _initialAttack = 20;
        private const int _initialDefence = 2;
        // 치명타 확률에 대한 상수(15%)
        public const int CriticalHitChance = 15;
        public int _hp = 30;
        private int _maxHp = 30;
        public int _mp = 40;
        private int _maxMp = 40;
        public int _attack = 20;
        public int _defence = 2;

        public Thief(string name)
            : base(name)
        {
            JobType = JobType.JT_Thief;

            // Thief의 스킬 설정
            _fSkillName = "그림자 돌진";
            _sSkillName = "스타더스트";
            _fSkillMp = 10;
            _sSkillMp = 20;
            _fSkillInfo = "그림자에 몸을 숨겨 적 하나를 관통합니다.";
            _sSkillInfo = "적군 둘에게 어둠의 힘이 담긴 구체를 던집니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 20;
            Defence = 2;
            Hp = 30;
            MaxHp = 30;
            Mp = 40;
            MaxMp = 40;
            Gold = 1500;
        }
        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();

            int addAttack = Program.player1.AttackDamage - _initialAttack;
            int addDefence = Program.player1.Defence - _initialDefence;

            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(38, 4 + i);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }
                else if (i == 1)
                    Console.WriteLine($"Lv.{Level}");
                else if (i == 2)
                    Console.WriteLine($"현재 경험치: {Exp} / {GetRequiredExpForNextLevel()}\n");
                else if (i == 3)
                    Console.WriteLine();
                else if (i == 4)
                    Console.WriteLine($"{Name} ( 도적 )");
                else if (i == 5)
                    Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
                else if (i == 6)
                    Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
                else if (i == 7)
                    Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
                else if (i == 8)
                    Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
                else if (i == 9)
                    Console.WriteLine($"Gold : {Gold} G");
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("_________________________________");
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.SetCursorPosition(50, 17);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(40, 18);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(50, 19);
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }

        // 공격하는 메서드
        public override int Attack(Character target)
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

            if (target.ReceiveDamage(randomDamage, DamageType.DT_Normal, target.Defence))
            {
                return ReturnDamage(randomDamage, target.Defence);
            }
            else
            {
                Console.WriteLine($"{target.Name}가 회피했습니다.");
                return 0;
            }
        }
    }


    public class Archer : Player
    {
        private const int _initialAttack = 16;
        private const int _initialDefence = 4;
        // 치명타 확률에 대한 상수(20%)
        public const int CriticalHitChance = 20;

        public int _hp = 35;
        private int _maxHp = 35;
        public int _mp = 30;
        private int _maxMp = 30;
        public int _attack = 16;
        public int _defence = 4;

        public Archer(string name)
            : base(name)
        {
            JobType = JobType.JT_Archer;

            // Archer의 스킬 설정
            _fSkillName = "정사필중";
            _sSkillName = "바람의 시";
            _fSkillMp = 10;
            _sSkillMp = 20;
            _fSkillInfo = "호흡을 가다듬고 적 하나를 조준합니다.";
            _sSkillInfo = "강력한 화살로 적군 둘을 빠르게 제압합니다.";
            _fSkillDamage = _attack * 2;
            _sSkillDamage = _attack * 1.5f;

            AttackDamage = 16;
            Defence = 4;
            Hp = 35;
            MaxHp = 35;
            Mp = 30;
            MaxMp = 30;
            Gold = 1500;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();

            int addAttack = Program.player1.AttackDamage - _initialAttack;
            int addDefence = Program.player1.Defence - _initialDefence;

            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(38, 4 + i);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("---------------------------------");
                    Console.ResetColor();
                }
                else if (i == 1)
                    Console.WriteLine($"Lv.{Level}");
                else if (i == 2)
                    Console.WriteLine($"현재 경험치: {Exp} / {GetRequiredExpForNextLevel()}\n");
                else if (i == 3)
                    Console.WriteLine();
                else if (i == 4)
                    Console.WriteLine($"{Name} ( 궁수 )");
                else if (i == 5)
                    Console.WriteLine($"공격력: {Program.player1.AttackDamage}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
                else if (i == 6)
                    Console.WriteLine($"방어력: {Program.player1.Defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
                else if (i == 7)
                    Console.WriteLine($"체력: {Program.player1.Hp} / {Program.player1.MaxHp}");
                else if (i == 8)
                    Console.WriteLine($"MP: {Program.player1.Mp} / {Program.player1.MaxMp}");
                else if (i == 9)
                    Console.WriteLine($"Gold : {Gold} G");
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("_________________________________");
                    Console.ResetColor();
                }

            }
            Console.WriteLine();
            Console.SetCursorPosition(50, 17);
            Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(40, 18);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(50, 19);
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }

        // 공격하는 메서드
        public override int Attack(Character target)
        {
            Random random = new Random();

            // 20% 확률로 치명타 여부 확인
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

            if (target.ReceiveDamage(randomDamage, DamageType.DT_Normal, target.Defence))
            {
                return ReturnDamage(randomDamage, target.Defence);
            }
            else
            {
                Console.WriteLine($"{target.Name}가 회피했습니다.");
                return 0;
            }
        }
    }
}

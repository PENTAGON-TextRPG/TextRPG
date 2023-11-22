using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace PENTAGON
{
    public class Dungeon
    {
        MonsterManager monsterManager = new MonsterManager();
        StageType stage;
        public void DisplayDungeonIntro(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 스테이지 선택");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = GameManager.Instance.CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    player.DisplayMyInfo();//플레이어 정보보기 메서드로 이동
                    break;
                case 2:
                    DisplayStage();//스테이지 선택 메서드
                    break;
                case 0:
                    GameManager.Instance.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면

        public void DisplayStage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("입장할 층을 입력해주세요.\n");
            var table = new ConsoleTable("층", "추천 공격력", "추천 방어력");

            table.AddRow(1, "5", "3");
            table.AddRow(2, "10", "5");
            table.AddRow(3, "15", "10");
            table.AddRow(4, "20","15");
            table.AddRow(5, "25", "20");
            table.Options.EnableCount = false;

            Console.WriteLine(table);   //층 별 난이도 표시
            Console.Write(">>");
            int input = GameManager.Instance.CheckValidInput(1, 5);
            switch(input)
            {
                case 1:
                    stage = StageType.ST_One;
                    break;
                case 2:
                    stage = StageType.ST_Two;
                    break;
                case 3:
                    stage = StageType.ST_Three;
                    break;
                case 4:
                    stage = StageType.ST_Four;
                    break;
                case 5:
                    stage = StageType.ST_Five;
                    break;
            }

            Battle(Program.player1, stage);//현재 플레이어 도달 스테이지로 이동
        }
        public void Battle(Player player, StageType stageType)
        {
            List<Monster> monsters = new List<Monster>(); //몬스터 리스트 선언
            monsters = monsterManager.GetMonsterOfStage(stageType); //해당 스테이지 몬스터의 리스트 저장
            var random = new Random();
            int monstercount = random.Next(1,5); //출현할 몬스터의 수
            List<Monster> stageMonster=monsters.OrderBy(x=>random.Next()).Take(monstercount).ToList(); //스테이지에 출현하는 몬스터 리스트 생성
            List<Monster> deadMonster = new List<Monster>(); // 죽은 몬스터 리스트
            List<Monster> aliveMonster = new List<Monster>(); // 살아있는 몬스터 리스트
            int alivecount = 0; //살아있는 몬스터의 수
            string Job = "전사";
            switch(player.JobType)
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

            for(int i=0;i<monstercount;i++)
            {
                aliveMonster.Add(stageMonster[i]); //현재 스테이지 몬스터 살아있는 몬스터 리스트로 추가
            }
 
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Battle!!");
                Console.ResetColor();
                for (int i = 0; i < monstercount; i++)
                {
                    if (aliveMonster.Contains(stageMonster[i]))
                    {
                        alivecount++;
                        Console.WriteLine($"{alivecount}. {stageMonster[i].Name} HP {stageMonster[i].Hp}/{stageMonster[i].MaxHp}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{stageMonster[i].Name} HP DEAD");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("\n[내 정보]");
                Console.WriteLine($"Lv. {player.Level} {player.Name} ({Job})"); //플레이어 레벨, 직업을 불러올 수 있어야 함
                Console.WriteLine($"HP {player.Hp} / {player.MaxHp}");
                Console.WriteLine($"MP {player.Mp} / {player.MaxMp}");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 아이템 사용");
                Console.WriteLine("4. 도망");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = GameManager.Instance.CheckValidInput(1, 4);

                if (input == 1)
                {
                    Console.WriteLine("공격할 몬스터를 선택해 주세요.");
                    Console.Write(">>");
                    int select = GameManager.Instance.CheckValidInput(1, alivecount);
                    player.BasicAttack(aliveMonster[select - 1]);//플레이어 공격 처리(몬스터 데미지 계산)
                }
                else if (input == 2)
                {
                    if (player.UseSkill(aliveMonster) == false)//플레이어 스킬 처리
                    {
                        alivecount = 0;
                        continue;//플레이어가 스킬을 사용하지 않으면
                    }
                }
                else if(input == 3)
                {
                    Console.Clear();
                    Console.WriteLine("[아이템 사용]");
                    Console.WriteLine($"1. Hp 포션 {player.Inventory.potionItem[0].Count}개");
                    Console.WriteLine($"2. Mp 포션 {player.Inventory.potionItem[1].Count}개");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    int potion = GameManager.Instance.CheckValidInput(0, 2);

                    switch(potion)
                    {
                        case 1:
                            player.Inventory.EatPotion(player.Inventory.potionItem[0]);
                            break;
                        case 2:
                            player.Inventory.EatPotion(player.Inventory.potionItem[1]);
                            break;
                        case 0:
                            continue;
                    }
                    //플레이어 아이템 사용
                }
                else
                {
                    player.Hp /= 2; //플레이어 체력을 절반으로 하고 던전 입장화면으로 이동
                    DisplayDungeonIntro(player);
                }

                for (int i = 0; i < aliveMonster.Count; i++)
                {
                    if (aliveMonster[i].IsDie() == true)    //몬스터 생존 판정
                    {
                        deadMonster.Add(aliveMonster[i]);   //죽은 몬스터를 죽은 몬스터 리스트에 추가
                        aliveMonster.Remove(aliveMonster[i]);//죽은 몬스터는 리스트에서 제거
                        if (aliveMonster.Count == 0)
                        {
                            Console.WriteLine("모든 몬스터를 처치했습니다.");
                            Console.WriteLine("승리했습니다.");
                            Console.ReadKey();
                            DisplayDungeonIntro(player);
                        }
                        i--;
                    }
                    else                //살아있는 몬스터 행동 판정
                    {
                        Console.WriteLine($"{aliveMonster[i].Name}의 공격! {aliveMonster[i].Attack(player)} 의 데미지를 받았습니다.");
                    }
                    if (player.Hp <= 0) //전투 패배 시 게임 종료
                    {
                        Console.WriteLine("YOU DIE\n");
                        Console.WriteLine("시작 화면으로 이동합니다.");
                        player.Hp = player.MaxHp / 10;
                        Console.ReadKey();
                        GameManager.Instance.DisplayGameIntro();
                        break;
                    }
                }
                Console.ReadKey();

                if (player.Hp <= 0) //전투 패배 시 게임 종료
                {
                    break;
                }
                alivecount = 0;
            }
        }
    }
}

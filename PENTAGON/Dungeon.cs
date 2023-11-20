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
        GameManager GameManager = new GameManager();
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

            int input = GameManager.CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    player.DisplayMyInfo();//플레이어 정보보기 메서드로 이동
                    break;
                case 2:
                    DisplayStage();//스테이지 선택 메서드
                    break;
                case 0:
                    GameManager.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면

        public void DisplayStage()
        {
            var table = new ConsoleTable("층","추천 공격력","추천 방어력");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("입장할 층을 입력해주세요.\n");

            table.AddRow(1, "5", "3");
            table.AddRow(2, "10", "5");
            table.AddRow(3, "15", "10");
            table.AddRow(4, "20","15");
            table.AddRow(5, "25", "20");

            Console.WriteLine(table);   //층 별 난이도 표시

            int input = GameManager.CheckValidInput(1, 5);
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
            while (true)
            {
                int IsWin = monstercount; //전투 승리 판정
                Console.Clear();
                Console.WriteLine("Battle!!");
                for(int i =0;i<monstercount;i++)
                {
                    var monsterAlive = (stageMonster[i].Hp > 0) ? (stageMonster[i].Hp).ToString() : "Dead"; //몬스터의 체력이 0이하일때 Dead를 출력, 0보다 크면 그 숫자를 출력
                    Console.WriteLine($"{i+1}. {stageMonster[i].Name} HP {monsterAlive}");
                    if(monsterAlive == "Dead")
                    {
                        IsWin--;//몬스터가 죽었으면 판정
                        stageMonster[i].IsDie();
                        if(IsWin == 0) //몬스터가 모두 죽었을때 전투승리 판정
                        {
                            player.GetPosionItems();
                            Console.WriteLine("모든 몬스터를 처치했습니다.");
                            Console.WriteLine("승리했습니다.\n다음 스테이지로 이동합니다.");
                            Thread.Sleep(1000);
                            DisplayDungeonIntro(player);
                        }
                    }
                }

                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.JobType})"); //플레이어 레벨, 직업을 불러올 수 있어야 함
                Console.WriteLine($"HP {player.Hp} / {player.MaxHp}");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 아이템 사용");
                Console.WriteLine("4. 도망");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = GameManager.CheckValidInput(1, 3);

                if (input == 1)
                {
                    Console.WriteLine("공격할 몬스터를 선택해 주세요.");
                    Console.Write(">>");
                    int select = GameManager.CheckValidInput(1, monstercount);
                    player.BasicAttack(stageMonster[select - 1]);//플레이어 공격 처리(몬스터 데미지 계산)
                }
                else if (input == 2)
                {
                    if (player.UseSkill(stageMonster))//플레이어 스킬 처리
                        continue;//플레이어가 스킬을 사용하지 않으면

                }
                else if(input == 3)
                {
                    //플레이어 아이템 사용
                }
                else
                {
                    player.Hp /= 2; //플레이어 체력을 절반으로 하고 던전 입장화면으로 이동
                    DisplayDungeonIntro(player);
                }

                //몬스터 행동 판정
                if (player.Hp <= 0) //전투 패배 시 던전 입장 화면으로 이동
                {
                    Console.WriteLine("YOU DIE\n던전 입장 화면으로 이동합니다.");
                    player.Hp = player.MaxHp/10; 
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}

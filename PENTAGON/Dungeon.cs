using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Dungeon
    {
        Program program = new Program();
        public void DisplayDungeonIntro(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine($"2. 전투 시작");//player 클래스에서 해당 플레이어가 도달한 스테이지 변수 추가
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = program.CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    player.DisplayMyInfo();//플레이어 정보보기 메서드로 이동
                    break;
                case 2:
                    DisplayStage();//스테이지 선택 메서드
                    break;
                case 0:
                    program.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면

        public void DisplayStage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Battle(Program.player1, );//현재 플레이어 도달 스테이지로 이동
        }
        public void Battle(Player player, List<Monster> monsters, StageType stageType)
        {
            var random = new Random();
            int monstercount = random.Next(1, 5); //출현하는 몬스터의 수 랜덤변수
            var takemonster = monsters.OrderBy(x => x.Name).Take(monstercount); //출현하는 몬스터 수만큼 몬스터를 가져옴
            while (true)
            {
                int IsWin = monstercount; //전투 승리 판정
                Console.Clear();
                Console.WriteLine("Battle!!");
                int i = 0;
                foreach(List<Monster> monster in takemonster)
                {
                    var monsterAlive = (monster[i].Hp > 0) ? (monster[i].Hp).ToString() : "Dead"; //몬스터의 체력이 0이하일때 Dead를 출력, 0보다 크면 그 숫자를 출력
                    Console.WriteLine($"{monster[i].Name} HP {monsterAlive}");
                    if(monsterAlive == "Dead")
                    {
                        IsWin--;//몬스터가 죽었으면 판정
                        monster[].IsDie();
                        if(IsWin == 0) //몬스터가 모두 죽었을때 전투승리 판정
                        {
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
                int input = program.CheckValidInput(1, 3);

                switch (input)
                {
                    case 1:
                        Console.WriteLine("공격할 몬스터를 선택해 주세요.");
                        Console.Write(">>");
                        int select = program.CheckValidInput(1, monstercount);
                        player.Attack(monsters[select - 1]);//플레이어 공격 처리(몬스터 데미지 계산)
                        //데미지 계산 처리에서 몬스터가 죽으면 player 경험치 획득 메서드 실행
                        break;
                    case 2:
                        player.UseSkill();//플레이어 스킬 처리
                        break;
                    case 3:
                        //플레이어 아이템 사용
                        break;
                    case 4:
                        player.Hp /= 2; //플레이어 체력을 절반으로 하고 던전 입장화면으로 이동
                        DisplayDungeonIntro(player);
                        break;
                }

                //몬스터 행동 판정
                if(player.Hp <= 0) //전투 패배 시 던전 입장 화면으로 이동
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

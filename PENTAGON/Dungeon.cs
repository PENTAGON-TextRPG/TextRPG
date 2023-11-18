using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class Dungeon
    {
        public void DisplayDungeonIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine($"2. 전투 시작 (현재 진행 : {Player.stage}층");//player 클래스에서 해당 플레이어가 도달한 스테이지 변수 추가
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = Program.CheckValidInput(0, 2);
            switch (input)
            {
                case 1:
                    Player.DisplayInfo();//플레이어 정보보기 메서드로 이동
                case 2:
                    Battle(Program.player, Program.Monster, Player.stage);//현재 플레이어 도달 스테이지로 이동
                case 0:
                    Program.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면
        public void Battle(Player player, List<Monster> monsters, int stage)
        {
            var random = new Random();
            int monstercount = random.Next(1, 5); //출현하는 몬스터의 수 랜덤변수
            var takemonster = monsters.OrderBy(x => x.Name).Take(monstercount); //출현하는 몬스터 수만큼 몬스터를 가져옴
            while (true)
            {
                int IsWin = monstercount; //전투 승리 판정
                Console.Clear();
                Console.WriteLine("Battle!!");
                foreach (Monster monster in takemonster)
                {
                    var monsterAlive = (monster.Hp > 0) ? (monster.Hp).ToString() : "Dead"; //몬스터의 체력이 0이하일때 Dead를 출력, 0보다 크면 그 숫자를 출력
                    Console.WriteLine($"{monster.Name} HP {monsterAlive}");
                    if(monsterAlive == "Dead")
                    {
                        IsWin--;//몬스터가 죽었으면 판정
                        monster.IsDie();
                        if(IsWin == 0) //몬스터가 모두 죽었을때 전투승리 판정
                        {
                            Console.WriteLine("모든 몬스터를 처치했습니다.");
                            Console.WriteLine("승리했습니다.\n다음 스테이지로 이동합니다.");
                            player.stage++;//플레이어 도달 층 증가
                            Thread.Sleep(1000);
                            Battle(player, monsters, stage);
                        }
                    }
                }

                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.job})"); //플레이어 레벨, 직업을 불러올 수 있어야 함
                Console.WriteLine($"HP {player.Hp} / {player.MaxHp}");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. ");
                Console.WriteLine("4. 도망");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = Program.CheckValidInput(1, 3);

                switch (input)
                {
                    case 1:
                        //플레이어 공격 처리(몬스터 데미지 계산)
                        //데미지 계산 처리에서 몬스터가 죽으면 player 경험치 획득 메서드 실행
                        break;
                    case 2:
                        //플레이어 스킬 처리
                        //데미지를 주는 스킬로 데미지 계산 처리에서 몬스터가 죽으면 player 경험치 획득 메서드 실행
                        break;
                    case 3:
                        player.Hp /= 2; //플레이어 체력을 절반으로 하고 던전 입장화면으로 이동
                        DisplayDungeonIntro();
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

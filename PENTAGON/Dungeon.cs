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
            int monstercount = random.Next(1, 5);
            var takemonster = monsters.OrderBy(x => x.Name).Take(monstercount);
            while (true)
            {
                int IsWin = monstercount;
                Console.Clear();
                Console.WriteLine("Battle!!");
                foreach (Monster monster in takemonster)
                {
                    var monsterAlive = (monster.Hp > 0) ? (monster.Hp).ToString() : "Dead";
                    Console.WriteLine($"{monster.Name} HP {monsterAlive}");
                    if(monsterAlive == "Dead")
                    {
                        IsWin--;
                        if(IsWin == 0)
                        {
                            Console.WriteLine("모든 몬스터를 처치했습니다.");
                            Console.WriteLine("승리했습니다.\n다음 스테이지로 이동합니다.");
                            player.stage++;
                            Thread.Sleep(1000);
                            Battle(player, monsters, stage);
                        }
                    }
                }

                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv. {player.Level} {player.Name} ({player.job})");
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
                        break;
                    case 2:
                        //플레이어 스킬 처리
                        break;
                    case 3:
                        player.Hp /= 2;
                        DisplayDungeonIntro();
                        break;
                }
                if(player.Hp <= 0)
                {
                    Console.WriteLine("패배했습니다.. 던전 입장 화면으로 이동합니다.");
                    player.Hp = player.MaxHp/10;
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}

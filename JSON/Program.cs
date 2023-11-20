using EnumsNamespace;
using Newtonsoft.Json;
using PENTAGON;
using System.Threading;

namespace JSON
{
    public class MonsterLists
    {
        [JsonProperty("stage1")]
        public List<Monster> Stage1 { get; set; }

        [JsonProperty("stage2")]
        public List<Monster> Stage2 { get; set; }

        [JsonProperty("stage3")]
        public List<Monster> Stage3 { get; set; }

        public MonsterLists()
        {
            Stage1 = new List<Monster>();
            Stage2 = new List<Monster>();
            Stage3 = new List<Monster>();
        }
    }
    public class Program
    {
        static void Main()
        {
            MonsterLists monsterLists = new MonsterLists();

            //Stage1
            Slime slime = new Slime();
            RatRider ratRider = new RatRider();
            Goblin goblin = new Goblin();
            Skeleton skeleton = new Skeleton();

            //Stage2
            Golem golem = new Golem();
            Crocodile crocodile = new Crocodile();
            Orc orc = new Orc();
            Sorcerer sorcerer = new Sorcerer();

            //Stage3
            FlameElemental flameElemental = new FlameElemental();
            Minotaur minotaur = new Minotaur();
            DarkKnight darkKnight = new DarkKnight();
            Dragon dragon = new Dragon();

            monsterLists.Stage1.Add(slime);
            monsterLists.Stage1.Add(ratRider);
            monsterLists.Stage1.Add(goblin);
            monsterLists.Stage1.Add(skeleton);

            monsterLists.Stage2.Add(golem);
            monsterLists.Stage2.Add(crocodile);
            monsterLists.Stage2.Add(orc);
            monsterLists.Stage2.Add(sorcerer);

            monsterLists.Stage3.Add(flameElemental);
            monsterLists.Stage3.Add(minotaur);
            monsterLists.Stage3.Add(darkKnight);
            monsterLists.Stage3.Add(dragon);


            string filePath = @"D:\jchwoon\PENTAGON\monster.json";

            string jsonData = JsonConvert.SerializeObject(monsterLists, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);

            Console.WriteLine("JSON 파일이 생성되었습니다.");
        }
    }
}
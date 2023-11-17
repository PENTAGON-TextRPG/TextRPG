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

        public MonsterLists()
        {
            Stage1 = new List<Monster>();
            Stage2 = new List<Monster>();
        }
    }
    public class Program
    {
        static void Main()
        {
            MonsterLists monsterLists = new MonsterLists();

            Slime slime = new Slime();
            RatRider ratRider = new RatRider();
            Goblin goblin = new Goblin();
            Skeleton skeleton = new Skeleton();

            monsterLists.Stage1.Add(slime);
            monsterLists.Stage1.Add(ratRider);
            monsterLists.Stage1.Add(goblin);
            monsterLists.Stage1.Add(skeleton);


            string filePath = @"D:\jchwoon\PENTAGON\monster.json";

            string jsonData = JsonConvert.SerializeObject(monsterLists, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);

            Console.WriteLine("JSON 파일이 생성되었습니다.");
        }
    }
}
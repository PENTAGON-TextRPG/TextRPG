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
        [JsonProperty("stage4")]
        public List<Monster> Stage4 { get; set; }
        [JsonProperty("stage5")]
        public List<Monster> Stage5 { get; set; }

        public MonsterLists()
        {
            Stage1 = new List<Monster>();
            Stage2 = new List<Monster>();
            Stage3 = new List<Monster>();
            Stage4 = new List<Monster>();
            Stage5 = new List<Monster>();
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

            //Stage4
            Phoenix phoenix = new Phoenix();
            GiantBear giantBear = new GiantBear();
            MysticalWizard mysticalWizard = new MysticalWizard();
            Demon demon = new Demon();
            //Stage5
            Queen queen = new Queen();
            VerdantShadow verdantShadow = new VerdantShadow();
            InfernoOverlord infernoOverlord = new InfernoOverlord();
            Wraith wraith = new Wraith();

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

            monsterLists.Stage4.Add(phoenix);
            monsterLists.Stage4.Add(giantBear);
            monsterLists.Stage4.Add(mysticalWizard);
            monsterLists.Stage4.Add(demon);

            monsterLists.Stage5.Add(queen);
            monsterLists.Stage5.Add(verdantShadow);
            monsterLists.Stage5.Add(infernoOverlord);
            monsterLists.Stage5.Add(wraith);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folderPath = path + @"\PENTAGON\data";
            string filePath = path + @"\PENTAGON\data\monster.json";

            string jsonData = JsonConvert.SerializeObject(monsterLists, Formatting.Indented);

            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllText(filePath, jsonData);


            Console.WriteLine("JSON 파일이 생성되었습니다.");
        }
    }
}
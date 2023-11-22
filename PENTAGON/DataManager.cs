using EnumsNamespace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class DataManager
    {
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataManager();
                }
                return _instance;
            }
        }

        public void InitializeMonsterDict()
        {
            _monsterLists = _json.GetMonsterJsonData();

            string[] StageEnumArray = System.Enum.GetNames(typeof(StageType));
            int stageTypeLength = StageEnumArray.Length;

            for (int i = 1; i <= stageTypeLength; i++)
            {
                StageType currentStage = (StageType)i;
                var stageProperty = typeof(MonsterLists).GetProperty("Stage" + (i));

                if (stageProperty != null)
                {
                    List<Monster> monstersOfStage = (List<Monster>)stageProperty.GetValue(_monsterLists);
                    _monsterDict.Add(currentStage, monstersOfStage);
                }
            }
        }

        public void SavePlayerData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = path + @"\PENTAGON\data\player.json"; //player 정보 저장 위치

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            PlayerData data = new PlayerData(Program.player1);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        public void LoadPlayerData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = path + @"\PENTAGON\data\player.json"; //player 정보 저장 위치

            if(File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);

                switch (data.job)
                {
                    case JobType.JT_Warrior:
                        Program.player1 = new Warrior(data.name);
                        break;
                    case JobType.JT_Mage:
                        Program.player1 = new Mage(data.name);
                        break;
                    case JobType.JT_Thief:
                        Program.player1 = new Thief(data.name);
                        break;
                    case JobType.JT_Archer:
                        Program.player1 = new Archer(data.name);
                        break;
                }

                Program.player1.Level = data.level;
                Program.player1.MaxHp = data.maxHp;
                Program.player1.MaxMp = data.maxMp;
                Program.player1.Mp = data.mp;
                Program.player1.Hp = data.hp;
                Program.player1.AttackDamage = data.attackDamage;
                Program.player1.Defence = data.defence;
                Program.player1.JobType = data.job;
                Program.player1.Inventory = data.inventory;
            }
        }

        public Dictionary<StageType, List<Monster>> MonsterDict { get { return _monsterDict; } }
        private MonsterLists _monsterLists;
        private Dictionary<StageType, List<Monster>> _monsterDict = new Dictionary<StageType, List<Monster>>();
        private JSON _json = new JSON();
        private static DataManager _instance;
    }
}

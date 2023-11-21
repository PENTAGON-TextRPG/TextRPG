using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PENTAGON
{
    public class MonsterLists
    {
        public List<Monster> Stage1 { get; set; }
        public List<Monster> Stage2 { get; set; }
        public List<Monster> Stage3 { get; set; }
        public List<Monster> Stage4 { get; set; }
        public List<Monster> Stage5 { get; set; }
    }
    public class JSON
    {
        public MonsterLists GetMonsterJsonData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = path + @"\PENTAGON\data\monster.json";

            // JSON 파일에서 데이터 읽어오기
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                MonsterLists monsterLists = JsonConvert.DeserializeObject<MonsterLists>(jsonData);

                return monsterLists;
            }
            else
            {
                return null;
            }

        }
    }
}

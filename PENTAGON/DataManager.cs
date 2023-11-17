using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _monsterLists = _json.GetJsonData();

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

        public Dictionary<StageType, List<Monster>> MonsterDict { get { return _monsterDict; } }

        private MonsterLists _monsterLists;
        private Dictionary<StageType, List<Monster>> _monsterDict = new Dictionary<StageType, List<Monster>>();
        private JSON _json = new JSON();
        private static DataManager _instance;
    }
}

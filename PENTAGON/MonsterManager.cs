using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    public class MonsterManager
    {
        public List<Monster> GetMonsterOfStage(StageType stageType)
        {
            return DataManager.Instance.MonsterDict[stageType];
        }
    }
}

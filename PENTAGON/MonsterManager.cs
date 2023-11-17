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
            Program program = new Program();

            return program.MonsterDict[stageType];
        }
    }
}

using EnumsNamespace;

namespace PENTAGON
{

    public class MonsterManager
    {
        public static MonsterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonsterManager();
                }
                return _instance;
            }
        }
        
        public List<Monster> GetMonsterOfStage(StageType stageType)
        {
            List<Monster> originalList = DataManager.Instance.MonsterDict[stageType];
            List<Monster> copyList = originalList.Select(monster => Clone(monster)).ToList();
            return copyList;
        }

        private Monster Clone(Monster monster)
        {
            return new Monster
            {
                Hp = monster.Hp,
                MaxHp = monster.MaxHp,
                Damage = monster.Damage,
                Defence = monster.Defence,
                Exp = monster.Exp,
                Gold = monster.Gold,
                Stage = monster.Stage,
                Name = monster.Name,
            };
        }

        private static MonsterManager _instance;
    }
}

using EnumsNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PENTAGON
{
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int level, hp, maxHp, mp, maxMp, attackDamage, defence, gold;
        public Inventory inventory;
        public JobType job;
        public Item[] equipmentItem = new Item[2];

        public PlayerData()
        {

        }
        public PlayerData(Player player)
        {
            name = player.Name;
            level = player.Level;
            hp = player.Hp;
            maxHp = player.MaxHp;
            mp = player.Mp;
            maxMp = player.MaxMp;
            attackDamage = player.AttackDamage;
            defence = player.Defence;
            inventory = player.Inventory;
            job = player.JobType;
            gold = player.Gold;
        }
    }
}

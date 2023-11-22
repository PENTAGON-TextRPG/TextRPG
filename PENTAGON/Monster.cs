using EnumsNamespace;

namespace PENTAGON
{
    public class Monster : Character
    {
        public event EventHandler Clid;

        
        public StageType Stage 
        {
            get { return _stage; } 
            set { _stage = value; }
        }

        private StageType _stage;
    }
    //***************************
    //          Stage1           
    //***************************
    public class Slime : Monster
    {
        public Slime()
        {
            Hp = 10;
            MaxHp = 10;
            Damage = 3;
            Defence = 1;
            Exp = 1;
            Gold = 50;
            Stage = StageType.ST_One;
            Name = "Slime";
        }
    }

    public class RatRider : Monster
    {
        public RatRider()
        {
            Hp = 12;
            MaxHp = 12;
            Damage = 3;
            Defence = 1;
            Exp = 2;
            Gold = 75;
            Stage = StageType.ST_One;
            Name = "RatRider";
        }
    }

    public class Goblin : Monster
    {
        public Goblin()
        {
            Hp = 15;
            MaxHp = 15;
            Damage = 4;
            Defence = 2;
            Exp = 3;
            Gold = 100;
            Stage = StageType.ST_One;
            Name = "Goblin";
        }
    }

    public class Skeleton : Monster
    {
        public Skeleton()
        {
            Hp = 17;
            MaxHp = 17;
            Damage = 4;
            Defence = 2;
            Exp = 3;
            Gold = 125;
            Stage = StageType.ST_One;
            Name = "Skeleton";
        }
    }

    //***************************
    //          Stage2           
    //***************************
    public class Golem : Monster
    {
        public Golem()
        {
            Hp = 20;
            MaxHp = 20;
            Damage = 7;
            Defence = 3;
            Exp = 5;
            Gold = 175;
            Stage = StageType.ST_Two;
            Name = "Golem";
        }
    }
    public class Crocodile : Monster
    {
        public Crocodile()
        {
            Hp = 23;
            MaxHp = 23;
            Damage = 7;
            Defence = 3;
            Exp = 5;
            Gold = 225;
            Stage = StageType.ST_Two;
            Name = "Crocodile";
        }
    }
    public class Orc : Monster
    {
        public Orc()
        {
            Hp = 25;
            MaxHp = 25;
            Damage = 8;
            Defence = 3;
            Exp = 6;
            Gold = 275;
            Stage = StageType.ST_Two;
            Name = "Orc";
        }
    }
    public class Sorcerer : Monster
    {
        public Sorcerer()
        {
            Hp = 30;
            MaxHp = 30;
            Damage = 8;
            Defence = 3;
            Exp = 6;
            Gold = 325;
            Stage = StageType.ST_Two;
            Name = "Sorcerer";
        }
    }

    //***************************
    //          Stage3           
    //***************************
    public class FlameElemental : Monster
    {
        public FlameElemental()
        {
            Hp = 35;
            MaxHp = 35;
            Damage = 13;
            Defence = 6;
            Exp = 9;
            Gold = 400;
            Stage = StageType.ST_Three;
            Name = "FlameElemental";
        }
    }
    public class Minotaur : Monster
    {
        public Minotaur()
        {
            Hp = 37;
            MaxHp = 37;
            Damage = 13;
            Defence = 6;
            Exp = 9;
            Gold = 475;
            Stage = StageType.ST_Three;
            Name = "Minotaur";
        }
    }
    public class DarkKnight : Monster
    {
        public DarkKnight()
        {
            Hp = 40;
            MaxHp = 40;
            Damage = 14;
            Defence = 6;
            Exp = 10;
            Gold = 550;
            Stage = StageType.ST_Three;
            Name = "DarkKnight";
        }
    }

    public class Dragon : Monster
    {
        public Dragon()
        {
            Hp = 45;
            MaxHp = 45;
            Damage = 14;
            Defence = 6;
            Exp = 10;
            Gold = 625;
            Stage = StageType.ST_Three;
            Name = "Dragon";
        }
    }

    //***************************
    //          Stage4           
    //***************************
    public class Phoenix : Monster
    {
        public Phoenix()
        {
            Hp = 145;
            MaxHp = 145;
            Damage = 30;
            Defence = 20;
            Exp = 20;
            Gold = 725;
            Stage = StageType.ST_Four;
            Name = "Phoenix";
        }
    }
    public class GiantBear : Monster
    {
        public GiantBear()
        {
            Hp = 165;
            MaxHp = 165;
            Damage = 34;
            Defence = 23;
            Exp = 23;
            Gold = 825;
            Stage = StageType.ST_Four;
            Name = "GiantBear";
        }
    }
    public class MysticalWizard : Monster
    {
        public MysticalWizard()
        {
            Hp = 185;
            MaxHp = 185;
            Damage = 38;
            Defence = 26;
            Exp = 26;
            Gold = 925;
            Stage = StageType.ST_Four;
            Name = "MysticalWizard";
        }
    }
    public class Demon : Monster
    {
        public Demon()
        {
            Hp = 205;
            MaxHp = 205;
            Damage = 42;
            Defence = 29;
            Exp = 29;
            Gold = 1025;
            Stage = StageType.ST_Four;
            Name = "Demon";
        }
    }
    //***************************
    //          Stage5           
    //***************************
    public class Queen : Monster
    {
        public Queen()
        {
            Hp = 235;
            MaxHp = 235;
            Damage = 48;
            Defence = 33;
            Exp = 33;
            Gold = 1175;
            Stage = StageType.ST_Five;
            Name = "Queen";
        }
    }
    public class VerdantShadow : Monster
    {
        public VerdantShadow()
        {
            Hp = 265;
            MaxHp = 265;
            Damage = 54;
            Defence = 37;
            Exp = 37;
            Gold = 1325;
            Stage = StageType.ST_Five;
            Name = "VerdantShadow";
        }

    }
    public class InfernoOverlord : Monster
    {
        public InfernoOverlord()
        {
            Hp = 295;
            MaxHp = 295;
            Damage = 60;
            Defence = 41;
            Exp = 41;
            Gold = 1475;
            Stage = StageType.ST_Five;
            Name = "InfernoOverlord";
        }
    }
    public class Wraith : Monster
    {
        public Wraith()
        {
            Hp = 325;
            MaxHp = 325;
            Damage = 66;
            Defence = 45;
            Exp = 45;
            Gold = 1625;
            Stage = StageType.ST_Five;
            Name = "Wraith";
        }
    }
}

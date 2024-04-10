using _Base.Scripts.Database;
using Demo.Scripts;
using UnityEngine;

namespace Demo.Scripts
{
    public class Database : MonoBehaviour
    {
        string monsterPath = "Database/Monster/monster";
        string heroPath = "Database/Hero/hero";

        public static Database<DataDef> Monster;
        public static Database<HeroDef> Hero;
        public void Load()
        {
            Monster = new Database<DataDef>(monsterPath);
            Hero = new Database<HeroDef>(heroPath);
            Hero.Load();
            Monster.Load();
        }
    }
}

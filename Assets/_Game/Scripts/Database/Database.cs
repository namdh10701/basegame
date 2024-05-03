using _Base.Scripts.Database;
using _Game.Scripts.InventorySystem;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Scripts.Database
{
    public class Database : MonoBehaviour
    {
        const string gearPath = "Database/Gears/gear";
        //Database<Gear> Gear = new Database<Gear>(gearPath);
        public void Load()
        {
            //Gear.Load();
            //Debug.Log(Gear.Records.Count);
        }
    }
}

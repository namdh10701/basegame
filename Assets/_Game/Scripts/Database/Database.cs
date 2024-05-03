using _Base.Scripts.Database;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Scripts.Database
{
    public class Database : MonoBehaviour
    {
        string hatGearPath = "Database/Gears/Hat/Images";
        string swordGearPath = "Database/Gears/Sword/Images";
        string necklaceGearPath = "Database/Gears/Necklace/Images";
        public void Load()
        {
            /*Sprite[] hatSprites = Resources.LoadAll<Sprite>(hatGearPath);
            Sprite[] swordSprites = Resources.LoadAll<Sprite>(swordGearPath);
            Sprite[] necklaces = Resources.LoadAll<Sprite>(necklaceGearPath);
            int count = 1;

            foreach (Sprite sprite in hatSprites)
            {
                GearIdentifier gearIdentifier = new GearIdentifier(sprite);
                GearKey gearKey = new GearKey(count, GearType.Hat);
                gearIdentifier.Id = gearKey;
                Gear.Records.Add(gearKey, gearIdentifier);
                count++;
            }
            count = 1;
            foreach (Sprite sprite in swordSprites)
            {
                GearIdentifier gearIdentifier = new GearIdentifier(sprite);
                GearKey gearKey = new GearKey(count, GearType.Sword);
                gearIdentifier.Id = gearKey;
                Gear.Records.Add(gearKey, gearIdentifier);
                count++;
            }
            count = 1;
            foreach (Sprite sprite in necklaces)
            {
                GearIdentifier gearIdentifier = new GearIdentifier(sprite);
                GearKey gearKey = new GearKey(count, GearType.Necklace);
                gearIdentifier.Id = gearKey;
                Gear.Records.Add(gearKey, gearIdentifier);
                count++;
            }*/

        }
    }
}

using _Game.Scripts.Entities;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;
using _Game.Features.Inventory;
using UnityEngine;
using _Game.Features.Gameplay;

namespace _Game.Scripts
{
    public static class ResourceLoader
    {
        [SerializeField]
        private static Sprite[] cannonSprites;

        static string hatGearPath = "Database/Gears/Hat/Images";
        static string swordGearPath = "Database/Gears/Sword/Images";
        static string necklaceGearPath = "Database/Gears/Necklace/Images";
        static string characterPath = "Database/Characters/Captain";
        static string skillPath = "Database/Skills/Images";

        static string cannonImagePath = "Database/GridItem/Cannons";
        static string bulletImagePath = "Database/GridItem/Bullets";

        static string cannonPrefabPath = "Prefabs/GridItems/Cannons";
        static string bulletPrefabPath = "Prefabs/GridItems/Bullets";
        static string crewPrefabPath = "Prefabs/GridItems/Crews";
        public static Sprite LoadGearImage(Gear gear)
        {
            GearType gearType = gear.GearType;
            int id = (gear.Id);
            string path = "";
            switch (gearType)
            {
                case GearType.Necklace:
                    path = necklaceGearPath;
                    break;
                case GearType.Sword:
                    path = swordGearPath;
                    break;
                case GearType.Hat:
                    path = hatGearPath;
                    break;
            }
            path += $"/{id}";
            return Resources.Load<Sprite>(path);
        }

        public static Sprite LoadCharacterImage(int id)
        {
            return Resources.Load<Sprite>(characterPath + $"/{id}");
        }

        public static Sprite LoadSkillImage(int id)
        {
            return Resources.Load<Sprite>(skillPath + $"/{id}");
        }
        public static GameObject LoadGridItemPrefab(GridItemDef def)
        {
            string path = "";
            switch (def.Type)
            {
                case ItemType.CANNON:
                    path = cannonPrefabPath;
                    break;
                case ItemType.AMMO:
                    path = bulletPrefabPath;
                    break;
                case ItemType.CREW:
                    path = crewPrefabPath;
                    break;
            }
            path += $"/{def.Name}";
            return Resources.Load<GameObject>(path);
        }

        public static Sprite LoadGridItemImage(GridItemDef def)
        {
            string path = "";
            switch (def.Type)
            {
                case ItemType.CANNON:
                    path = cannonImagePath;
                    break;
                case ItemType.AMMO:
                    path = bulletImagePath;
                    break;
            }
            path += $"/{def.Id}";
            return Resources.Load<Sprite>(path);
        }

        public static Cannon LoadCannon(string name)
        {
            Cannon ret = Resources.Load<Cannon>(cannonPrefabPath + $"/{name}");
            return ret;
        }

        static string StatsTemplatePath = "Database/StatsTemplate";
        public static ShipStatsTemplate LoadShipTemplateConfig(string id)
        {
            ShipStatsTemplate ret = Resources.Load<ShipStatsTemplate>(StatsTemplatePath + $"/Ship/{id}");
            return ret;
        }

        public static CannonStatsTemplate LoadCannonStatsTemplate(string id)
        {
            CannonStatsTemplate ret = Resources.Load<CannonStatsTemplate>(StatsTemplatePath + $"/Cannon/{id}");
            return ret;
        }


        static string shipPath = "Prefabs/Entities/Ship/Ship_0001/Ship_";
        static string enemyPath = "Prefabs/Entities/Enemies/";
        public static Ship LoadShip(string id)
        {
            return Resources.Load<Ship>($"Prefabs/Entities/Ship/Ship_{id}/Ship_{id}");
        }

        public static EnemyModel LoadEnemy(string id)
        {
            return Resources.Load<EnemyModel>($"{enemyPath}{enemyIdNameDic[id]}");
        }

        public static Dictionary<string, string> enemyIdNameDic = new Dictionary<string, string>()
        {
            { "0001", "Puffer Fish"},
            { "0002", "Electric Eel"},
            { "0003","Squid"},
            { "0004","Jelly Fish" }
        };


        public static Dictionary<string, string> cannonIdNameDic = new Dictionary<string, string>()
        {
            { "0001", "Puffer Fish"},
            { "0002", "Electric Eel"},
            { "0003","Squid"},
            { "0004","Jelly Fish" }
        };
    }
}
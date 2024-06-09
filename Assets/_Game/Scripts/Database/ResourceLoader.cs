using _Game.Scripts.Entities;
using _Game.Scripts.InventorySystem;
using UnityEngine;

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
                case GridItemType.Cannon:
                    path = cannonPrefabPath;
                    break;
                case GridItemType.Bullet:
                    path = bulletPrefabPath;
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
                case GridItemType.Cannon:
                    path = cannonImagePath;
                    break;
                case GridItemType.Bullet:
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
    }
}
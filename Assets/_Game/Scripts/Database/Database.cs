using _Game.Features.Inventory;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.DB
{
    public static class Database
    {
        private static Dictionary<string, Sprite> AmmoImageDic = new Dictionary<string, Sprite>();
        private static Dictionary<string, Sprite> CrewImageDic = new Dictionary<string, Sprite>();
        private static Dictionary<string, Sprite> CannonImageDic = new Dictionary<string, Sprite>();

        private static Dictionary<string, Cannon> CannonDic = new Dictionary<string, Cannon>();
        private static Dictionary<string, Bullet> BulletDic = new Dictionary<string, Bullet>();
        private static Dictionary<string, Crew> CrewDic = new Dictionary<string, Crew>();

        private static Dictionary<string, string> CannonOperatorDic = new Dictionary<string, string>();
        private static Dictionary<string, string> BulletOperatorDic = new Dictionary<string, string>();
        private static Dictionary<string, string> CrewOperatorDic = new Dictionary<string, string>();

        private static Dictionary<KeyValuePair<string, string>, Vector3> CannonOffsetDic = new Dictionary<KeyValuePair<string, string>, Vector3>();
        private static Dictionary<KeyValuePair<string, string>, Vector3> BulletOffsetDic = new Dictionary<KeyValuePair<string, string>, Vector3>();

        private static Dictionary<KeyValuePair<ItemType, string>, int[,]> ShapeIdDic = new Dictionary<KeyValuePair<ItemType, string>, int[,]>();
        private static Dictionary<string, float> EnemyPowerDic = new Dictionary<string, float>();


        public static void Load()
        {
            CreateImageDic();
            CreateCannonDic();
            CreateBulletDic();
            CreateCrewDic();
            CreateOffsetDic();
            CreateShapeDic();
            CreateMonsterPowerDic();

            Debug.Log(GetShapeByTypeAndOperationType("0001", ItemType.CREW) == Shape.ShapeDic[1]);
            Debug.Log(GetShapeByTypeAndOperationType("0001", ItemType.CANNON) == Shape.ShapeDic[2]);
        }

        static void CreateShapeDic()
        {
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "arrow"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "burning"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "normal"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "icy"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "culling"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "boom"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "whirlpool"), Shape.ShapeDic[0]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.AMMO, "rocket"), Shape.ShapeDic[0]);


            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "fast"), Shape.ShapeDic[2]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "charge"), Shape.ShapeDic[4]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "normal"), Shape.ShapeDic[1]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "twin"), Shape.ShapeDic[3]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "chaining"), Shape.ShapeDic[1]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CANNON, "far"), Shape.ShapeDic[3]);

            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CREW, "captain"), Shape.ShapeDic[1]);
            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CREW, "crew"), Shape.ShapeDic[1]);
        }

        static void CreateMonsterPowerDic()
        {
            foreach (var enemy in GDConfigLoader.Instance.Enemies)
            {
                EnemyPowerDic.Add(enemy.Value.id, enemy.Value.power_number);
            }
        }

        static void CreateImageDic()
        {
            foreach (var ammo in GDConfigLoader.Instance.Ammos)
            {
                string operationType = ammo.Value.operation_type;
                ItemType Type = ItemType.AMMO;
                var itemType = Type.ToString().ToLower();
                var itemOperationType = operationType.ToLower();
                var path = $"Database/GridItem/{itemType}/{itemOperationType}";
                Sprite sprite = Resources.Load<Sprite>(path);
                AmmoImageDic.Add(ammo.Value.id, sprite);
            }

            foreach (var cannon in GDConfigLoader.Instance.Cannons)
            {
                string operationType = cannon.Value.operation_type;
                ItemType Type = ItemType.CANNON;
                var itemType = Type.ToString().ToLower();
                var itemOperationType = operationType.ToLower();
                var path = $"Database/GridItem/{itemType}/{itemOperationType}";
                Sprite sprite = Resources.Load<Sprite>(path);
                CannonImageDic.Add(cannon.Value.id, sprite);
            }

            for (int i = 0; i <= 1; i++)
            {
                var rarities = Enum.GetValues(typeof(Features.Inventory.Rarity)).Cast<Features.Inventory.Rarity>();
                for (int j = 1; j < rarities.Count() + 1; j++)
                {
                    ItemType Type = ItemType.CREW;
                    string Id = (i * rarities.Count() + j).ToString("D4");
                    var itemType = Type.ToString().ToLower();
                    string operationType = i == 0 ? "Captain" : "Crew";
                    var itemOperationType = operationType.ToLower();

                    CrewOperatorDic.Add(Id, itemOperationType);
                    var path = $"Database/GridItem/{itemType}/{itemOperationType}";
                    Sprite sprite = Resources.Load<Sprite>(path);
                    CrewImageDic.Add(Id, sprite);
                }
            }
        }


        static void CreateCannonDic()
        {
            foreach (var cannon in GDConfigLoader.Instance.Cannons)
            {
                string operationType = cannon.Value.operation_type.ToLower();
                string path = $"Prefabs/GridItems/Cannons/{operationType}";
                Cannon cannonPrefab = Resources.Load<Cannon>(path);
                CannonDic.Add(cannon.Key, cannonPrefab);
                CannonOperatorDic.Add(cannon.Key, operationType);
            }
        }

        static void CreateBulletDic()
        {
            foreach (var ammo in GDConfigLoader.Instance.Ammos)
            {
                string operationType = ammo.Value.operation_type.ToLower();
                string path = $"Prefabs/GridItems/Bullets/{operationType}";
                Debug.Log(ammo.Key + " "+ operationType);
                Bullet bulletPrefab = Resources.Load<Bullet>(path);
                BulletDic.Add(ammo.Key, bulletPrefab);
                BulletOperatorDic.Add(ammo.Key, operationType);
            }
        }

        static void CreateCrewDic()
        {
            Crew crew = Resources.Load<Crew>($"Prefabs/GridItems/Crews/Captain");
            CrewDic.Add("0001", crew);
            CrewDic.Add("0002", crew);
            CrewDic.Add("0003", crew);
            CrewDic.Add("0004", crew);
            CrewDic.Add("0005", crew);
            Crew crew1 = Resources.Load<Crew>($"Prefabs/GridItems/Crews/Crew");
            CrewDic.Add("0006", crew1);
            CrewDic.Add("0007", crew1);
            CrewDic.Add("0008", crew1);
            CrewDic.Add("0009", crew1);
            CrewDic.Add("0010", crew1);
        }

        static void CreateOffsetDic()
        {
            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0001"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0001"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0001"), new Vector3(0.52f, 0.653f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0001"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0001"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0001"), new Vector3(0.48f, 0.38f, 0));

            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0002"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0002"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0002"), new Vector3(0.52f, 0.653f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0002"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0002"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0002"), new Vector3(0.48f, 0.38f, 0));

            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0003"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0003"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0003"), new Vector3(0.52f, 0.653f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0003"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0003"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0003"), new Vector3(0.48f, 0.38f, 0));


            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0001"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0001"), new Vector3(0, -0.576f, 0));

            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0002"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0002"), new Vector3(0, -0.576f, 0));

            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0003"), new Vector3(0, -0.576f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0003"), new Vector3(0, -0.576f, 0));

        }

        public static Cannon GetCannon(string id)
        {
            return CannonDic[id];
        }
        public static Bullet GetBullet(string id)
        {
            return BulletDic[id];
        }

        public static Crew GetCrew(string id)
        {
            return CrewDic[id];
        }

        public static Sprite GetAmmoImage(string id)
        {
            return AmmoImageDic[id];
        }

        public static Sprite GetCannonImage(string id)
        {
            return CannonImageDic[id];
        }

        public static Sprite GetCrewImage(string id)
        {
            return CrewImageDic[id];
        }

        public static Vector3 GetOffsetCannonWithStartCell(string cannonId, string shipId)
        {
            string opeartor = CannonOperatorDic[cannonId];
            return CannonOffsetDic[new KeyValuePair<string, string>(opeartor, shipId)];
        }

        public static Vector3 GetOffsetBulletWithStartCell(string bulletId, string shipId)
        {
            string opeartor = BulletOperatorDic[bulletId];
            return BulletOffsetDic[new KeyValuePair<string, string>(opeartor, shipId)];
        }

        public static int[,] GetShapeByTypeAndOperationType(string id, ItemType itemType)
        {
            string operationType = "";
            switch (itemType)
            {
                case ItemType.AMMO:
                    operationType = BulletOperatorDic[id];
                    break;
                case ItemType.CANNON:
                    operationType = CannonOperatorDic[id];
                    break;
                case ItemType.CREW:
                    operationType = CrewOperatorDic[id];
                    break;
            }
            return ShapeIdDic[new KeyValuePair<ItemType, string>(itemType, operationType)];
        }

        public static float GetEnemyPower(string id)
        {
            return EnemyPowerDic[id];
        }
    }
}

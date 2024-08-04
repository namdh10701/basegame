using _Game.Features.Gameplay;
using _Game.Features.Inventory;
using _Game.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Scripts.GD.DataManager;
using Online.Enum;
using Online.Model;
using UnityEngine;

namespace _Game.Scripts.DB
{
    public static class Database
    {
        private static Dictionary<string, Carpet> CarpetDic = new Dictionary<string, Carpet>();
        private static Dictionary<string, Cannon> CannonDic = new Dictionary<string, Cannon>();
        private static Dictionary<string, Ammo> BulletDic = new Dictionary<string, Ammo>();
        private static Dictionary<string, Crew> CrewDic = new Dictionary<string, Crew>();
        private static Dictionary<string, UICrew> CrewUIDic = new Dictionary<string, UICrew>();

        private static Dictionary<string, string> CannonOperatorDic = new Dictionary<string, string>();
        private static Dictionary<string, string> BulletOperatorDic = new Dictionary<string, string>();
        private static Dictionary<string, string> CrewOperatorDic = new Dictionary<string, string>();
        private static Dictionary<string, string> CarpetOperatorDic = new Dictionary<string, string>();

        private static Dictionary<KeyValuePair<string, string>, Vector3> CannonOffsetDic = new Dictionary<KeyValuePair<string, string>, Vector3>();
        private static Dictionary<KeyValuePair<string, string>, Vector3> BulletOffsetDic = new Dictionary<KeyValuePair<string, string>, Vector3>();
        private static Dictionary<KeyValuePair<string, string>, Vector3> CarpetOffsetDic = new Dictionary<KeyValuePair<string, string>, Vector3>();


        private static Dictionary<KeyValuePair<ItemType, string>, int[,]> ShapeIdDic = new Dictionary<KeyValuePair<ItemType, string>, int[,]>();
        private static Dictionary<string, float> EnemyPowerDic = new Dictionary<string, float>();


        public static void Load()
        {
            CreateImageDic();
            CreateCannonDic();
            CreateBulletDic();
            CreateCrewDic();
            CreateCrewUIDic();
            CreateOffsetDic();
            CreateShapeDic();
            CreateMonsterPowerDic();
            CreateCarpetDic();
        }
        static void CreateCarpetDic()
        {
            string path = $"Prefabs/GridItems/Carpets/1x2Vertical";
            Carpet cannonPrefab = Resources.Load<Carpet>(path);
            CarpetDic.Add("0001", cannonPrefab);
            CarpetOperatorDic.Add("0001", "1x2Vertical");

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



            ShapeIdDic.Add(new KeyValuePair<ItemType, string>(ItemType.CARPET, "1x2Vertical"), Shape.ShapeDic[1]);
        }

        static void CreateMonsterPowerDic()
        {
            foreach (var enemy in GameData.MonsterTable.Records)
            {
                EnemyPowerDic.Add(enemy.Id, enemy.PowerNumber);

            }
            EnemyPowerDic.Add("9999", 500);
        }

        static void CreateImageDic()
        {
            for (var i = 0; i <= 1; i++)
            {
                var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>();
                for (var j = 1; j < rarities.Count() + 1; j++)
                {
                    var Type = ItemType.CREW;
                    var Id = (i * rarities.Count() + j).ToString("D4");
                    var itemType = Type.ToString().ToLower();
                    var operationType = i == 0 ? "Captain" : "Crew";
                    var itemOperationType = operationType.ToLower();

                    CrewOperatorDic.Add(Id, itemOperationType);
                }
            }
        }


        static void CreateCannonDic()
        {
            foreach (var cannon in GameData.CannonTable.Records)
            {
                var operationType = cannon.OperationType.ToLower();
                var path = $"Prefabs/GridItems/Cannons/{operationType}";
                var cannonPrefab = Resources.Load<Cannon>(path);
                CannonDic.Add(cannon.Id, cannonPrefab);
                CannonOperatorDic.Add(cannon.Id, operationType);
            }
        }

        static void CreateBulletDic()
        {
            foreach (var ammo in GameData.AmmoTable.Records)
            {
                var operationType = ammo.OperationType.ToLower();
                var path = $"Prefabs/GridItems/Ammos/{operationType}";
                var bulletPrefab = Resources.Load<Ammo>(path);
                BulletDic.Add(ammo.Id, bulletPrefab);
                BulletOperatorDic.Add(ammo.Id, operationType);
            }
        }

        static void CreateCrewDic()
        {
            foreach (var rec in GameData.CrewTable.Records)
            {
                var resPath = $"Prefabs/GridItems/Crews/{rec.OperationType}";
                var crew = Resources.Load<Crew>(resPath);
                CrewDic.Add(rec.Id, crew);
            }
        }

        static void CreateCrewUIDic()
        {
            foreach (var rec in GameData.CrewTable.Records)
            {
                var resPath = $"Prefabs/GridItems/CrewsUI/{rec.OperationType}";
                UICrew crew = Resources.Load<UICrew>(resPath);
                CrewUIDic.Add(rec.Id, crew);
            }
        }

        static void CreateOffsetDic()
        {
            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0001"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0001"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0001"), new Vector3(0.52f, 0.5f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0001"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0001"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0001"), new Vector3(0.48f, 0.38f, 0));

            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0002"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0002"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0002"), new Vector3(0.52f, 0.5f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0002"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0002"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0002"), new Vector3(0.48f, 0.38f, 0));

            CannonOffsetDic.Add(new KeyValuePair<string, string>("normal", "0003"), new Vector3(0, 0.41f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("fast", "0003"), new Vector3(0, 0.45f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("charge", "0003"), new Vector3(0.52f, 0.5f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("twin", "0003"), new Vector3(0.52f, 0.33f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("chaining", "0003"), new Vector3(0, 0.27f, 0));
            CannonOffsetDic.Add(new KeyValuePair<string, string>("far", "0003"), new Vector3(0.48f, 0.38f, 0));


            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0001"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0001"), new Vector3(0, -0.41f, 0));

            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0002"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0002"), new Vector3(0, -0.41f, 0));

            BulletOffsetDic.Add(new KeyValuePair<string, string>("arrow", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("burning", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("normal", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("icy", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("culling", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("boom", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("whirlpool", "0003"), new Vector3(0, -0.41f, 0));
            BulletOffsetDic.Add(new KeyValuePair<string, string>("rocket", "0003"), new Vector3(0, -0.41f, 0));


            CarpetOffsetDic.Add(new KeyValuePair<string, string>("1x2Vertical", "0001"), new Vector3(0, 0.41f, 0));

            CarpetOffsetDic.Add(new KeyValuePair<string, string>("1x2Vertical", "0002"), new Vector3(0, 0.41f, 0));

            CarpetOffsetDic.Add(new KeyValuePair<string, string>("1x2Vertical", "0003"), new Vector3(0, 0.41f, 0));
        }

        public static Cannon GetCannon(string id)
        {
            return CannonDic[id];
        }
        public static Ammo GetBullet(string id)
        {
            return BulletDic[id];
        }
        public static Carpet GetCarpet(string id)
        {
            return CarpetDic[id];
        }

        public static Crew GetCrew(string id)
        {
            return CrewDic[id];
        }
        public static UICrew GetUICrew(string id)
        {
            return CrewUIDic[id];
        }

        private static Sprite GetGridItemImage(ItemType itemType, string itemOperationType, Rarity itemRarity)
        {
            var path = $"Images/Items/item_{itemType}_{itemOperationType}_{itemRarity.ToString().ToLower()}";
            // var path = $"Database/GridItem/{itemType}/{itemOperationType}";
            return CachedResources.Load<Sprite>(path);
        }

        public static Sprite GetAmmoImage(string id)
        {
            const ItemType type = ItemType.AMMO;
            var record = GameData.AmmoTable.FindById(id);
            return GetGridItemImage(type, record.OperationType, record.Rarity);
        }
        public static Sprite GetAmmoImageShipHUD(string id)
        {
            var record = GameData.AmmoTable.FindById(id);
            return CachedResources.Load<Sprite>($"Database/GridItem/ammo/{record.OperationType}");
        }
        public static Sprite GetCannonImage(string id)
        {
            const ItemType type = ItemType.CANNON;
            var record = GameData.CannonTable.FindById(id);
            return GetGridItemImage(type, record.OperationType, record.Rarity);
        }

        public static Sprite GetCrewImage(string id)
        {
            const ItemType type = ItemType.CREW;
            var record = GameData.CrewTable.FindById(id);
            return GetGridItemImage(type, record.OperationType, record.Rarity);
        }
        
        public static Sprite GetShipImage(string id) 
            => CachedResources.Load<Sprite>($"Images/Items/item_ship_{id}");
        
        public static Sprite GetResource(string id)
            => CachedResources.Load<Sprite>($"Images/Items/item_{id}");

        public static Sprite GetRankingTierBadge(ERank rank) => CachedResources.Load<Sprite>($"Images/Rank/rank_badge_{rank.ToString().ToLower()}");
        
        public static Sprite GetItemSprite(ItemType itemType, string id)
        {
            switch (itemType)
            {
                case ItemType.SHIP: return GetShipImage(id);
                case ItemType.AMMO: return GetAmmoImage(id);
                case ItemType.CANNON: return GetCannonImage(id);
                case ItemType.CREW: return GetCrewImage(id);
                case ItemType.MISC: return GetResource(id);
            }

            return null;
        }

        public static Vector3 GetOffsetCarpetWithStartCell(string cannonId, string shipId)
        {
            string opeartor = CarpetOperatorDic[cannonId];
            return CarpetOffsetDic[new KeyValuePair<string, string>(opeartor, shipId)];
        }

        public static Vector3 GetOffsetCannonWithStartCell(string cannonId, string shipId)
        {
            var opeartor = CannonOperatorDic[cannonId];
            return CannonOffsetDic[new KeyValuePair<string, string>(opeartor, shipId)];
        }

        public static Vector3 GetOffsetBulletWithStartCell(string bulletId, string shipId)
        {
            var opeartor = BulletOperatorDic[bulletId];
            return BulletOffsetDic[new KeyValuePair<string, string>(opeartor, shipId)];
        }

        public static int[,] GetShapeByTypeAndOperationType(string id, ItemType itemType)
        {
            var operationType = "";
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
                case ItemType.CARPET:
                    operationType = CarpetOperatorDic[id];
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

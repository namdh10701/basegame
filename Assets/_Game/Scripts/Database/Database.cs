using _Base.Scripts.Database;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.InventorySystem;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Database
{
    public static class Database
    {
        //Database<Gear> Gear = new Database<Gear>(gearPath);

                                            //id    ,operation type ->  sprite
        public static Dictionary<KeyValuePair<string, string>,  Sprite> ImageDic = new Dictionary<KeyValuePair<string, string>, Sprite>();
        public static Dictionary<KeyValuePair<string, string>, Cannon> CannonDic = new Dictionary<KeyValuePair<string, string>, Cannon>();
        public static Dictionary<KeyValuePair<string, string>, Bullet> BulletDic = new Dictionary<KeyValuePair<string, string>, Bullet>();
        public static Dictionary<KeyValuePair<string, string>, Crew> CrewDic = new Dictionary<KeyValuePair<string, string>, Crew>();

        public static Sprite arrowBullet;
        public static void Load()
        {
            CreateImageDic();
            CreateCannonDic();
            CreateBulletDic();
            CreateCrewDic();
        }

        static void CreateImageDic()
        {
            
        }

        static void CreateCannonDic()
        {
            //CannonDic.Add("0001", Resources.Load<Cannon>("fast"));
        }

        static void CreateBulletDic()
        {

        }

        static void CreateCrewDic()
        {

        }
    }
}

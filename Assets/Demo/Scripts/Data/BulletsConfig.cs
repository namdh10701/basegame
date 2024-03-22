using System;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Data
{
    [CreateAssetMenu(fileName = "BulletsConfig", menuName = "BulletsConfig/Data", order = 1)]
    public class BulletsConfig : ScriptableObject
    {
        public List<BulletData> BulletsData;
    }


    [Serializable]
    public class BulletData
    {
        public int Id;
        public Sprite Sprite;
    }
}
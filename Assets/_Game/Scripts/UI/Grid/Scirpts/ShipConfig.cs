using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ship Info")]
    public class ShipConfig : ScriptableObject
    {
        public List<ShipInfor> ships = new List<ShipInfor>();
    }

    [Serializable]
    public class ShipInfor
    {
        public string id;
        public GameObject ship;

    }

}

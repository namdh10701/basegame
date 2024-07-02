using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ship Seletion Info")]
    public class ShipSeletionInfoConfig : ScriptableObject
    {
        public List<ShipSeletionInfo> ships = new List<ShipSeletionInfo>();
    }

    [Serializable]
    public class ShipSeletionInfo
    {
        public string id;
        public bool isApprove;

    }

}

using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ship Seletion Info")]
    public class ShipSelectionInfoConfig : ScriptableObject
    {
        public List<ShipSelectionInfo> shipSelectionInfo = new List<ShipSelectionInfo>();
    }

    [Serializable]
    public class ShipSelectionInfo
    {
        public string id;
        public bool isApprove;

    }

}

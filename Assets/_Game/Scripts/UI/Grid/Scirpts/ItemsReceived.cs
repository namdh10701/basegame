using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ItemsReceived")]
    public class ItemsReceived : ScriptableObject
    {
        public List<InventoryItemInfo> inventoryItemsInfo = new List<InventoryItemInfo>();
    }
}

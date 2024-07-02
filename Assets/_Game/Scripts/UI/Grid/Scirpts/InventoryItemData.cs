using System.Collections;
using System.Collections.Generic;
using _Game.Scripts;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public int startX;
        public int startY;
        public string gridID;
        public Vector2 position;
        public GridItemDef gridItemDef;
    }

    public enum OperationType
    {
        None = 0,
        normal = 1,
        bouncing = 1,
        charge = 1,
        fast = 2,
        twin = 3
    }
}

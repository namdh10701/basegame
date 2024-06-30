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

    public enum InventoryItemType
    {
        None = 0,
        Gun = 1,
        Bullet = 2,
        Crew
    }
}

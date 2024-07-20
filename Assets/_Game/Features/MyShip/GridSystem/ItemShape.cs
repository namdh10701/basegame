using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.MyShip.GridSystem
{
    [CreateAssetMenu(fileName = "InventoryItemPattern", menuName = "SO/Inventory Item Pattern", order = 1)]
    public class ItemShape: ScriptableObject
    {
        public List<Vector2Int> Data;
        // public ItemPattern(params Vector2Int[] points)
        // {
        //     AddRange(points);
        // }

        public static ItemShape Load(string shapeId) => Resources.Load<ItemShape>($"ItemShapes/ItemShape_{shapeId}");
    }
}

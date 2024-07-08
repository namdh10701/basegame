using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.MyShip.GridSystem
{
    public class ItemPattern: List<Vector2Int>
    {
        public ItemPattern(params Vector2Int[] points)
        {
            AddRange(points);
        }
    }
}

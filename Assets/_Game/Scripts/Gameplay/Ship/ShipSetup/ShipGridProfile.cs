using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ship Grid Profile")]
    public class ShipGridProfile : ScriptableObject
    {
        public GridDefinition[] GridDefinitions;
    }

    [System.Serializable]
    public struct GridDefinition
    {
        public string Id;
        public Vector2 rootPosition;
        public int Col;
        public int Row;
        public Vector2 Spaces;
        public Cell CellPrefab;
    }
}
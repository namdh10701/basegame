using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Item Definition")]
    public class GridItemDef : ScriptableObject
    {
        public string Id;
        public GridItemType Type;
        public int ShapeId;
        public string Name;
        public override string ToString()
        {
            return Id + " " + Type.ToString() + " " + ShapeId;
        }
    }
}
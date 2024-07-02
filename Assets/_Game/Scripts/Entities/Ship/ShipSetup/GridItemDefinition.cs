using System.Collections;
using System.Collections.Generic;
using _Game.Features.Inventory;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Item Definition")]
    public class GridItemDef : ScriptableObject
    {
        public string Id;
        public ItemType Type;
        public int[,] Shape;
        public string Name;
        public string Path;
        public Sprite Image;
        public Sprite ProjectileImage;
    }
}
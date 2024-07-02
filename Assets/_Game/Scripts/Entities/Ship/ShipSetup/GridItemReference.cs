using _Game.Scripts.Entities;
using UnityEngine;
namespace _Game.Scripts
{
    [System.Serializable]
    public struct GridItemReference
    {
        public Sprite Image;
        public IGridItem Prefab;
        //TODO
        public bool Selected;
        public override bool Equals(object obj)
        {
            return Image == ((GridItemReference)obj).Image && Prefab == ((GridItemReference)obj).Prefab;
        }

    }
}

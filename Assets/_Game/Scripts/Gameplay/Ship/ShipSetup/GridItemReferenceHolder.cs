using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Item Reference Holder")]
    public class GridItemReferenceHolder : ScriptableObject
    {
        public GridItemDef[] CannonReferences;

        public GridItemDef[] BulletReferences;

        public GridItemDef[] CrewReferences;

    }
}
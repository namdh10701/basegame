using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Item Reference Holder")]
    public class GridItemReferenceHolder : ScriptableObject
    {
        public GridItemReference[] CannonReferences;

        public GridItemReference[] BulletReferences;

        public GridItemReference[] CrewReferences;

        public GridItem GetItemByIdAndType(string id, GridItemType gridItemType)
        {
            switch (gridItemType)
            {
                case GridItemType.Cannon:
                    return LookForItemId(id, CannonReferences);
                case GridItemType.Bullet:
                    return LookForItemId(id, BulletReferences);
                case GridItemType.Crew:
                    return LookForItemId(id, CrewReferences);
            }
            return null;
        }

        GridItem LookForItemId(string id, GridItemReference[] references)
        {
            foreach (var item in references)
            {
                if (item.Prefab.Def.Id == id)
                {
                    return item.Prefab;
                }
            }
            return null;
        }
    }
}
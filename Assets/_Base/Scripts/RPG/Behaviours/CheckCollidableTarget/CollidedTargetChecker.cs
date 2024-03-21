using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.CheckCollidableTarget
{
    public abstract class CollidedTargetChecker: MonoBehaviour
    {
        public abstract bool Check(Collider2D collision);
    }
}
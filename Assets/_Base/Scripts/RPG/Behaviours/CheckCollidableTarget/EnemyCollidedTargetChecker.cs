using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.CheckCollidableTarget
{
    public class EnemyCollidedTargetChecker: CollidedTargetChecker
    {
        public override bool Check(Collider2D collision)
        {
            return collision.gameObject.TryGetComponent<_Game.Scripts.Enemy>(out var tmp);
        }
    }
}
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("My Services/Update Is In Cooldown")]
    public class UpdateIsInCooldown : Service
    {
        public BoolReference isInCooldown;
        public EnemyReference enemyReference;
        public override void Task()
        {
            isInCooldown.Value = !enemyReference.Value.IsInCooldown();
        }
    }
}

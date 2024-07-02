using _Game.Scripts;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("My Services/Update Ready To Attack")]
    public class UpdateReadyToAttack : Service
    {
        public EnemyReference Enemy;
        public BoolReference IsReadyToAttack;

        public override void Task()
        {
            IsReadyToAttack.Value = Enemy.Value.IsReadyToAttack();
        }
    }
}

using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("UPDAte");
            IsReadyToAttack.Value = Enemy.Value.IsReadyToAttack();
        }
    }
}

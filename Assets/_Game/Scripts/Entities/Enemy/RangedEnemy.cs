using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class RangedEnemy : Enemy
    {
        [SerializeField] RangedAttack RangedAttack;

        protected override void Start()
        {
            base.Start();
            Area moveArea = GameObject.Find("MoveArea").GetComponent<Area>();
            _blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea;
        }
    }
}

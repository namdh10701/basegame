using _Base.Scripts.RPG.Effects;
using MBT;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class Shark : Enemy
    {
        [SerializeField] HitFx hitFx;
        [SerializeField] private float frequency;
        [SerializeField] private float amplitude;
        [SerializeField] private float modifier;

        
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {

            if (!IsPlayerInRange)
            {
                body.velocity = Mathf.Clamp((Mathf.Cos(Time.time * frequency) * amplitude + modifier), 0, 10000) * Vector2.down;
            }
            else
            {
                body.velocity = Vector2.zero;
            }

            base.Update();


        }

        public override void DoTarget()
        {
            targetCells.Clear();
            targetCells = gridPicker.PickCells(transform, CellPickType.ClosetCell, cellPattern, 2, out centerCell);
            gridAttackHandler.PlayTargetingFx(targetCells);
        }

        public override void DoAttack()
        {
            base.DoAttack();
            gridAttackHandler.ProcessAttack(targetCells, new DecreaseHealthEffect(5));
            Cooldown.StartCooldown();
        }


    }
}
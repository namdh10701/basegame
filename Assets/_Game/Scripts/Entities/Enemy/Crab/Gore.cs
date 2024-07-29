using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Utils;
using BehaviorDesigner.Runtime.Tasks;
using MBT;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Crab/Gore")]

    public class Gore : Leaf
    {
        public Crab crab;
        public CrabView crabView;
        public ShipReference shipReference;
        public StatReference statReference;
        bool isImpacted;
        Vector2 targetPos;
        public float multiplier;
        public ObjectCollisionDetector impactCollider;
        public CameraShake cameraShake;

        public override void OnEnter()
        {
            base.OnEnter();
            impactCollider.OnObjectCollisionEnter += ImpactCollider_OnObjectCollisionEnter;
            targetPos = shipReference.Value.ShipBound.ClosetPointTo(crab.Body.position);
        }

        private void ImpactCollider_OnObjectCollisionEnter(GameObject obj)
        {
            if (obj.TryGetComponent(out EffectTakerCollider taker))
            {
                if (taker.Taker is Ship ship)
                {
                    isImpacted = true;
                    cameraShake.Shake(.2f, new Vector3(.2f, .2f, .2f));

                    Cell cell = crab.gridPicker.PickRandomCell();
                    Cell cell2 = crab.gridPicker.PickRandomCell();
                    Cell cell3 = crab.gridPicker.PickRandomCell();
                    EnemyAttackData enemyAttackData = new EnemyAttackData();
                    enemyAttackData.TargetCells = new List<Cell>() { cell, cell2, cell3 };
                    enemyAttackData.CenterCell = cell;

                    Debug.LogError(cell.ToString());

                    DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
                    decreaseHp.Amount = crab._stats.AttackDamage.Value;
                    decreaseHp.ChanceAffectCell = 1;
                    decreaseHp.transform.position = cell.transform.position;
                    enemyAttackData.Effects = new List<Effect> { decreaseHp };
                    crab.atkHandler.ProcessAttack(enemyAttackData);
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            isImpacted = false;
            impactCollider.OnObjectCollisionEnter -= ImpactCollider_OnObjectCollisionEnter;
            crab.CrabState = CrabState.Idle;
        }


        public override NodeResult Execute()
        {
            if (!isImpacted)
            {
                Vector2 direction = (targetPos - crab.Body.position).normalized;
                crab.Body.AddForce(statReference.Value.Value * direction * multiplier);
            }
            return isImpacted ? NodeResult.success : NodeResult.running;
        }
    }
}
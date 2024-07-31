
using _Game.Features.Gameplay;
using DG.Tweening;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts
{
    public class PufferFishView : EnemyView
    {
        protected override IEnumerator EntryVisualize()
        {
            //transform.localScale = Vector3.zero;
            Debug.Log("ENTRY PUFFER FIZZ");
            Tween tween = enemyModel.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
            yield return tween.WaitForCompletion();
            yield return base.EntryVisualize();
        }

        public override void Initialize(EnemyModel enemyModel)
        {
            base.Initialize(enemyModel);
            PufferFishModel pufferFishModel = enemyModel as PufferFishModel;
            pufferFishModel.PufferFishMove.OnFast += PlayMoveFast;
            enemyModel.OnChargeStateStateEntered += ChargeStateEntered;
        }

        private void ChargeStateEntered(ChargeState state)
        {
            if (state == ChargeState.Charging)
            {
                ChargeExplode();
            }
        }

        public void ChargeExplode()
        {
            skeletonAnim.AnimationState.SetAnimation(0, "bomb_ship_transform", false);
            skeletonAnim.AnimationState.AddAnimation(0, "bomb_ship_loop", true, 0);
        }

        public void PlayMoveFast()
        {
            skeletonAnim.AnimationState.SetAnimation(0, "move_fast", false);
            skeletonAnim.AnimationState.AddAnimation(0, "idle_move", true, 0);
        }
    }
}
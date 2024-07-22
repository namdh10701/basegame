using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SpawnPartView : PartView
    {
        public Action attackDone;
        public override void HandleIdleEnter()
        {
        }
        protected override void OnStateEntered(PartState state)
        {
            Debug.Log(state);
            base.OnStateEntered(state);
            if (state == PartState.Attacking)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        IEnumerator AttackCoroutine()
        {
            meshRenderer.enabled = true;
            skeletonAnim.AnimationState.SetAnimation(0, attack, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
            attackDone?.Invoke();
        }
        protected override IEnumerator HideCoroutine()
        {
            meshRenderer.enabled = false;
            skeletonAnim.AnimationState.ClearTracks();
            yield break;
        }
        public override void PlayEntry()
        {
            skeletonAnim.AnimationState.ClearTracks();
            base.PlayEntry();
        }
    }
}
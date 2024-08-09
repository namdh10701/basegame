using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CrabView : EnemyView
    {
        [SpineAnimation] public string DefenseBegin;
        [SpineAnimation] public string DefenseCharge;
        [SpineAnimation] public string DefenseToGore;
        [SpineAnimation] public string GoreLoop;
        [SpineAnimation] public string GoreImpact;

        [SpineAnimation] public string StunBegin;
        [SpineAnimation] public string StunLoop;
        [SpineAnimation] public string StunToIdle;

        public Action OnDefenseEntered;
        public Action OnDefenseExit;
        public CrabState lastCrabState;


        public override void Initialize(EnemyModel enemyModel)
        {
            base.Initialize(enemyModel);
            Crab crab = enemyModel as Crab;
            crab.OnCrabStateEntered += OnCrabStateEntered;
            skeletonAnim.AnimationState.Start += AnimationState_Start;
        }

        private void AnimationState_Start(Spine.TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == DefenseCharge)
            {
                OnDefenseEntered?.Invoke();
            }
            if (trackEntry.Animation.Name == Idle || trackEntry.Animation.Name == Moving || trackEntry.Animation.Name == attack)
            {
                OnDefenseExit?.Invoke();
            }
        }

        private void OnCrabStateEntered(CrabState state)
        {
            switch (state)
            {
                case CrabState.Defense:
                    skeletonAnim.AnimationState.SetAnimation(0, DefenseBegin, false);
                    skeletonAnim.AnimationState.AddAnimation(0, DefenseCharge, true, 0);
                    break;
                case CrabState.Gore:
                    skeletonAnim.AnimationState.SetAnimation(0, DefenseToGore, false);
                    skeletonAnim.AnimationState.AddAnimation(0, GoreLoop, true, 0);
                    break;
                case CrabState.Idle:
                    if (lastCrabState == CrabState.Gore)
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, GoreImpact, false);
                        skeletonAnim.AnimationState.AddAnimation(0, Idle, false, 0);
                    }
                    if (lastCrabState == CrabState.Stun)
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, StunToIdle, false);
                        skeletonAnim.AnimationState.AddAnimation(0, Idle, true, 0);
                    }
                    break;
                case CrabState.Move:
                    if (lastCrabState == CrabState.Defense)
                        return;
                    skeletonAnim.AnimationState.AddAnimation(0, Moving, true, 0);

                    break;
                case CrabState.Attack:
                    skeletonAnim.AnimationState.SetAnimation(0, Attacking, false);
                    skeletonAnim.AnimationState.AddAnimation(0, Idle, false, 0);
                    break;
                case CrabState.Stun:
                    stunFx.Play();
                    skeletonAnim.AnimationState.SetAnimation(0, StunBegin, false);
                    skeletonAnim.AnimationState.AddAnimation(0, StunLoop, true, 0);
                    break;
            }
            lastCrabState = state;
            if (state != CrabState.Stun)
            {
                stunFx.Stop();
            }
        }
    }

}
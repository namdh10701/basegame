using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BodyPartView : PartView
    {
        [Header("normal")]
        [SpineAnimation] public string startAttack_Norm;
        [SpineAnimation] public string charging_Norm;
        [SpineAnimation] public string chargingToAttack_Norm;
        [SpineAnimation] public string attackToIdle_Norm;
        [SpineAnimation] public string shake_Norm;
        [SpineAnimation] public string shakeToIdle_Norm;

        [SpineAnimation] public string stunBegin_Norm;
        [SpineAnimation] public string stunLoop_Norm;
        [SpineAnimation] public string stunToIdle_Norm;


        [Header("mad")]
        [SpineAnimation] public string idle_Mad;
        [SpineAnimation] public string startAttack_Mad;
        [SpineAnimation] public string charging_Mad;
        [SpineAnimation] public string chargingToAttack_Mad;
        [SpineAnimation] public string attackToIdle_Mad;
        [SpineAnimation] public string shake_Mad;
        [SpineAnimation] public string shakeToIdle_Mad;
        [SpineAnimation] public string stunBegin_Mad;
        [SpineAnimation] public string stunLoop_Mad;
        [SpineAnimation] public string stunToIdle_Mad;

        string startAttack;
        string charging;
        string chargingToAttack;
        string attackToIdle;
        string shake;
        string stunBegin;
        string stunLoop;
        string stunToIdle;


        [Header("mix")]
        [SpineAnimation] public string aim;
        [SerializeField] GameObject aimFK;

        public Action StunEnded;
        public override void Initnialize(PartModel partModel)
        {
            base.Initnialize(partModel);
            skeletonAnim.AnimationState.Start += AnimationState_Start;
            skeletonAnim.AnimationState.End += AnimationState_End;
            stunToIdle = stunToIdle_Norm;
            startAttack = attackToIdle_Norm;
            charging = charging_Norm;
            chargingToAttack = chargingToAttack_Norm;
            attackToIdle = attackToIdle_Norm;
            shake = shake_Norm;
            stunBegin = stunBegin_Norm;
            stunLoop = stunLoop_Norm;

        }



        protected override IEnumerator TransformCoroutine()
        {
            SwapAnimation();
            yield return base.TransformCoroutine();
        }

        void SwapAnimation()
        {
            idle = idle_Mad;
            startAttack = attackToIdle_Mad;
            charging = charging_Mad;
            chargingToAttack = chargingToAttack_Mad;
            attackToIdle = attackToIdle_Mad;
            shake = shake_Mad;
            stunBegin = stunBegin_Mad;
            stunLoop = stunLoop_Mad;
            stunToIdle = stunToIdle_Mad;
        }

        private void AnimationState_End(Spine.TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == shake && lastState == PartState.Attacking)
            {
                skeletonAnim.AnimationState.ClearTrack(1);
                aimFK.SetActive(false);
            }
        }

        private void AnimationState_Start(Spine.TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == shake && lastState == PartState.Attacking)
            {
                skeletonAnim.AnimationState.SetAnimation(1, aim, true);
                aimFK.SetActive(true);
                OnAttack?.Invoke();
            }
            if (trackEntry.Animation.Name == stunToIdle)
            {
                StunEnded?.Invoke();
            }
        }

        protected override void OnStateEntered(PartState state)
        {
            base.OnStateEntered(state);
            if (state == PartState.Attacking)
            {
                skeletonAnim.AnimationState.SetAnimation(0, startAttack_Norm, false);
                skeletonAnim.AnimationState.AddAnimation(0, charging_Norm, true, 0);
                skeletonAnim.AnimationState.AddAnimation(0, chargingToAttack_Norm, false, 2);
                skeletonAnim.AnimationState.AddAnimation(0, shake_Norm, true, 0);
                skeletonAnim.AnimationState.AddAnimation(1, aim, true, 3);
            }

        }

        public override void HandleIdleEnter()
        {
            if (lastState == PartState.Stunning)
            {
                skeletonAnim.AnimationState.SetAnimation(0, stunToIdle, false);
                skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
            }
            else
            {
                skeletonAnim.AnimationState.SetAnimation(0, idle, true);
            }
        }

        public override void StunAnim()
        {
            skeletonAnim.AnimationState.SetAnimation(0, stunBegin_Norm, false);
            skeletonAnim.AnimationState.AddAnimation(0, stunLoop_Norm, true, 0);
        }

        public override void PlayEntry()
        {
            gameObject.SetActive(true);
            skeletonAnim.AnimationState.SetAnimation(0, entry, false);
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
        }

        public void PlayShake()
        {
            skeletonAnim.AnimationState.SetAnimation(0, shake_Mad, true);
        }

        public void StopShake()
        {
            skeletonAnim.AnimationState.SetAnimation(0, shakeToIdle_Norm, true);
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
        }
    }
}
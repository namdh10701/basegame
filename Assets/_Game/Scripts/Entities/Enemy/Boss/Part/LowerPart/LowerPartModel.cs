using _Game.Features.Gameplay;
using _Game.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class LowerPartModel : PartModel
    {
        LowerPartView lowerPartView;
        public CameraShake cameraShake;

        public System.Action OnAttack;
        public bool isGrabbing;
        public override void OnEnterState()
        {
            base.OnEnterState();
            isGrabbing = false;
        }
        public override void Active()
        {
            base.Active();
            lowerPartView = partView as LowerPartView;
            State = PartState.Attacking;
        }

        public override void DoAttack()
        {
            base.DoAttack();
            isGrabbing = true;
            cameraShake.Shake(.1f, new Vector3(.1f, .1f, .1f));
            OnAttack?.Invoke();
        }

        public override void AfterStun()
        {
            if (lastpartState == PartState.Attacking)
            {
                State = PartState.Hidding;
            }
        }
    }
}
using _Game.Features.Gameplay;
using _Game.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BehindPartModel : PartModel
    {
        BehindPartView lowerPartView;
        public Transform shootPos;

        public System.Action OnAttack;

        public override void DoAttack()
        {
            base.DoAttack();
            OnAttack?.Invoke();
            State = PartState.Idle;
        }
    }
}
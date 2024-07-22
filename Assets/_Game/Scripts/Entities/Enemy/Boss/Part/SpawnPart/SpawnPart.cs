using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SpawnPart : PartModel
    {
        public Action OnAttackDone;
        public override void Initialize(GiantOctopus giantOctopus)
        {
            base.Initialize(giantOctopus);
            SpawnPartView spartView = partView as SpawnPartView;
            spartView.attackDone += OnAttackEnded;
        }
        public override void DoAttack()
        {
            base.DoAttack();
        }


        public void OnAttackEnded()
        {
            OnAttackDone?.Invoke();
        }
    }
}
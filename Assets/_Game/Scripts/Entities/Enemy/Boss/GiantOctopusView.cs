using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GiantOctopusView : MonoBehaviour
    {
        GiantOctopus giantOctopus;
        public void Initialize(GiantOctopus giantOctopus)
        {
            this.giantOctopus = giantOctopus;
            giantOctopus.OnStateEntered += OnStateEntered;
        }

        private void OnStateEntered(GiantOctopus.OctopusState state)
        {

        }
    }
}
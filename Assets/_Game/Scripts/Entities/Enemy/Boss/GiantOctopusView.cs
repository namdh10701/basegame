using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GiantOctopusView : MonoBehaviour
    {
        GiantOctopus giantOctopus;
        OctopusState lastState;

        PartView body;
        PartView behindLeft;
        PartView behindRight;
        PartView upperLeft;
        PartView upperRight;
        PartView lowerLeft;
        PartView lowerRight;



        public void Initialize(GiantOctopus giantOctopus)
        {
            this.giantOctopus = giantOctopus;
            giantOctopus.OnStateEntered += OnStateEntered;
        }

        private void OnStateEntered(OctopusState state)
        {
            if (lastState == state)
            {
                return;
            }

            switch (state)
            {
                case OctopusState.Entry:
                    break;
                case OctopusState.State1:
                    break;
                case OctopusState.State2:
                    break;
            }


        }
    }
}
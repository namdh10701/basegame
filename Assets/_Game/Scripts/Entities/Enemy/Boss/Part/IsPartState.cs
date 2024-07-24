using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Giant Octopus/Is Part State")]
    public class IsPartState : Condition
    {
        public PartState state;
        public PartModel part;
        public override bool Check()
        {
            return state == part.State;
        }

    }
}

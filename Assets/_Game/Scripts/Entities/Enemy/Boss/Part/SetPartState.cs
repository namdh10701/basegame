using _Game.Scripts;
using MBT;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Giant Octopus/Set Part State")]
    public class SetPartState : Leaf
    {
        public PartState state;
        public PartModel part;
        public override NodeResult Execute()
        {
            part.State = state;
            return NodeResult.success;
        }

    }
}
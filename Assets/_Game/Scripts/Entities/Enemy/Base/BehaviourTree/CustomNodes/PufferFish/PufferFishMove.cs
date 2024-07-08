using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using MBT;
using UnityEngine;
namespace _Game.MBT
{
    [MBTNode("Puffer Fish/Move")]
    [AddComponentMenu("")]
    public class PufferFishMove : Leaf
    {
        public PufferFishController PufferFish;
        public override NodeResult Execute()
        {
            PufferFish.Move();
            return NodeResult.success;
        }
    }
}

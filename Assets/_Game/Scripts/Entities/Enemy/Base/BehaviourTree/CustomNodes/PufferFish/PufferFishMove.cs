using _Game.Scripts.Entities;
using MBT;
using UnityEngine;
[MBTNode("Puffer Fish/Move")]
[AddComponentMenu("")]
public class PufferFishMove : Leaf
{
    public PufferFish PufferFish;
    public override NodeResult Execute()
    {
        PufferFish.Move();
        return NodeResult.success;
    }
}

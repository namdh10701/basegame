using _Game.Features.Gameplay;
using _Game.Scripts;
using MBT;
using UnityEngine;
using static MBT.DistanceCondition;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Crab/Distance Condition")]
    public class DistanceToShip : Condition
    {
        public Crab crab;
        public ShipReference shipReference;
        public float distance;


        public Comparator comparator = Comparator.GreaterThan;

        public override bool Check()
        {
            // Squared magnitude is enough to compare distances
            float sqrMagnitude = (crab.Body.position - (Vector2)shipReference.Value.transform.position).sqrMagnitude;

            if (comparator == Comparator.GreaterThan)
            {
                return sqrMagnitude > distance * distance;
            }
            else
            {
                return sqrMagnitude < distance * distance;
            }
        }

    }
}

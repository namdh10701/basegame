
using _Game.Scripts.Gameplay.Ship;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts.Battle
{
    public class MasterBlackboard : MonoBehaviour
    {
        [SerializeField] Blackboard blackboard;

        private void Awake()
        {
            ShipVariable shipVar = blackboard.GetVariable<ShipVariable>("Ship");
        }

    }
}

using _Game.Features.Gameplay;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

namespace _Game.Scripts
{
    public class HomeManager : MonoBehaviour
    {
        public Ship ship;
        public NodeGraph[] WalkingPosition;
        private void Awake()
        {
            // enable selecting ship
            // get loadout
            // WalkingPosition[3] = ship.Nodegraph
            // foreachCrew, random WalkingPosition, random Node, set position, set pathFinding
        }
        public void Refresh()
        {
            //if(selecting Ship != spawned ship) hide last ship, enable selecting ship
            //ship setup refresh
            //get loadout
            //same awake
        }
    }
}
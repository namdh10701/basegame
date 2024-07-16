using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Home
{
    public class PathFindingHabor : MonoBehaviour
    {
        public NodeGraph nodeGraph;
        public AStarPathFinding HaborPathFinding;

        private void Start()
        {
            HaborPathFinding = new AStarPathFinding(nodeGraph);
        }
    }
}
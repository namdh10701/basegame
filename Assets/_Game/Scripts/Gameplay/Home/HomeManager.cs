using _Game.Features.Gameplay;
using _Game.Scripts.PathFinding;
using _Game.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class HomeManager : MonoBehaviour
    {
        public Ship ship;
        public PathfindingController[] WalkingPosition;
        public Ship[] ships;
        private void Awake()
        {
            string currentShip = SaveSystem.GameSave.ShipSetupSaveData.CurrentShipId;
            foreach (Ship ship in ships)
            {
                if (ship.Id == currentShip)
                {
                    this.ship = ship;
                    ship.gameObject.SetActive(true);
                }
            }
            ship.ShipSetup.Initialize();
            ship.HideHUD();
            WalkingPosition[2] = ship.PathfindingController;
            foreach (PathfindingController pathfinding in WalkingPosition)
            {
                pathfinding.Initialize();
            }

            foreach (Crew crew in ship.ShipSetup.CrewController.crews)
            {
                PathfindingController nodeGraph = WalkingPosition[UnityEngine.Random.Range(0, 3)];
                crew.CrewMovement.pathfinder = nodeGraph;
                crew.transform.position = nodeGraph.NodeGraph.getRandomFreeNode().transform.position;
                crew.transform.parent = nodeGraph.NodeGraph.transform;
            }
            ship.ShipSetup.CrewController.ActivateCrews();
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
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.SaveLoad;
using UnityEngine;

namespace _Game.Features.Home
{
    public class HomeManager : MonoBehaviour
    {
        private static HomeManager instance;
        public static HomeManager Instance => instance;
        public Ship ship;
        public PathfindingController[] WalkingPosition;
        public Ship[] ships;
        private void Awake()
        {
            Debug.Log("RERESH 2");
            instance = this;

            foreach (PathfindingController pathfinding in WalkingPosition)
            {
                pathfinding.Initialize();
            }
            Refresh();
        }
        public void Refresh()
        {
            string currentShip = SaveSystem.GameSave.ShipSetupSaveData.CurrentShipId;
            foreach (Ship ship in ships)
            {
                if (ship.Id == currentShip)
                {
                    this.ship = ship;
                    ship.gameObject.SetActive(true);
                }
                else
                {
                    ship.gameObject.SetActive(false);
                    ship.ShipSetup.ClearItems();
                }
            }
            WalkingPosition[2] = ship.PathfindingController;
            ship.ShipSetup.Refresh();
            ship.HideHUD();

            foreach (Crew crew in ship.ShipSetup.CrewController.crews)
            {
                PathfindingController nodeGraph = WalkingPosition[UnityEngine.Random.Range(0, 3)];
                crew.CrewMovement.pathfinder = nodeGraph;
                crew.transform.position = nodeGraph.NodeGraph.getRandomFreeNode().transform.position;
                crew.transform.parent = nodeGraph.NodeGraph.transform;
                if (nodeGraph.name == "Home_Top")
                {
                    crew.Animation.SortingGroup = "WaterEnemy";
                }
                else
                if (nodeGraph.name == "Home_Bottom")
                {
                    crew.Animation.SortingGroup = "AboveShipFront";

                }
                else
                {
                    crew.Animation.SortingGroup = "OnShip";
                }
            }
            ship.ShipSetup.CrewController.ActivateCrews();
        }
    }
}
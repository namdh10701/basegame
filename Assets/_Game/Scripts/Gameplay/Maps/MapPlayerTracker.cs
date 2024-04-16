using System;
using System.Linq;
using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (mapManager.CurrentMap.path.Count == 1)
                {
                    if (mapManager.CurrentMap.IsLastNodePassed)
                    {
                        if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                            SendPlayerToNode(mapNode);
                        else
                            PlayWarningThatNodeCannotBeAccessed();
                    }
                    else
                    if (!mapManager.CurrentMap.IsLastNodeLocked)
                    {
                        if (mapNode.Node.point.y == 0)
                            SendPlayerToNode(mapNode);
                        else
                            PlayWarningThatNodeCannotBeAccessed();
                    }
                }
                else
                {
                    var previousPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 2];
                    var previousNode = mapManager.CurrentMap.GetNode(previousPoint);

                    if (mapManager.CurrentMap.IsLastNodePassed)
                    {
                        if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                            SendPlayerToNode(mapNode);
                        else
                            PlayWarningThatNodeCannotBeAccessed();
                    }
                    else
                    if (!mapManager.CurrentMap.IsLastNodeLocked)
                    {
                        if (previousNode != null && previousNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                            SendPlayerToNode(mapNode);
                        else
                            PlayWarningThatNodeCannotBeAccessed();
                    }

                }
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            if (mapManager.CurrentMap.path.Count > 0)
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (mapNode.Node.point.y == currentNode.point.y)
                {
                    mapManager.CurrentMap.path.Remove(currentNode.point);
                }
            }

            mapManager.CurrentMap.IsLastNodeLocked = false;
            mapManager.CurrentMap.IsLastNodePassed = false;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    break;
                case NodeType.EliteEnemy:
                    break;
                case NodeType.RestSite:
                    break;
                case NodeType.Relic:
                    break;
                case NodeType.Shop:
                    break;
                case NodeType.Boss:
                    break;
                case NodeType.Armory:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LevelInfoPopup levelInfoPopup = PopupManager.Instance.GetPopup<LevelInfoPopup>();
            levelInfoPopup.SetData(mapNode);
            PopupManager.Instance.ShowPopup(levelInfoPopup);
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }

        public void OnGameStart()
        {
            mapManager.CurrentMap.IsLastNodeLocked = true;
            mapManager.SaveMap();

            view.SetAttainableNodes();
            view.SetLineColors();
        }

        public void OnGamePassed()
        {
            mapManager.CurrentMap.IsLastNodeLocked = false;
            mapManager.CurrentMap.IsLastNodePassed = true;
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();

        }
    }
}
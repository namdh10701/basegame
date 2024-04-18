﻿using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

    
        public IntMinMax numOfPreBossNodes;
       
        public IntMinMax numOfStartingNodes;

        public int numOfLayer;
        [Tooltip("Increase this number to generate more paths")]
        public int extraPaths;
         public FloatMinMax distanceFromPreviousLayer;
        public float nodesApartDistance;
        [Range(0, 1)] public float randomizePosition;

        public Odds Odds;

        public MapLayer[] overridedLayers;

        public MapLayer GetOverrideLayer(int layerNum)
        {
            foreach (MapLayer mapLayer in overridedLayers)
            {
                if (mapLayer.LayerNumber == layerNum)
                {
                    return mapLayer;
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public struct Odds
    {
        public OddsItem[] OddItems;
    }

    [System.Serializable]
    public struct OddsItem
    {
        public NodeType NodeType;
        [Range(0, 1)] public float Probability;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStartEvent : MonoBehaviour, ITriggerEnterEvent
{
    [SerializeField] SectionTerrain terrainSection;
    public void Execute()
    {
        TerrainManager.Instance.OnEnterTerrain(terrainSection);
    }
}

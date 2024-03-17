using _Base.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
public class TerrainManager : SingletonMonoBehaviour<TerrainManager>
{
    private Dictionary<int, List<SectionTerrain>> terrains = new Dictionary<int, List<SectionTerrain>>();
    [SerializeField] private SectionTerrain[] allTerrains;

    public SectionTerrain CurrentTerrain;

    private void Start()
    {
        AddTerrainToDic();
    }
    void AddTerrainToDic()
    {
        foreach (SectionTerrain terrain in allTerrains)
        {
            List<SectionTerrain> sectionTerrains = new List<SectionTerrain>();
            if (!terrains.ContainsKey(terrain.TerrainTypeId))
            {
                sectionTerrains.Add(terrain);
                terrains[terrain.TerrainTypeId] = sectionTerrains;
            }
            else
            {
                terrains[terrain.TerrainTypeId].Add(terrain);
            }
        }
    }

    public void OnEnterTerrain(SectionTerrain terrainSection)
    {
        CurrentTerrain = terrainSection;
    }

    public void SpawnAheadTerrain()
    {
        if (CurrentTerrain.NextTerrain is not null)
        {
            return;
        }
        int nextTypeId = CurrentTerrain.AvailableNextTerrainTypeIds[Random.Range(0, CurrentTerrain.AvailableNextTerrainTypeIds.Length)];
        SectionTerrain nextTerrain = GetRandomTerrainOfTypeId(nextTypeId);
        CurrentTerrain.NextTerrain = Instantiate(nextTerrain, CurrentTerrain.SnapPos.position, Quaternion.identity, transform);
    }

    SectionTerrain GetRandomTerrainOfTypeId(int typeId)
    {
        SectionTerrain ret = null;
        ret = terrains[typeId][Random.Range(0, terrains[typeId].Count)];
        return ret;
    }
}
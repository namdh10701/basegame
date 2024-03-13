using UnityEngine;
public class SpawnAheadTerrainSection : MonoBehaviour, ITriggerEnterEvent
{
    public void Execute()
    {
        TerrainManager.Instance.SpawnAheadTerrain();
    }
}
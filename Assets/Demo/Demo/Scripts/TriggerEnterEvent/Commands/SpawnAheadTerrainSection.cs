using Demo.Scripts.Terrain;
using UnityEngine;

namespace Demo.Scripts.TriggerEnterEvent.Commands
{
    public class SpawnAheadTerrainSection : MonoBehaviour, ITriggerEnterEvent
    {
        public void Execute()
        {
            TerrainManager.Instance.SpawnAheadTerrain();
        }
    }
}
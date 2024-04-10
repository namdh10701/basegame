using Demo.Scripts.Terrain;
using UnityEngine;

namespace Demo.Scripts.TriggerEnterEvent.Commands
{
    public class TerrainStartEvent : MonoBehaviour, ITriggerEnterEvent
    {
        [SerializeField] SectionTerrain terrainSection;
        public void Execute()
        {
            TerrainManager.Instance.OnEnterTerrain(terrainSection);
        }
    }
}

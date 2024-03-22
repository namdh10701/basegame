using UnityEngine;

namespace Demo.Scripts.Terrain
{
    public class SectionTerrain : MonoBehaviour
    {
        public int TerrainTypeId;
        public int[] AvailableNextTerrainTypeIds;
        public Transform SnapPos;
        [HideInInspector] public SectionTerrain NextTerrain = null;
    }
}
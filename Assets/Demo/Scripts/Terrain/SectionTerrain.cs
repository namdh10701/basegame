using UnityEngine;
public class SectionTerrain : MonoBehaviour
{
    public int TerrainTypeId;
    public int[] AvailableNextTerrainTypeIds;
    public Transform SnapPos;
    [HideInInspector] public SectionTerrain NextTerrain = null;
}
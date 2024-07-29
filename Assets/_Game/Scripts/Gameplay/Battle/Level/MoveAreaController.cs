using _Game.Scripts.Battle;
using UnityEngine;

public enum AreaType
{
    Floor1st = 0, Floor2nd = 1, Floor3rd = 2, Floor1Plus2 = 3, Floor2Plus3 = 4, All = 5
}
public class MoveAreaController : MonoBehaviour
{
    public static MoveAreaController Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Area[] MoveAreas;
    public Transform[] SkullGangMovePosPool;
    public Transform[] CrabMovePosPool;
    public Transform SkullStartPos;
    public Area GetArea(AreaType areaType)
    {
        return MoveAreas[(int)areaType];
    }

    public Area GetCloset(Vector3 position)
    {
        Area area = DistanceHelper.GetClosetToPosition(MoveAreas, position).GetComponent<Area>();
        return area;
    }
    public Vector3 GetSkullGangMovePos()
    {
        //
        return SkullGangMovePosPool[UnityEngine.Random.Range(0, SkullGangMovePosPool.Length)].position;
    }

    public Vector3 GetCrabMovePos()
    {
        //
        return CrabMovePosPool[UnityEngine.Random.Range(0, CrabMovePosPool.Length)].position;
    }
}

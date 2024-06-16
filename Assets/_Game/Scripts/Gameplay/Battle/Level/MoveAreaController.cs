using _Game.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType
{
    Floor1st = 0, Floor2nd = 1, Floor3rd = 2, Floor1Plus2 = 3, Floor2Plus3 = 4
}
public class MoveAreaController : MonoBehaviour
{
    public Area[] MoveAreas;

    public Area GetArea(AreaType areaType)
    {
        return MoveAreas[(int)areaType];
    }
}

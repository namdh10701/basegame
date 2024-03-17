using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyLayer
{
    MoveAlongShip,
    Free
}
public class EnemySpawnData : MonoBehaviour
{
    public int EnemyId;
    public EnemyLayer Layer;
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPool : MonoBehaviour
{
    public List<Vector2> Pool = new List<Vector2>();
    int index = 0;

    public void SetPool(List<Vector2> pool)
    {
        this.Pool = pool;
    }

    public Vector2 GetNextPosition()
    {
        if (Pool.Count == 0)
        {
            return Vector2.zero;
        }
        else
        {

        }
        Vector2 ret = Pool[index];
        index++;
        if (index >= Pool.Count)
        {
            index = 0;
        }
        return ret;
    }

    public Vector2 GetRandomPosition()
    {
        return Pool[UnityEngine.Random.Range(0, Pool.Count)];
    }

    public Vector2 GetRandomExcept(Vector2 vector2)
    {
        if (Pool.Count == 1)
        {
            throw new System.Exception("infinite loop");
        }
        Vector2 ret = GetRandomPosition();
        while (ret == vector2)
        {
            ret = Pool[index];
        }
        return ret;
    }
}

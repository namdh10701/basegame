using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    protected PoolManager poolManager;
    public void SetPool(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }
    public abstract void OnReset();
}

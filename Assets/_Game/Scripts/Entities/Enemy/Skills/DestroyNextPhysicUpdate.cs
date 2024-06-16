using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNextPhysicUpdate : MonoBehaviour
{
    bool shouldDestroy = false;
    private void FixedUpdate()
    {
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            shouldDestroy = true;
        }
    }
}

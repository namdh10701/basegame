using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEnabled : MonoBehaviour
{
    float time = .5f;
    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > time)
        {
            Destroy(gameObject);
        }
    }
}

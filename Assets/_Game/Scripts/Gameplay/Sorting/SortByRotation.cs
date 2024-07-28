using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByRotation : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;
    [SerializeField] Transform refer;
    // Update is called once per frame
    void Update()
    {
        if (refer.rotation.eulerAngles.z > 90 && refer.rotation.eulerAngles.z < 270)
        {
            mr.sortingLayerName = "FlyingEnemy";
        }
        else
        {
            mr.sortingLayerName = "Player";
        }
    }
}

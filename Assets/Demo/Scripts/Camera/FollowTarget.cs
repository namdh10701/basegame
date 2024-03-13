using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public enum FollowType
    {
        Horizontal, Vertical, Both
    }
    [SerializeField] Transform target;
    [SerializeField] float offsetXThreshold;
    [SerializeField] FollowType followType;
    //[SerializeField] float offsetYThreshold;

    Vector3 position = new Vector3();

    private void Update()
    {
        Vector3 pos = transform.position;
        switch (followType)
        {
            case FollowType.Horizontal:
                {
                    position.y = target.position.y;
                    float offsetX = target.position.x - transform.position.x;
                    if (Mathf.Abs(transform.position.x - target.position.x) > offsetXThreshold)
                    {
                        pos = Vector2.Lerp(pos, target.position, Time.deltaTime * 8);
                        position.x = target.position.x;
                    }
                }
                break;
        }
    }

    private void LateUpdate()
    {
        position.z = -10;
        transform.position = position;
    }


}

using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SetAnimColor : MonoBehaviour
{
    public SkeletonAnimation skeletonAnim;
    public Color color;
    private void Start()
    {
        if (skeletonAnim != null)
        {
            skeletonAnim.skeleton?.SetColor(color);
        }
    }
    private void OnValidate()
    {
        if (skeletonAnim != null)
        {
            skeletonAnim.skeleton?.SetColor(color);
        }
    }
}

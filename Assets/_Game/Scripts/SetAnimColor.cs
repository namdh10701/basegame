using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimColor : MonoBehaviour
{
    public SkeletonAnimation skeletonAnim;
    public Color color;
    private void Start()
    {
        skeletonAnim.skeleton.SetColor(color);
    }
}

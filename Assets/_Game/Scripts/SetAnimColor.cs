using Spine.Unity;
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

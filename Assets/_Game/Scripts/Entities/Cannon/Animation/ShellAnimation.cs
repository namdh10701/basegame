using _Base.Scripts.Patterns.BuiltInPool;
using Spine.Unity;
using UnityEngine;

public class ShellAnimation : PoolObject, ICannonVisualElement
{
    public int offsetSort = 1;
    public MeshRenderer meshRenderer;
    public SkeletonAnimation SkeletonAnimation;
    private void Start()
    {
        SkeletonAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "shell_left" || trackEntry.Animation.Name == "shell_right")
            Release();
    }

    public override void OnRelease()
    {
        SkeletonAnimation.gameObject.SetActive(false);
    }

    public void PlayLeftShell()
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, "shell_left", false);
    }
    public void PlayRightShell()
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, "shell_right", false);
    }

    public void UpdateSorting(Renderer mainRenderer)
    {
        meshRenderer.sortingLayerName = mainRenderer.sortingLayerName;
        meshRenderer.sortingOrder = mainRenderer.sortingOrder + offsetSort;
    }
}

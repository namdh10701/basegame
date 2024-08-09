using Spine.Unity;
using UnityEngine;

public class MuzzleAnimation : MonoBehaviour, ICannonVisualElement
{
    public SkeletonAnimation SkeletonAnimation;
    [SpineAnimation] public string smoke;

    public SkeletonAnimation SkeletonAnimationSmoke;
    public MeshRenderer[] meshRenderers;
    bool isPlayed;
    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        Destroy(gameObject);
    }

    public void Play(float delay)
    {
        if (isPlayed)
            return;
        isPlayed = true;
        if (SkeletonAnimationSmoke != null)
            SkeletonAnimationSmoke.AnimationState.AddAnimation(0, "demo_fireeffect", false, delay);

        SkeletonAnimation.AnimationState.Complete += AnimationState_Complete;
        SkeletonAnimation.AnimationState.AddAnimation(0, smoke, false, delay);
    }

    public void UpdateSorting(Renderer mainRenderer)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            /*meshRenderers[i].sortingLayerName = mainRenderer.sortingLayerName;
            meshRenderers[i].sortingOrder = mainRenderer.sortingOrder - 1 - i;*/
        }
    }
}

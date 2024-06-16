using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class EffectTakerCollider : MonoBehaviour, IEffectTakerCollider
{
    public IEffectTaker Taker { get; set; }
}

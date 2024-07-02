using DG.Tweening;
using UnityEngine;

public class Fx : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer;
    Sequence sequence;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] float duration = .25f;
    [SerializeField] float destroyAfter = 1.5f;
    private void OnEnable()
    {
        SpriteRenderer.color = startColor;
        sequence = DOTween.Sequence();

        sequence.Append(SpriteRenderer.DOColor(endColor, duration));
        sequence.Append(SpriteRenderer.DOColor(startColor, duration));
        sequence.SetLoops(-1);
        Invoke("SelfDestroy", destroyAfter);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        sequence.Kill();
    }
}
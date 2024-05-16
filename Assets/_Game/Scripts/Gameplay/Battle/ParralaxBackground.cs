using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParralaxBackground : MonoBehaviour
{
    [SerializeField] Renderer background;
    [SerializeField] Vector2 currentOffset;
    public Vector2 direction;
    public Vector2 scrollSpeed;

    Material material;
    public Texture backgroundTexture;
    private void Start()
    {
        material = background.material;
        material.SetTexture("_MainTex", backgroundTexture);
    }
    void FixedUpdate()
    {
        currentOffset += scrollSpeed * direction;
        material.SetVector("_Offset", currentOffset);
    }

    public void AdjustSpeed(Vector2 newSpeed, float duration)
    {
        StartCoroutine(AdjustSpeedCoroutine(newSpeed, duration));
    }

    IEnumerator AdjustSpeedCoroutine(Vector2 newSpeed, float duration)
    {
        float elapsedTime = 0;
        Vector2 startSpeed = scrollSpeed;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollSpeed = Vector2.Lerp(startSpeed, newSpeed, elapsedTime / 2);
            yield return null;
        }
        scrollSpeed = newSpeed;
    }

}

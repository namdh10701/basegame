using System.Collections;
using UnityEngine;


public class ParralaxBackground : MonoBehaviour
{
    [SerializeField] Renderer background;
    [SerializeField] Renderer waves;

    [SerializeField] Vector2 currentOffset;
    public Vector2 direction;
    public Vector2 scrollSpeed;

    Material material;
    Material waveMaterial;
    public Texture backgroundTexture;


    public float startSpeed;


    private void Start()
    {
        material = background.material;
        waveMaterial = waves.material;
        material.SetTexture("_MainTex", backgroundTexture);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AdjustSpeed(new Vector2(0.001f, 0), 1f);
        }

        currentOffset += scrollSpeed * Time.deltaTime * direction;
        material.SetVector("_Offset", currentOffset);
        waveMaterial.SetVector("_Offset", currentOffset);
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
            scrollSpeed = Vector2.Lerp(startSpeed, newSpeed, elapsedTime / duration);
            yield return null;
        }
        scrollSpeed = newSpeed;
    }

}

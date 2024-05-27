
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter filter;
    [SerializeField] public float FOV;
    [SerializeField] private float viewDistance;

    [SerializeField] Transform target;
    private int rayCount = 10;
    private Mesh mesh;
    Material material;
    private void Start()
    {
        material = meshRenderer.material;
        mesh = new Mesh();
        filter.mesh = mesh;
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeToAlpha(material, 1.6F,1));
    }

    public void TurnOff()
    {
        StartCoroutine(FadeToAlpha(material, 0F, 1));
    }

    IEnumerator FadeToAlpha(Material material, float targetAlpha, float duration)
    {
        float elapsedTime = 0;
        float currentAlpha = material.GetFloat("_Alpha");

        material.SetFloat("_Alpha", 0);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / duration);
            material.SetFloat("_Alpha", newAlpha);
            yield return null;
        }

        material.SetFloat("_Alpha", targetAlpha);
    }


    public void SetAimingDirection(Vector2 direction)
    {
        transform.right = direction;
    }
    private void Update()
    {
        SetViewDistance(Vector2.Distance(target.transform.position, transform.position));
        SetAimingDirection(target.transform.position - transform.position);
    }
    private void LateUpdate()
    {
        float angle = FOV / 2;
        float angleIncrease = FOV / (rayCount);

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            vertex = Vector3.zero + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            uv[vertexIndex] = new Vector2((vertices[vertexIndex].x - Vector3.zero.x) / viewDistance, (vertices[vertexIndex].y - Vector3.zero.y) / viewDistance);

            vertexIndex++;
            angle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
    }

    public void SetFoV(float fov)
    {
        this.FOV = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

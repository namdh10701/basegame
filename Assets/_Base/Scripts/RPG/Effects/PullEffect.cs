using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullEffect : MonoBehaviour
{
    public Collider2D pullArea;
    public float pullStrength = 5f;
    public float swirlStrength = 2f;
    public float duration = 4f;
    private List<Rigidbody2D> enemiesInRange = new List<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && !enemiesInRange.Contains(rb))
            {
                enemiesInRange.Add(rb);
            }
        }
        StartCoroutine(PullEffectCoroutine());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && enemiesInRange.Contains(rb))
            {
                enemiesInRange.Remove(rb);
            }
        }
    }

    IEnumerator PullEffectCoroutine()
    {
        yield return new WaitForSeconds(1f); // Đợi 1 giây trước khi hút
        PullEnemies(); // Hút các enemy một lần
        yield return new WaitForSeconds(3f); // Đợi thêm 3 giây nữa trước khi huỷ Collider2D
        Destroy(pullArea); // Huỷ Collider2D sau 4 giây
    }

    void PullEnemies()
    {
        foreach (Rigidbody2D rb in enemiesInRange)
        {
            Vector2 direction = (transform.position - rb.transform.position).normalized;
            Vector2 perpendicular = Vector2.Perpendicular(direction).normalized;

            Vector2 pullForce = direction * pullStrength;
            Vector2 swirlForce = perpendicular * swirlStrength;

            rb.AddForce(pullForce + swirlForce);
        }
    }

    void OnDrawGizmos()
    {
        if (pullArea != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, pullArea.bounds.extents.x);
        }
    }
}

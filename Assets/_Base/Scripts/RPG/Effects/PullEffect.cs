using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

public class PullEffect : MonoBehaviour
{
    public CircleCollider2D pullArea;
    public float pullStrength = 5f;
    public float swirlStrength = 2f;
    public float duration = 4f;
    public LayerMask enemyLayer; // Thêm biến để lưu layer của Enemy
    private List<Rigidbody2D> enemiesInRange = new List<Rigidbody2D>();
    void Awake()
    {
        pullArea = GetComponent<CircleCollider2D>();
    }

    public void Setsize(float size)
    {
        pullArea.radius = size;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            EntityProvider entityProvider = other.GetComponent<EntityProvider>();
            if (entityProvider != null)
            {
                enemiesInRange.Add(entityProvider.Entity.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && enemiesInRange.Contains(rb))
            {
                enemiesInRange.Remove(rb);
            }
        }
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

    void Update()
    {
        if (duration > 0 && enemiesInRange.Count > 0)
        {
            PullEnemies();
            duration = duration - Time.deltaTime;
        }

        if (duration <= 0)
            Destroy(pullArea);

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

using UnityEngine;
public class DefenseBehaviour : MonoBehaviour
{
    public float Hp;
    public float BlockFactor;

    string bulletLayerName = "PlayerProjectile";
    private int bulletLayer;

    private void Start()
    {
        bulletLayer = LayerMask.NameToLayer(bulletLayerName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == bulletLayer)
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            projectile.OnHit();
        }
    }
}
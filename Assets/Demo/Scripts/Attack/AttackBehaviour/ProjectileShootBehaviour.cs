using UnityEngine;
public class ProjectileShootBehaviour : AttackBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    public int Piercing;
    public int Speed;
    public float Accuracy;
    public int AOE;

    public override void Attack(Transform target)
    {
        Rigidbody2D body = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, 5);
    }
}
using UnityEngine;
public class CanonShoot : MonoBehaviour
{
    public Canon gun;
    public Projectile projectilePrefab;
    public Transform shootPos;
    public Transform visual;
    public float Accuracy { get; set; }

    public void Shoot(Transform transform)
    {
        Vector3 aim = visual.rotation.eulerAngles;
        aim.z += Random.Range(-Accuracy, Accuracy);
        Quaternion shootDirection = Quaternion.Euler(aim);
        Projectile projectile = Instantiate(projectilePrefab, shootPos.position, shootDirection, null);
        projectile.Speed = 2;
        DamageSource damageSource = projectile.GetComponent<DamageSource>();
        damageSource.Dmg += gun.Attack;
    }
}
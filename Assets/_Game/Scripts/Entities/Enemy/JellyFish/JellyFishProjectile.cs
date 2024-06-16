using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways]
public class JellyFishProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    EnemyAttackData atkData;
    public float speed;
    public float rotateSpeed;
    [SerializeField] ParticleSystem particle;
    public bool isLaunched;
    public void SetData(EnemyAttackData atkData, Vector2 startPos, float deviation)
    {
        this.atkData = atkData;
        transform.position = startPos;
        var targetPosition = atkData.CenterCell.transform.position;
        Vector2 targetDirection = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle - 90 + deviation, Vector3.forward);
        transform.rotation = newRotation;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {

        if (isLaunched)
        {
            Vector2 targetDirection = (atkData.CenterCell.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * rotateSpeed);

            body.velocity = speed * transform.up;
            rotateSpeed += Time.fixedDeltaTime * 5;
            if (Vector2.Distance(body.position, atkData.CenterCell.transform.position) < .5f)
            {
                OnImpact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 3);
    }
    public void Launch()
    {
        isLaunched = true;
    }

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == atkData.CenterCell.gameObject)
            {
                OnImpact();
            }
        }*/

    void OnImpact()
    {
        Destroy(gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
        GridAttackHandler attackHandler = FindAnyObjectByType<GridAttackHandler>();
        attackHandler.ProcessAttack(atkData.TargetCells, atkData.Effect);
    }
}

using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    EnemyAttackData atkData;
    public float speed;
    public float rotateSpeed;
    public float deviation;
    [SerializeField] ParticleSystem particle;
    public void SetData(EnemyAttackData atkData, Vector2 startPos)
    {
        this.atkData = atkData;
        transform.position = startPos;
        //transform.up = (atkData.CenterCell.transform.position - transform.position).normalized ;
        var targetPosition = atkData.CenterCell.transform.position;
        targetPosition.x += Random.Range(-deviation, deviation);
        targetPosition.y += Random.Range(-deviation, deviation);

        var direction = (Vector2)targetPosition - startPos;
        transform.up = direction.normalized;

        body.velocity = speed * transform.up;
    }

    private void FixedUpdate()
    {
        Vector2 direction = atkData.CenterCell.transform.position - transform.position;
        transform.up = Vector3.Lerp(transform.up, direction, rotateSpeed * Time.fixedDeltaTime);
        body.velocity = speed * transform.up;
    }

    public void Launch()
    {
        body.velocity = speed * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == atkData.CenterCell.gameObject)
        {
            OnImpact();
        }
    }

    void OnImpact()
    {
        Destroy(gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
        GridAttackHandler attackHandler = FindAnyObjectByType<GridAttackHandler>();
        attackHandler.ProcessAttack(atkData.TargetCells, atkData.Effect);
    }
}

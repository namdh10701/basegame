using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D body;
    EnemyAttackData atkData;
    public float speed;

    public void SetData(EnemyAttackData atkData, Vector2 startPos)
    {
        this.atkData = atkData;
        transform.position = startPos;
        transform.up = (atkData.CenterCell.transform.position - transform.position).normalized;

    }

    public void Launch()
    {
        body.velocity = new Vector2(0, speed);
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
        GridAttackHandler attackHandler = FindAnyObjectByType<GridAttackHandler>();
        attackHandler.ProcessAttack(atkData.TargetCells, atkData.Effect);
    }
}

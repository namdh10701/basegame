using _Base.Scripts.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GlobalData.PlayerLayer)
        {
            enemy.IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GlobalData.PlayerLayer)
        {
            enemy.IsPlayerInRange = false;
        }
    }
}

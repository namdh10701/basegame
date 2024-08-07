using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyNet : MonoBehaviour
{
    [SerializeField] DecreaseHealthEffect dhe;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EffectTakerCollider enemy))
        {
            enemy.Taker.EffectHandler.Apply(dhe);
        }
    }
}

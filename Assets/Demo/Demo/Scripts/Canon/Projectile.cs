using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using Demo.ScriptableObjects.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Rigidbody2D body;
        [SerializeField] public AmmoData AmmoData;
        [SerializeField] public List<_Game.Scripts.Cell> targetCells;
        [SerializeField] public Cell centerCell;
        bool isDestroyed;
        [SerializeField] GridAttackHandler gridAttackHandler;
        private void Start()
        {
            gridAttackHandler = FindAnyObjectByType<GridAttackHandler>();
            body.velocity = transform.up * 3;
        }

        private void Update()
        {
            if (!isDestroyed)
            {
               /* if (centerCell.GetComponent<BoxCollider2D>().bounds.Contains(transform.position))
                {
                    gridAttackHandler.ProcessAttack(targetCells, new DecreaseHealthEffect(1));
                    isDestroyed = true;
                    Destroy(gameObject);
                }*/

            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public void OnHit()
        {
            Destroy(gameObject);
        }


    }
}
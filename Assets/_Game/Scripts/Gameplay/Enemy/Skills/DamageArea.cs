using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace _Game.Scripts
{
    public class DamageArea : MonoBehaviour
    {
        public CircleCollider2D collider;
        public float forceApply;
        public DecreaseHealthEffect decreaseHealthEffect;
        public List<Entity> ignoreEntity = new List<Entity>();
        bool isActivated;

        public void Activate()
        {
            isActivated = true;
            RaycastHit2D[] results = Physics2D.CircleCastAll(transform.position, collider.radius, Vector2.zero);
            List<Entity> affectEntities = new List<Entity>();
            List<Cell> affectCells = new List<Cell>();
            foreach (RaycastHit2D result in results)
            {
                if (result.collider.TryGetComponent<EntityCollisionDetector>(out EntityCollisionDetector entityCollisionDetector))
                {
                    Entity entity = entityCollisionDetector.GetComponent<EntityProvider>().Entity;
                    if (!ignoreEntity.Contains(entity))
                        affectEntities.Add(entity);
                }
                if (gameObject.TryGetComponent<Cell>(out Cell cell))
                {
                    affectCells.Add(cell);
                }
            }
            foreach (Entity entity in affectEntities)
            {
                Rigidbody2D body = entity.GetComponent<Rigidbody2D>();
                EffectHandler eh = entity.EffectHandler;
                body.AddForceAtPosition(forceApply * (Vector2)(body.position - (Vector2)transform.position).normalized, transform.position, ForceMode2D.Impulse);
                eh.Apply(decreaseHealthEffect);
            }


            GridAttackHandler gah = FindAnyObjectByType<GridAttackHandler>();
            gah.ProcessAttack(affectCells, decreaseHealthEffect);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.V))
            {
                Activate();
            }
        }
    }
}
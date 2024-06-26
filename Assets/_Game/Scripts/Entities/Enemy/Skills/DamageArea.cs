using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace _Game.Scripts
{
    public class DamageArea : MonoBehaviour, IEffectGiver
    {
        public CircleCollider2D collider;
        public float forceApply;
        public List<Entity> ignoreEntity = new List<Entity>();
        bool isActivated;

        public List<Effect> outGoingEffects = new List<Effect>();
        public Transform Transform => transform;
        public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

        public void SetDamage(float damage)
        {
            foreach (Effect effect in OutGoingEffects)
            {
                if (effect is ScaledDecreaseHealthEffect decreaseHealthEffect)
                {
                    decreaseHealthEffect.PlayerAmount = damage;
                    decreaseHealthEffect.EnemyAmount = damage;
                }
            }
        }
    }
}
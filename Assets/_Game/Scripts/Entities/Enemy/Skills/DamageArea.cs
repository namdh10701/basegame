using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class DamageArea : MonoBehaviour, IEffectGiver
    {
        public CircleCollider2D collider;
        public float forceApply;
        public LayerMask affectLayers;
        public List<Entity> ignoreEntity = new List<Entity>();
        bool isActivated;

        public List<Effect> outGoingEffects = new List<Effect>();
        public Transform Transform => transform;
        public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

        public void SetDamage(float damage, float armoPenetrate)
        {
            foreach (Effect effect in OutGoingEffects)
            {
                if (effect is DecreaseHealthEffect decreaseHealthEffect)
                {
                    decreaseHealthEffect.Amount = damage;
                    decreaseHealthEffect.AmmoPenetrate = armoPenetrate;
                }
            }
        }

        public void SetRange(float range)
        {
            if (range <= 0)
            {
                range = 1;
            }
            collider.radius = range;
        }
    }
}
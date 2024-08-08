using _Base.Scripts.EventSystem;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Features.Gameplay
{
    public class DamageInflictTracker : MonoBehaviour
    {
        public InflictedDamageDisplay prefab;
        public ConsumedManaDisplay consumedManaDisplayPrefab;
        public ConsumedManaDisplay refunded;
        private void OnEnable()
        {
            GlobalEvent<float, bool, IEffectGiver, IEffectTaker, Vector3>.Register("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
            GlobalEvent<int, Vector3>.Register("MANA_REFUNDED", OnManaRefunded);
        }


        private void OnDisable()
        {
            GlobalEvent<float, bool, IEffectGiver, IEffectTaker, Vector3>.Unregister("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Unregister("MANA_CONSUMED", OnManaConsumed);
        }

        void OnManaRefunded(int amount, Vector3 position)
        {
            ConsumedManaDisplay idd = Instantiate(refunded, transform);
            idd.Init("Refunded", position);
        }

        void OnDamageInflicted(float amount, bool isCrit, IEffectGiver effectGiver, IEffectTaker effectTaker, Vector3 position)
        {
            if (effectTaker is Cell || effectTaker is Carpet || effectTaker is CarpetComponent)
            {
                return;
            }
            if (effectGiver != null)
            {
                if (effectGiver is CannonProjectile)
                    GlobalEvent<float>.Send("PlayerDmgInflicted", amount);
            }
            InflictedDamageDisplay idd = Instantiate(prefab, transform);
            idd.Init(amount, isCrit, position);
        }
        void OnManaConsumed(int amount, Vector3 pos)
        {
            ConsumedManaDisplay idd = Instantiate(consumedManaDisplayPrefab, transform);
            idd.Init(amount, pos);
        }
    }
}
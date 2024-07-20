using _Base.Scripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class DamageInflictTracker : MonoBehaviour
    {
        public InflictedDamageDisplay prefab;
        public ConsumedManaDisplay consumedManaDisplayPrefab;
        private void OnEnable()
        {
            GlobalEvent<float, bool, IEffectTaker>.Register("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
        }

        private void OnDisable()
        {
            GlobalEvent<float, bool, IEffectTaker>.Unregister("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
        }

        void OnDamageInflicted(float amount, bool isCrit, IEffectTaker effectTaker)
        {
            if (effectTaker is Ship)
            {
                return;
            }
            InflictedDamageDisplay idd = Instantiate(prefab, transform);
            idd.Init(amount, isCrit, effectTaker.Transform.position);
        }
        void OnManaConsumed(int amount, Vector3 pos)
        {
            Debug.Log("HERE");
            ConsumedManaDisplay idd = Instantiate(consumedManaDisplayPrefab, transform);
            idd.Init(amount, pos);
        }
    }
}
using _Base.Scripts.EventSystem;
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
        private void OnEnable()
        {
            GlobalEvent<float, bool, IEffectGiver, IEffectTaker, Vector3>.Register("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
        }

        private void OnDisable()
        {
            GlobalEvent<float, bool, IEffectGiver, IEffectTaker, Vector3>.Unregister("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Unregister("MANA_CONSUMED", OnManaConsumed);
        }

        void OnDamageInflicted(float amount, bool isCrit, IEffectGiver effect, IEffectTaker effectTaker, Vector3 position)
        {
            if (effectTaker is Cell || effectTaker is Carpet || effectTaker is CarpetComponent)
            {
                return;
            }
            InflictedDamageDisplay idd = Instantiate(prefab, transform);
            idd.Init(amount, isCrit, position);
        }
        void OnManaConsumed(int amount, Vector3 pos)
        {
            Debug.Log("HERE");
            ConsumedManaDisplay idd = Instantiate(consumedManaDisplayPrefab, transform);
            idd.Init(amount, pos);
        }
    }
}
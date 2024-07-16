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
            GlobalEvent<float, bool, Vector3>.Register("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
        }

        private void OnDisable()
        {
            GlobalEvent<float, bool, Vector3>.Unregister("DAMAGE_INFLICTED", OnDamageInflicted);
            GlobalEvent<int, Vector3>.Register("MANA_CONSUMED", OnManaConsumed);
        }

        void OnDamageInflicted(float amount, bool isCrit, Vector3 pos)
        {
            InflictedDamageDisplay idd = Instantiate(prefab, transform);
            idd.Init(amount, isCrit, pos);
        }
        void OnManaConsumed(int amount, Vector3 pos)
        {
            Debug.Log("HERE");
            ConsumedManaDisplay idd = Instantiate(consumedManaDisplayPrefab, transform);
            idd.Init(amount, pos);
        }
    }
}
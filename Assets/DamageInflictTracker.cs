using _Base.Scripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInflictTracker : MonoBehaviour
{
    public InflictedDamageDisplay prefab;

    private void OnEnable()
    {
        GlobalEvent<float, bool, Vector3>.Register("DAMAGE_INFLICTED", OnDamageInflicted);
    }

    private void OnDisable()
    {
        GlobalEvent<float, bool, Vector3>.Unregister("DAMAGE_INFLICTED", OnDamageInflicted);
    }

    void OnDamageInflicted(float amount, bool isCrit, Vector3 pos)
    {
        InflictedDamageDisplay idd = Instantiate(prefab,transform);
        idd.Init(amount, isCrit, pos);
    }
}

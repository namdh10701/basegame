using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cannon Data")]
public class CanonData : ScriptableObject
{
    public string Id;
    public string GunName;
    public float Attack;
    public float AttackSpeed;
    public float Accuracy;
    public float CritChance;
    public float CritDamage;
}

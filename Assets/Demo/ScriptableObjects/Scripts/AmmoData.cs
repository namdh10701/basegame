using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ammo Data")]
public class AmmoData : ScriptableObject
{
    public int Id;
    public float Attack;
    public float Piercing;
    public float Speed;
    public float Accuracy;
    public float CritChance;
    public float CritDamage;
    public float AOE;
}
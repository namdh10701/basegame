using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ship Stats Template")]
[Serializable]
public class ShipStatsTemplate : ScriptableObject
{
    [field: SerializeField] public ShipStats Data { get; set; } = new();
}

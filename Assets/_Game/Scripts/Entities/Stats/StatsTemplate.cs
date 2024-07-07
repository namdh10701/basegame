using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatsTemplate : ScriptableObject
{
    public abstract void ApplyConfig(Stats stats);
}

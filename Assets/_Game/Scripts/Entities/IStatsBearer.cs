using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsBearer
{
    public Stats Stats { get; }
    public void ApplyStats();
}

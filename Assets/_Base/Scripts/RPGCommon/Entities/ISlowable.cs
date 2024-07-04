using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlowable
{
    public void OnSlowed();
    public void OnSlowEnded();
    public List<Stat> SlowableStats { get; }
}

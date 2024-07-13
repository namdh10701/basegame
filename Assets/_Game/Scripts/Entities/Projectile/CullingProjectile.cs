using _Game.Scripts.Entities;
using UnityEngine;

public class CullingProjectile : StandardProjectile
{
    public KillShotEffect killShotEffect;

    public override void ApplyStats()
    {
        base.ApplyStats();
        killShotEffect.Threshold = _stats.HpThreshold.Value;
    }
}

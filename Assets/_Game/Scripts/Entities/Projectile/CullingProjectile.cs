using _Game.Scripts.Entities;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class CullingProjectile : StandardProjectile
    {
        public KillShotEffect killShotEffect;

        public override void ApplyStats()
        {
            base.ApplyStats();
            killShotEffect.Threshold = _stats.HpThreshold.Value;
        }
    }
}
using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;

namespace _Game.Scripts.Entities
{
    public class CannonProjectile : Projectile
    {
        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ammos.TryGetValue(Id, out AmmoConfig cannonConfig))
                {
                    cannonConfig.ApplyGDConfig(_stats);
                }
            }
            else
            {
                _statsTemplate.ApplyConfig(_stats);
            }
        }

        protected override void LoadModifiers()
        {

        }
        protected override void ApplyStats()
        {
            outGoingEffects = new List<Effect>() {
                new DecreaseHealthEffect(_stats.Damage.Value)
            };
        }
    }
}
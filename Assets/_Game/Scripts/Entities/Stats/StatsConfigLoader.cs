using _Base.Scripts.RPG.Attributes;
using _Game.Features.Gameplay;
using _Game.Scripts.Attributes;
using _Game.Scripts.GD.DataManager;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Scripts
{
    public abstract class StatsConfigLoader<TStat, TDataTableRecord>
        where TStat : Stats
        where TDataTableRecord : DataTableRecord
    {
        public abstract void LoadConfig(TStat stats, TDataTableRecord tableRecord);
    }

    public class CannonStatsConfigLoader : StatsConfigLoader<CannonStats, CannonTableRecord>
    {
        public override void LoadConfig(CannonStats stats, CannonTableRecord tableRecord)
        {
            stats.HealthPoint.MaxStatValue.BaseValue = tableRecord.Hp;
            stats.HealthPoint.StatValue.BaseValue = tableRecord.Hp;
            stats.AttackDamage.BaseValue = tableRecord.Attack;
            stats.AttackAccuracy.BaseValue = tableRecord.Accuracy;
            stats.CriticalChance.BaseValue = tableRecord.CritChance;
            stats.CriticalDamage.BaseValue = tableRecord.CritDamage;
            stats.AttackSpeed.BaseValue = tableRecord.AttackSpeed;
            stats.AttackRange.BaseValue = tableRecord.Range;
            stats.ProjectileCount.BaseValue = tableRecord.ProjectCount;
            stats.PrimaryDamage.BaseValue = tableRecord.PrimaryProjectDmg;
            stats.SecondaryDamage.BaseValue = tableRecord.SecondaryProjectDmg;
            stats.Angle.BaseValue = tableRecord.Angle;
        }
    }

    public class CrewStatsConfigLoader : StatsConfigLoader<CrewStats, CrewTableRecord>
    {
        public override void LoadConfig(CrewStats stats, CrewTableRecord tableRecord)
        {
            stats.Luck.BaseValue = tableRecord.Luck;
            stats.BonusAmmo.BaseValue = tableRecord.BonusAmmo;
            stats.StatusReduce.BaseValue = tableRecord.StatusReduce;
            stats.FeverTimeProb.BaseValue = tableRecord.FeverTimeProb;
            stats.ZeroManaCost.BaseValue = tableRecord.ZeroManaCost;
            stats.MoveSpeed.BaseValue = tableRecord.MoveSpeed;
            stats.RepairSpeed.BaseValue = tableRecord.RepairSpeed;
        }
    }

    public class EnemyStatsConfigLoader : StatsConfigLoader<EnemyStats, MonsterTableRecord>
    {
        public override void LoadConfig(EnemyStats stats, MonsterTableRecord tableRecord)
        {
            stats.AttackDamage.BaseValue = tableRecord.Attack;
            stats.AttackRange.BaseValue = tableRecord.AttackRange;
            stats.HealthPoint.MaxStatValue.BaseValue = tableRecord.Hp;
            stats.HealthPoint.StatValue.BaseValue = tableRecord.Hp;
            stats.ActionSequenceInterval.BaseValue = tableRecord.AttackSpeed;
        }
    }

    public class AmmoStatsConfigLoader : StatsConfigLoader<Stats, AmmoTableRecord>
    {
        public override void LoadConfig(Stats stats, AmmoTableRecord tableRecord)
        {
            if (stats is AmmoStats ammoStats)
            {
                ammoStats.HealthPoint.MaxStatValue.BaseValue = tableRecord.Hp;
                ammoStats.HealthPoint.StatValue.BaseValue = tableRecord.Hp;
                ammoStats.EnergyCost.BaseValue = tableRecord.EnergyCost;
                ammoStats.MagazineSize.BaseValue = tableRecord.MagazineSize;

            }
            else
            {
                ProjectileStats projectileStats = stats as ProjectileStats;
                projectileStats.Damage.BaseValue = tableRecord.AmmoAttack;
                projectileStats.CritDamage.BaseValue = tableRecord.AmmoCritDamage;
                projectileStats.CritChance.BaseValue = tableRecord.AmmoCritChance;
                projectileStats.Accuracy.BaseValue = tableRecord.AmmoAccuracy;
                projectileStats.ArmorPenetrate.BaseValue = tableRecord.ArmorPen;
                projectileStats.AttackAOE.BaseValue = tableRecord.AttackAoe;
                projectileStats.Speed.BaseValue = tableRecord.ProjectSpeed;
                projectileStats.Piercing.BaseValue = tableRecord.ProjectPiercing;
                projectileStats.HpThreshold.BaseValue = tableRecord.HpThreshold;
                projectileStats.SpeedModifier.BaseValue = tableRecord.SpeedModifer;
                projectileStats.Duration.BaseValue = tableRecord.Duration;
                projectileStats.TriggerProb.BaseValue = tableRecord.TriggerProb;
            }
        }
    }

    public class ShipStatsConfigLoader : StatsConfigLoader<ShipStats, ShipTableRecord>
    {
        public override void LoadConfig(ShipStats shipStats, ShipTableRecord tableRecord)
        {
            shipStats.BlockChance.BaseValue = tableRecord.BlockChance;
            shipStats.HealthPoint.MinStatValue.BaseValue = 0;
            shipStats.HealthPoint.MaxStatValue.BaseValue = tableRecord.Hp;
            shipStats.HealthPoint.StatValue.BaseValue = tableRecord.Hp;

            shipStats.ManaRegenerationRate.BaseValue = tableRecord.ManaRegenRate;
            shipStats.ManaPoint.MinStatValue.BaseValue = tableRecord.MaxMana;
            shipStats.ManaPoint.MaxStatValue.BaseValue = tableRecord.MaxMana;
            shipStats.ManaPoint.StatValue.BaseValue = tableRecord.MaxMana;

            shipStats.AmmoLimit.BaseValue = tableRecord.AmmoLimit;
            shipStats.CannonLimit.BaseValue = tableRecord.CannonLimit;
        }
    }
}
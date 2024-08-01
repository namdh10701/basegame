using _Game.Scripts.GD.DataManager;

namespace _Game.Scripts
{
    public abstract class StatsConfigLoader<TStat, TDataTableRecord> 
        where TStat: Stats 
        where TDataTableRecord: DataTableRecord
    {
        public abstract void LoadConfig(TStat stats, TDataTableRecord tableRecord);
    }
    
    public class CannonStatsConfigLoader: StatsConfigLoader<CannonStats, CannonTableRecord>
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
}
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.Parser
{
    public class RawLevelData
    {
        [Index(0)]
        public string Level { get; set; }
        
        [Index(1)]
        public string NormalTimeOffset { get; set; }
        
        [Index(2)]
        public string NormalTotalPower { get; set; }
        
        [Index(3)]
        public string NormalEnemyId { get; set; }
        
        [Index(4)]
        public string ElitePool { get; set; } 
        
        [Index(5)]
        public string EliteTimeOffset { get; set; }
        
        [Index(6)]
        public string EliteTotalPower { get; set; }
        
        [Index(7)]
        public string EliteEnemyId { get; set; }
    }
    
    public class LevelData
    {
        public string Stage { get; set; }
        public string Level { get; set; }
        public string TimeOffset { get; set; }
        public string TotalPower { get; set; }
        public List<string> EnemyId { get; set; }
    }
    
    public class NormalLevel: LevelData {}
    
    public class EliteLevel: LevelData {}
}
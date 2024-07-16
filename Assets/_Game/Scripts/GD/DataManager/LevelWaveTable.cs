using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration.Attributes;
using UnityEngine;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelWaveTable: DataTable<LevelWaveTableRecord>
    {
        private Dictionary<string, Dictionary<string, List<LevelData>>> _normalLevelDataSource = new();
        private Dictionary<string, Dictionary<string, List<LevelData>>> _eliteLevelDataSource = new();
        
        protected override void HandleLoadedRecords(List<LevelWaveTableRecord> rawRecords)
        {
            _normalLevelDataSource.Clear();
            _eliteLevelDataSource.Clear();

            foreach (var record in rawRecords.Skip(1))
            {
                var parts = record.Level.Split('.');
                if (parts.Length != 3) continue;

                var fullId = record.Level;
                var stageId = parts[0];
                var levelId = string.Join('.', parts.Take(2));
                var idx = parts[2];

                // normal
                if (!_normalLevelDataSource.ContainsKey(levelId))
                {
                    _normalLevelDataSource[levelId] = new();
                }

                if (!_normalLevelDataSource[levelId].ContainsKey(idx))
                {
                    _normalLevelDataSource[levelId][idx] = new();
                }

                if (!string.IsNullOrEmpty(record.NormalEnemyId))
                {
                    _normalLevelDataSource[levelId][idx].Add(new NormalLevel()
                    {
                        Stage = parts[0],
                        Level = parts[1],
                        EnemyId = record.NormalEnemyId.Split(',').Select(v => v.Trim()).ToList(),
                        TimeOffset = record.NormalTimeOffset,
                        TotalPower = record.NormalTotalPower,
                    });
                }

                // elite

                if (!string.IsNullOrEmpty(record.EliteEnemyId))
                {
                    if (!_eliteLevelDataSource.ContainsKey(stageId))
                    {
                        _eliteLevelDataSource[stageId] = new();
                    }

                    if (!_eliteLevelDataSource[stageId].ContainsKey(record.Level))
                    {
                        _eliteLevelDataSource[stageId][record.Level] = new();
                    }
                    _eliteLevelDataSource[stageId][record.Level].Add(new EliteLevel()
                    {
                        Stage = parts[0],
                        Level = parts[1],
                        EnemyId = record.EliteEnemyId.Split(',').Select(v => v.Trim()).ToList(),
                        TimeOffset = record.EliteTimeOffset,
                        TotalPower = record.EliteTotalPower,
                    });
                }
            }
        }

        public List<LevelData> GetLevelData(string stageId, string floorId)
        {
            var list = _normalLevelDataSource[$"{stageId}.{floorId}"];
            return list[Random.Range(1, list.Count).ToString()];
        }

        public List<LevelData> GetStageEliteData(string stageId)
        {
            var list = _eliteLevelDataSource[$"{stageId}"];
            return list[Random.Range(1, list.Count).ToString()];
        }

        public LevelWaveTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class LevelWaveTableRecord
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
    
    /// <summary>
    /// 
    /// </summary>
    public class LevelData
    {
        public string Stage { get; set; }
        public string Level { get; set; }
        public string TimeOffset { get; set; }
        public string TotalPower { get; set; }
        public List<string> EnemyId { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class NormalLevel: LevelData {}
    
    /// <summary>
    /// 
    /// </summary>
    public class EliteLevel: LevelData {}
}
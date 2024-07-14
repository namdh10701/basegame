using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.Parser;
using UnityEngine;

namespace _Game.Scripts.GD
{
    public class GameLevelManager: DataManager<RawLevelData>
    {
        public static GameLevelManager Instance = new ();
        protected override string DownloadUrl => "https://docs.google.com/spreadsheets/d/16zvsN6iALnKVByPfI9BGuvyW44DHVhBZVg07CDoUyOY/edit?gid=755631495#gid=755631495";
        protected override string DataFileName => "WaveLevel";
        
        private Dictionary<string, Dictionary<string, List<LevelData>>> _normalLevelDataSource = new();
        private Dictionary<string, Dictionary<string, List<LevelData>>> _eliteLevelDataSource = new();
        
        protected override void HandleLoadedRecords(List<RawLevelData> rawRecords)
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
    }
}
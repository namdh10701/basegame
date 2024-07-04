using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using _Base.Scripts.Utils;
using CsvHelper;
using Fusion.LagCompensation;
using UnityEngine;

namespace _Game.Scripts.GD.Parser
{
    public class Parser
    {
        public static List<TRecordClass> GetRecords<TRecordClass>(FileStream filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TRecordClass>().ToList();
        }
        
        public static List<TRecordClass> GetRecords<TRecordClass>(string csvContent)
        {
            using var reader = new StringReader(csvContent);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TRecordClass>().ToList();
        }
    }

    public class GameLevelManager
    {
        public static readonly string DataFileName = "WaveLevel";
        private static Dictionary<string, Dictionary<string, List<LevelData>>> _normalLevelDataSource = new ();
        private static Dictionary<string, Dictionary<string, List<LevelData>>> _eliteLevelDataSource = new ();
        
        public static async void LoadData()
        {
            _normalLevelDataSource.Clear();
            _eliteLevelDataSource.Clear();
            
            var filePath = GetFilePath(DataFileName);
            await GSheetDownloader.Download(
                "https://docs.google.com/spreadsheets/d/16zvsN6iALnKVByPfI9BGuvyW44DHVhBZVg07CDoUyOY/edit?gid=755631495#gid=755631495",
                filePath);
            await using var stream = File.OpenRead(filePath);
            
            var rawRecords = Parser.GetRecords<RawLevelData>(stream);

            var idxElite = 0;
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
                    _normalLevelDataSource[levelId] = new ();
                }
                
                if (!_normalLevelDataSource[levelId].ContainsKey(idx))
                {
                    _normalLevelDataSource[levelId][idx] = new ();
                    idxElite++;
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
                        _eliteLevelDataSource[stageId] = new ();
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

        private static string GetFilePath(string dataFileName) => Path.Combine(Application.persistentDataPath, "Data", dataFileName);

        public static List<LevelData> GetLevelData(string stageId, string levelId)
        {
            var list =  _normalLevelDataSource[$"{stageId}.{levelId}"];
            return list[Random.Range(1, list.Count).ToString()];
        }
        
        public static List<LevelData> GetStageEliteData(string stageId)
        {
            var list =  _eliteLevelDataSource[$"{stageId}"];
            return list[Random.Range(1, list.Count).ToString()];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace _Game.Scripts.GD
{
    [DefaultExecutionOrder(-10000)]
    public class LevelDesignConfigLoader : MonoBehaviour
    {
        public static LevelDesignConfigLoader Instance;
        public event Action OnLoaded;
        public string KEY = "AIzaSyAgCbSrEuvfjkYcPUtQN9hYv4TzNtgOS8A";
        public string DOC_ID = "16zvsN6iALnKVByPfI9BGuvyW44DHVhBZVg07CDoUyOY";
        [HideInInspector] public List<LevelDesignConfig> LevelDesignConfigs;

        private async void Awake()
        {
            Instance = this; DontDestroyOnLoad(this);
        }
        public async Task Load()
        {
            Debug.Log("Loaded Level Design: 1");
            LevelDesignConfigs = await GetConfigMap(getSheetData("level_Wave Design"));
            OnLoaded?.Invoke();
            Debug.Log("Loaded Level Design: ");
/*            foreach (LevelDesignConfig config in LevelDesignConfigs)
            {
                Debug.Log(config.ToString());
            }*/
        }

        private string getSheetData(string sheetName)
            => $"https://sheets.googleapis.com/v4/spreadsheets/{DOC_ID}/values/{sheetName}?alt=json&key={KEY}";

        private async Task<GSheetData> GetGSheetData(string url)
        {
            var jsonContent = await GetHttpResponse(url);
            var gSheetData = JsonConvert.DeserializeObject<GSheetData>(jsonContent);
            return gSheetData;
        }


        private async Task<List<LevelDesignConfig>> GetConfigMap(string url, int headerCount = 1)
        {
            var list = new List<LevelDesignConfig>();
            var gSheetData = await GetGSheetData(url);
            Debug.Log(gSheetData);

            var fields = gSheetData.values.First();

            foreach (var row in gSheetData.values.Skip(headerCount))
            {
                if (!row.Any())
                {
                    continue;
                }

                var levelDesignConfig = new LevelDesignConfig();
                /*levelDesignConfig.stage = row[0];
                levelDesignConfig.enemy_ids = new List<string>();
                levelDesignConfig.time_offset = float.Parse(GetValueOrDefault(row, 1), CultureInfo.InvariantCulture);
                levelDesignConfig.total_power = int.Parse(GetValueOrDefault(row, 2));
                string enemyIds = GetValueOrDefault(row, 3);*/
                //string[] ids = enemyIds.Split(',');
                /*foreach (var id in ids)
                {
                    levelDesignConfig.enemy_ids.Add(id);
                }*/
                list.Add(levelDesignConfig);
            }

            return list;
        }

        private string GetValueOrDefault(List<string> row, int index)
        {
            if (index < row.Count)
            {
                var value = row[index];
                return string.IsNullOrEmpty(value) ? null : value;
            }
            return null;
        }

        async Task<string> GetHttpResponse(string url)
        {
            return (await UnityWebRequest.Get(url).SendWebRequest()).downloadHandler.text;
        }
    }
}

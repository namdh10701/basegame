using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Game.Scripts.GD
{
    public class GSheetData
    {
        public string range;
        public string majorDimension;
        public List<List<string>> values;
    }

    [DefaultExecutionOrder(-10000)]
    public class GDConfigLoader: MonoBehaviour
    {
        public static GDConfigLoader Instance;

        public event Action OnLoaded;

        public string KEY = "AIzaSyAgCbSrEuvfjkYcPUtQN9hYv4TzNtgOS8A";
        public string DOC_ID = "1M91hXkFM9BvP5SsfMKz-oDndDaOx-hJLfFTE39kfEJM";
        // public Dictionary<string, CannonConfig> Cannon { get; private set; }
        // public Dictionary<string, AmmoConfig> Ammo { get; private set; }
        
        private Dictionary<string, Dictionary<string, string>> CannonMap { get; set; }
        private Dictionary<string, Dictionary<string, string>> AmmoMap { get; set; }
        private Dictionary<string, Dictionary<string, string>> EnemyMap { get; set; }
        private Dictionary<string, Dictionary<string, string>> ShipMap { get; set; }
        
        public Dictionary<string, CannonConfig> Cannons { get; } = new ();
        public Dictionary<string, AmmoConfig> Ammos { get; } = new ();
        public Dictionary<string, EnemyConfig> Enemies { get; } = new ();
        public Dictionary<string, ShipConfig> Ships { get; } = new ();

        private async void Awake()
        {
            Instance = this;
        }
        public async Task Load()
        {
            // Cannon = await GetConfig<CannonConfig>(getSheetData("Cannon"));
            // Ammo = await GetConfig<AmmoConfig>(getSheetData("Ammo"));

            CannonMap = await GetConfigMap(getSheetData("Cannon"));
            AmmoMap = await GetConfigMap(getSheetData("Ammo"));
            EnemyMap = await GetConfigMap(getSheetData("Monster"));
            ShipMap = await GetConfigMap(getSheetData("Ship"));

            SetLocalData();
            
            OnLoaded?.Invoke();
            // SceneManager.LoadScene(SceneToLoadWhenComplete);
        }

        private void SetLocalData()
        {
            // var gdConfigs = Resources.LoadAll<ScriptableObject>("").OfType<IGDConfig>();
            // foreach (var cfg in gdConfigs)
            // {
            //     cfg.ApplyGDConfig();
            // }

            LoadAll();
        }
        
        [ContextMenu("LoadAll")]
        private void LoadAll()
        {
            // Load cannon config
            foreach (var (key, properties) in GDConfigLoader.Instance.CannonMap)
            {
                var so = Resources.Load<CannonConfig>($"Configs/Cannon/Cannon_{key}");
                if (!so) continue;
                Load(so, properties);

                Cannons[key] = so;
            }
            
            // Load ammo config
            foreach (var (key, properties) in GDConfigLoader.Instance.AmmoMap)
            {
                var so = Resources.Load<AmmoConfig>($"Configs/Ammo/Ammo_{key}");
                if (!so) continue;
                Load(so, properties);
                
                Ammos[key] = so;
            }
            
            // Load enemy config
            foreach (var (key, properties) in GDConfigLoader.Instance.EnemyMap)
            {
                var so = Resources.Load<EnemyConfig>($"Configs/Enemy/Enemy_{key}");
                if (!so) continue;
                Load(so, properties);
                
                Enemies[key] = so;
            }
            
            // Load ship config
            foreach (var (key, properties) in GDConfigLoader.Instance.ShipMap)
            {
                var so = Resources.Load<ShipConfig>($"Configs/Ship/Ship_{key}");
                if (!so) continue;
                Load(so, properties);
                
                Ships[key] = so;
            }
        }
        
        public static void Load(ScriptableObject so, Dictionary<string, string> properties)
        {
            Debug.Log($"Load SO: {so.name} | {string.Join(Environment.NewLine, properties)}");
            foreach (var fieldInfo in so.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = properties[fieldInfo.Name];
                if (fieldInfo.FieldType == typeof(float))
                {
                    value ??= "0";
                    value = float.Parse(value.ToString());
                }
                fieldInfo.SetValue(so, value);
            }
        }
        
        

        private string getSheetData(string sheetName) 
            => $"https://sheets.googleapis.com/v4/spreadsheets/{DOC_ID}/values/{sheetName}?alt=json&key={KEY}";

        private async Task<GSheetData> GetGSheetData(string url)
        {
            var jsonContent = await GetHttpResponse(url);
            var gSheetData = JsonConvert.DeserializeObject<GSheetData>(jsonContent);
            return gSheetData;
        }


        private async Task<Dictionary<string, Dictionary<string, string>>> GetConfigMap(string url, int headerCount = 1)
        {
            var dict = new Dictionary<string, Dictionary<string, string>>();
            var gSheetData = await GetGSheetData(url);
            Debug.Log(gSheetData);

            var fields = gSheetData.values.First();

            foreach (var list in gSheetData.values.Skip(headerCount))
            {
                var colIdx = 0;
                dict[list[0]] = new Dictionary<string, string>();
                foreach (var fieldInfo in fields)
                {
                    string value = null;

                    if (colIdx < list.Count)
                    {
                        value = list[colIdx++];

                        if (string.IsNullOrEmpty(value))
                        {
                            value = null;
                        }
                    }

                    dict[list[0]][fieldInfo] = value;
                }
            }

            return dict;
        }
        
        private async Task<Dictionary<string, T>> GetConfigs<T>(string url, int headerCount = 1)
        {
            var dict = new Dictionary<string, T>();
            var gSheetData = await GetGSheetData(url);
            Debug.Log(gSheetData);

            foreach (var list in gSheetData.values.Skip(headerCount))
            {
                var colIdx = 0;
                dict[list[0]] = Activator.CreateInstance<T>();
                foreach (var fieldInfo in dict[list[0]].GetType().GetFields())
                {
                    object value = "0";

                    if (colIdx < list.Count)
                    {
                        value = list[colIdx++];

                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = "0";
                        }
                    }

                    if (fieldInfo.FieldType == typeof(float))
                    {
                        value = float.Parse(value.ToString());
                    }

                    fieldInfo.SetValue(dict[list[0]], value);
                }
            }

            return dict;
        }
        
        async Task<string> GetHttpResponse(string url) {
            return (await UnityWebRequest.Get(url).SendWebRequest()).downloadHandler.text;
        }
    }
}

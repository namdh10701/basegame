using _Base.Scripts.EventSystem;
using _Base.Scripts.Shared;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;

        public Map CurrentMap { get; private set; }

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Map"))
            {
                var mapJson = PlayerPrefs.GetString("Map");
                var map = JsonUtility.FromJson<Map>(mapJson);
                // using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)) && map.IsLastNodePassed)
                {
                    // payer has already reached the boss, generate a new map
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            GlobalEvent.Send(GlobalData.MAP_CHANGED);
        }

        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonUtility.ToJson(CurrentMap);
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
        public MapViewUI mapView;
        public void OnGamePassed()
        {
            CurrentMap.IsLastNodeLocked = false;
            CurrentMap.IsLastNodePassed = true;
            if (CurrentMap.path.Count == CurrentMap.BossNodeLayer)
            {
                GenerateNewMap();
            }
            SaveMap();
            mapView.UpdateVisual();
        }
        public void OnGameStart()
        {
            CurrentMap.IsLastNodeLocked = true;
            SaveMap();

            mapView.UpdateVisual();
        }

    }
}

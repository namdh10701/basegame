using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Features.WorldMap
{
    // [ExecuteAlways]
    public class WorldMapContent : MonoBehaviour
    {
        public GameObject MapStartPrefab;

        public List<GameObject> MapLoopPrefabs = new ();

        public int LoopCount = 5;

        public GameObject MapNodePrefab;

        public VerticalLayoutGroup MapContainer;

        private void Awake()
        {
            if (!MapStartPrefab)
            {
                return;
            }

            if (!PlayerPrefs.HasKey("currentStage"))
            {
                PlayerPrefs.SetString("currentStage", "0001");
            }

            // foreach (Transform child in MapContainer.transform) {
            //     Destroy(child.gameObject);
            // }

            // var maps = new List<GameObject>() { StartMap };
            // for (var i = 0; i < LoopCount; i++)
            // {
            //     // maps.AddRange(LoopMaps);
            //
            //     for (var loopMapIdx = LoopMaps.Count - 1; loopMapIdx >= 0; loopMapIdx--)
            //     {
            //         maps.Add(LoopMaps[loopMapIdx]);
            //     }
            // }
            //
            // maps.ForEach(map =>
            // {
            //     Instantiate(map, MapContainer.transform);
            // });

            var stageId = 1;
            var startMap = Instantiate(MapStartPrefab, MapContainer.transform);
            foreach (var pin in startMap.GetComponentsInChildren<WorldMapPin>())
            {
                var node = Instantiate(MapNodePrefab, pin.transform);
                var rect = (RectTransform)node.transform;
                rect.anchoredPosition = Vector2.zero;
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                
                node.GetComponentInChildren<WorldMapNodeSingle>().Id = $"{stageId++}".PadLeft(4, '0');
            }

            for (int i = 0; i < LoopCount; i++)
            {
                MapLoopPrefabs.ForEach(map =>
                {
                    var newMap = Instantiate(map, MapContainer.transform);
                    newMap.transform.SetAsFirstSibling();
                    
                    foreach (var pin in newMap.GetComponentsInChildren<WorldMapPin>())
                    {
                        var node = Instantiate(MapNodePrefab, pin.transform);
                        var rect = (RectTransform)node.transform;
                        rect.anchoredPosition = Vector2.zero;
                        rect.anchorMin = new Vector2(0.5f, 0.5f);
                        rect.anchorMax = new Vector2(0.5f, 0.5f);
                        rect.pivot = new Vector2(0.5f, 0.5f);
                        
                        node.GetComponentInChildren<WorldMapNodeSingle>().Id = $"{stageId++}".PadLeft(4, '0');

                    }
                });                
            }
            
        }

        private void Start() { }
    }
}

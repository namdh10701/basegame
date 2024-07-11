using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Features.MyShip.GridSystem
{
    public class CellRayTracker : MonoBehaviour
    {
        public event Action<SlotCell, SlotCell> OnTrackCellChanged;

        public EventSystem EventSystem;
        public GraphicRaycaster Raycaster;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _results = new();

        private SlotCell _baseCell = null;
        // private GridLayoutGroup _gridLayoutGroup;

        public SlotCell FeedData(Vector2 point)
        {
            if (_pointerEventData == null)
            {
                return null;
            }
            
            _pointerEventData.position = point;
            
            _results.Clear();
            Raycaster.Raycast(_pointerEventData, _results);

            SlotCell cell = null;
            foreach (var ray in _results)
            {
                var foundCell = ray.gameObject.GetComponent<SlotCell>();
                if (!foundCell || !foundCell.IsAvailable) continue;
                cell = foundCell;
                break;
            }
            
            if (_baseCell == cell) return null;

            var prevCell = _baseCell;
            _baseCell = cell;

            OnTrackCellChanged?.Invoke(prevCell, _baseCell);
            return _baseCell;
        }
        
        public SlotCell FindSlotCell(Vector2 point)
        {
            if (_pointerEventData == null)
            {
                return null;
            }
            
            _pointerEventData.position = point;
            
            _results.Clear();
            Raycaster.Raycast(_pointerEventData, _results);

            SlotCell cell = null;
            foreach (var ray in _results)
            {
                var foundCell = ray.gameObject.GetComponentInChildren<SlotCell>();
                if (!foundCell || !foundCell.IsAvailable) continue;
                Debug.Log("ray.gameObject: " + ray.gameObject);
                cell = foundCell;
                break;
            }
            
            if (_baseCell == cell) return null;

            var prevCell = _baseCell;
            _baseCell = cell;

            OnTrackCellChanged?.Invoke(prevCell, _baseCell);
            return _baseCell;
        }

        private void Awake()
        {
            if (!EventSystem)
            {
                EventSystem = FindObjectOfType<EventSystem>();
            }

            if (!Raycaster)
            {
                Raycaster = GetComponent<GraphicRaycaster>();
            }

            _pointerEventData = new PointerEventData(EventSystem);
        }
    }
}

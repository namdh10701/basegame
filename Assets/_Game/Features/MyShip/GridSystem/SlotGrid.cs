using System;
using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.MyShip.GridSystem
{
    [Binding]
    public class SlotGrid : RootViewModel
    {
        // public GridLayoutGroup GridLayoutGroup;
        // private Dictionary<Vector2Int, SlotCell> _cellPositions = new ();
        public List<SlotCell> AvailableSlots = new ();
        private List<SlotCell> _highLightedPositions = new ();
        public ItemShape shape;

        // public CellRayTracker cellTracker;
        public GridLayoutGroup _gridLayoutGroup;

        // public void OnCellReady(SlotCell cell)
        // {
        //     _cellPositions[cell.Position] = cell;
        // }

        private void Awake()
        {
            // cellTracker.OnTrackCellChanged += OnTrackCellChanged;

            if (!_gridLayoutGroup)
            {
                _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            }
        }
        
        IEnumerator Start() {
            yield return null;
            Init();
        }

        private void Init()
        {
            // var rect = transform as RectTransform;
            // Debug.Log(rect!.rect.width);

            // _cellPositions.Clear();
            // for (var idx = 0; idx < transform.childCount; idx++)
            // {
            //     var cell = transform.GetChild(idx).GetComponent<SlotCell>();
            //     var pos = GridLayoutGroupUtils.GetCellPosition(_gridLayoutGroup, idx);
            //     _cellPositions[pos] = cell;
            // }
        }

        protected virtual void LateUpdate()
        {
            // if (!shape) return;
            //
            // var centerVector = GridLayoutGroupUtils.FindCenterVector(shape.Data);
            //
            // cellTracker.FeedData(Input.mousePosition + new Vector3(centerVector.x * SlotCell.WIDTH / 2f, centerVector.y * SlotCell.HEIGHT / 2f));
        }

        public SlotCell GetCell(int siblingIndex)
        {
            return _gridLayoutGroup.transform.GetChild(siblingIndex).GetComponentInChildren<SlotCell>();
        }
        
        public SlotCell GetCell(Vector2Int position)
        {
            var idx = GridLayoutGroupUtils.GetSiblingIndex(_gridLayoutGroup, position);
            if (idx == -1)
            {
                return null;
            }
            return GetCell(idx);
        }


        private void OnTrackCellChanged(SlotCell prev, SlotCell current)
        {
            AvailableSlots.Clear();
            
            foreach (var cell in _highLightedPositions)
            {
                cell.IsHighLight = false;
            }
            _highLightedPositions.Clear();

            if (!current) return;

            if (!shape || shape.Data.Count == 0) return;
            
            Debug.Log("Hit " + current.Position);

            var isItemFitted = true;
            var highLight = Vector2IntArrayUtils.Shift(shape.Data, current.Position);
            foreach (var point in highLight)
            {
                Debug.Log("HighLight " + point);
                
                var cell = GetCell(point);

                if (cell == null)
                {
                    isItemFitted = false;
                    continue;
                }
                
                cell.IsHighLight = true;
                _highLightedPositions.Add(cell);
            }

            if (isItemFitted)
            {
                OnReadyToPlaceItem(_highLightedPositions);
            }
        }

        private void OnReadyToPlaceItem(List<SlotCell> highLightedPositions)
        {
            AvailableSlots.AddRange(highLightedPositions);
        }
    }
}

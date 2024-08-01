using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GridItemView : MonoBehaviour
    {
        [SerializeField] private IGridItem item;
        [SerializeField] private IGridItemView itemView;
        [SerializeField] GridItemState lastState;
        public void Init(IGridItem item)
        {
            itemView = GetComponent<IGridItemView>();
            itemView.Init(item);
            this.item = item;
            item.GridItemStateManager.OnStateEntered += OnStateEntered;
        }

        void OnStateEntered(GridItemState state)
        {
            switch (state)
            {
                case GridItemState.Active:
                    itemView.HandleActive();
                    break;
                case GridItemState.Broken:
                    itemView.HandleBroken();
                    break;
            }
            lastState = state;
        }


    }
}
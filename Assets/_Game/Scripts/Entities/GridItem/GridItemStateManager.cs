using _Game.Scripts.Entities;
using System;
using UnityEngine;

namespace _Game.Features.Gameplay
{

    public class GridItemStateManager : MonoBehaviour
    {
        public IGridItem gridItem;
        private GridItemState state;
        private GridItemState lastState;

        public GridItemState GridItemState
        {
            get => state;
            set
            {
                if (state != value)
                {
                    lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnStateEnter(state);
                }
            }
        }

        public Action<GridItemState> OnStateEntered;

        protected virtual void OnStateEnter(GridItemState state)
        {
            // Default or shared implementation for handling state changes
            switch (state)
            {
                case GridItemState.Active:
                    gridItem.Active();
                    break;
                case GridItemState.Broken:
                    gridItem.OnBroken();
                    break;
            }
        }
    }
}
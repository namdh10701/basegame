using System.Collections.Generic;
using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using _Game.Features.MyShip.GridSystem;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    [Binding]
    public class ShipSetupItem : RootViewModel//, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public List<Vector2Int> Positions;
        public ItemShape Shape;
        public InventoryItem InventoryItem;

        #region Binding Prop: Removable

        /// <summary>
        /// Removable
        /// </summary>
        [Binding]
        public bool Removable
        {
            get => _removable;
            set
            {
                if (Equals(_removable, value))
                {
                    return;
                }

                _removable = value;
                OnPropertyChanged(nameof(Removable));
            }
        }

        private bool _removable;

        #endregion

        [Binding]
        public void Remove()
        {
            var shipEditSheet = IOC.Resolve<NewShipEditSheet>();

            MyShip.StashItem firstEmptyStashItem = null;
            foreach (var stashItem in shipEditSheet.StashItems)
            {
                if (stashItem.InventoryItem == null)
                {
                    firstEmptyStashItem = stashItem;
                    break;
                }
            }

            if (firstEmptyStashItem == null)
            {
                return;
            }
            
            firstEmptyStashItem.InventoryItem = InventoryItem;
            Destroy(gameObject);
            shipEditSheet.SaveSetupProfile();
        }
    }
}

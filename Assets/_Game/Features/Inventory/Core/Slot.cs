using UnityWeld.Binding;

namespace _Game.Features.Inventory.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Binding]
    public class Slot
    {
        public bool IsDisabled { get; set; } = false;

        public bool IsHidden { get; set; } = false;

        public InventoryItem Value { get; set; }
    }
}
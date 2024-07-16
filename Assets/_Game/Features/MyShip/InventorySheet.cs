using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using UnityWeld.Binding;

namespace _Game.Features.MyShip
{
    [Binding]
    public class InventorySheet : InventoryViewModel
    {
        protected override void Awake()
        {
            base.Awake();
            IOC.Register(this);
        }
    }
}
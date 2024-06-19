using _Game.Features.Inventory;
using UnityWeld.Binding;

namespace Sava.Binding.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    [Adapter(typeof(int), typeof(ItemType))]
    public class IntToItemTypeAdapter : IAdapter 
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            int value = (int)valueIn;

            return (ItemType) value;
        }
    }
}

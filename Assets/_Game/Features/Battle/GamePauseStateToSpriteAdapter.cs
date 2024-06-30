using UnityEngine;

namespace UnityWeld.Binding.Adapters
{
    /// <summary>
    /// Adapter that inverts the value of the bound boolean property.
    /// </summary>
    [Adapter(typeof(bool), typeof(Sprite))]
    public class GamePauseStateToSpriteAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            var isPause = !(bool)valueIn;
            return isPause ? Resources.Load("Images/btn_x2") : Resources.Load("Images/btn_pause");
        }
    }
}

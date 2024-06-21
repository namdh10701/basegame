using _Base.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Features.SeaMapNodeInfoPopup
{
    [Binding]
    public class SeaMapNodeInfoPopup : Popup
    {
        public enum Style
        {
            Normal,
            Boss
        }

        public Style style = Style.Normal;

        [Binding] 
        public bool IsBossStyle => style == Style.Boss;
    }
}
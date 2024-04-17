namespace _Game.Scripts.UI
{
    using _Game.Scripts.GameContext;
    using Slash.Unity.DataBind.Foundation.Setters;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///     Set the fill amount of an Image depending on the string data value.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] ManaFillSetter")]
    public class ManaFillSetter : ComponentSingleSetter<Image, float>
    {
        protected override void UpdateTargetValue(Image target, float value)
        {
            target.fillAmount = value / GlobalContext.PlayerContext.MaxManaPoint;
        }
    }
}
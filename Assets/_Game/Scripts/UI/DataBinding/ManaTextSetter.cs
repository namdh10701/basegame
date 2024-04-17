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
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] ManaTextSetter")]
    public class ManaTextSetter : ComponentSingleSetter<TextMeshProUGUI, float>
    {
        protected override void UpdateTargetValue(TextMeshProUGUI target, float value)
        {
            target.text = $"{value}/{GlobalContext.PlayerContext.MaxManaPoint}";
        }
    }
}
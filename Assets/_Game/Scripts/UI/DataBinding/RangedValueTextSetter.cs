using _Base.Scripts.RPG.Stats;
using Slash.Unity.DataBind.Foundation.Setters;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI.DataBinding
{
    /// <summary>
    ///     Set the fill amount of an Image depending on the string data value.
    /// </summary>
    [AddComponentMenu("[Game DB] RangedValueTextSetter")]
    public class RangedValueTextSetter : ComponentSingleSetter<TextMeshProUGUI, RangedValue>
    {
        protected override void UpdateTargetValue(TextMeshProUGUI target, RangedValue value)
        {
            target.text = $"{(int)(value?.Value ?? 0)}/{(int)(value?.MaxValue ?? 0)}";
        }
    }
}
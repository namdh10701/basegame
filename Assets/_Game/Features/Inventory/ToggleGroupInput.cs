using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.Features.Inventory
{
    public class ToggleGroupInputEvent : UnityEvent<int>
    {
    }

    public class ToggleGroupInput : MonoBehaviour
    {
        public int Value { get; set; }
        public ToggleGroupInputEvent onValueChanged = new ToggleGroupInputEvent();

        private Toggle[] _toggles;

        private void Awake()
        {
            _toggles = GetComponentsInChildren<Toggle>();
            for (var index = 0; index < _toggles.Length; index++)
            {
                var toggle = _toggles[index];
                var index1 = index;
                toggle.onValueChanged.AddListener(selected =>
                {
                    if (selected)
                    {
                        OnValueChanged(index1);
                    }
                });
            }
        }

        // private void OnDestroy()
        // {
        //     foreach (var toggle in _toggles)
        //     {
        //         toggle.onValueChanged.RemoveListener(OnValueChanged);
        //     }
        // }

        private void OnValueChanged(int selectedIndex)
        {
            Value = selectedIndex;
            onValueChanged?.Invoke(selectedIndex);
            Debug.Log("Selected: " + Value);
        }
    }
}
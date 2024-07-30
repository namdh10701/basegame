using _Game.Features.InventoryCustomScreen;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.Features.Inventory
{
    public class ToggleGroupInputEvent : UnityEvent<int>
    {
    }

    [RequireComponent(typeof(ToggleGroup))]
    public class ToggleGroupInput : MonoBehaviour
    {
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateView();
            }
        }

        public ToggleGroupInputEvent onValueChanged = new ToggleGroupInputEvent();

        private Toggle[] _toggles;
        private ToggleGroup _toggleGroup;
        private int _value;
        private bool _suspendValueChangeListeners = false;

        private void OnEnable()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
            _toggles = GetComponentsInChildren<Toggle>();
            for (var index = 0; index < _toggles.Length; index++)
            {
                var toggle = _toggles[index];
                // _toggleGroup.RegisterToggle(toggle);
                toggle.group = _toggleGroup;

                var index1 = index;
                toggle.onValueChanged.AddListener(selected =>
                {
                    if (_suspendValueChangeListeners)
                    {
                        return;
                    }
                    if (selected)
                    {
                        OnValueChanged(index1);
                    }
                });
            }
            UpdateView();
        }

        public void Setup()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
            _toggles = GetComponentsInChildren<Toggle>();
            for (var index = 0; index < _toggles.Length; index++)
            {
                var toggle = _toggles[index];
                // _toggleGroup.RegisterToggle(toggle);
                toggle.group = _toggleGroup;

                var index1 = index;
                toggle.onValueChanged.AddListener(selected =>
                {
                    if (_suspendValueChangeListeners)
                    {
                        return;
                    }
                    if (selected)
                    {
                        OnValueChanged(index1);
                    }
                });
            }
            UpdateView();
        }

        private void UpdateView()
        {
            _suspendValueChangeListeners = true;

            if (_toggles != null)
            {
                _toggles[Value].isOn = true;
            }

            _suspendValueChangeListeners = false;
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

        public void RemoveToggleItem(int index)
        {
            if (_toggles == null || _toggles.Length == 0 || index < 0 || index >= _toggles.Length)
            {
                return;
            }

            Toggle[] newToggles = new Toggle[_toggles.Length - 1];

            for (int i = 0, j = 0; i < _toggles.Length; i++)
            {
                if (i != index)
                {
                    newToggles[j++] = _toggles[i];
                }
            }

            _toggles = newToggles;
        }
    }
}
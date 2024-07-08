using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace _Game.Features.Inventory
{
    public class ButtonGroupInputEvent : UnityEvent<int>
    {
    }
    public class ButtonGroupInput : MonoBehaviour
    {
        public ButtonGroupInputEvent onClick = new ButtonGroupInputEvent();
        private Button[] _buttons;
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        void Awake()
        {
            _buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                var index = i;
                button.onClick.AddListener(() =>
                {
                    OnValueChanged(index);
                    Interactable(false);
                });
            }
        }

        private void OnValueChanged(int index)
        {
            Value = index;
            onClick?.Invoke(index);
        }

        public void Interactable(bool enable)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i == Value)
                    continue;

                _buttons[i].interactable = enable;
            }
        }
    }
}
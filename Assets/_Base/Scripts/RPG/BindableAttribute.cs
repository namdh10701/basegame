using System;
using Slash.Unity.DataBind.Core.Data;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [Serializable]
    public abstract class BindableAttribute<T>: Attribute<T>
    {
        private readonly Property<T> _valueProperty = new();

        public override T Value
        {
            get => _valueProperty.Value;
            set => _valueProperty.Value = value;
        }
    }
}
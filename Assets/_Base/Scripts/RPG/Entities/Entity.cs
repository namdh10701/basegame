using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = _Base.Scripts.RPG.Attributes.Attribute;

namespace _Base.Scripts.RPG.Entities
{
    [Serializable]
    public class AttributeDictionary : SerializableDictionary<Type, string> { }

    [Serializable]
    public class StringColorDictionary : SerializableDictionary<string, Color>
    {
        public StringColorDictionary(IDictionary<string, Color> dict) : base(dict) { }
    }

    [DisallowMultipleComponent]
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity")]
        public Rigidbody2D body;
        public abstract _Game.Scripts.Stats Stats { get; }

        protected virtual void Awake()
        {
            LoadStats();
            LoadModifiers();
            ApplyStats();
        }
        protected abstract void LoadStats();
        protected abstract void LoadModifiers();
        protected abstract void ApplyStats();
    }
}
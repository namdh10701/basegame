using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using Unity.VisualScripting;
using _Base.Scripts.Utils.Extensions;

using UnityEngine;
using Attribute = _Base.Scripts.RPG.Attributes.Attribute;

namespace _Base.Scripts.RPG.Entities
{
    [Serializable]
    public class AttributeDictionary : SerializableDictionary<Type, string> {}
    
    [Serializable]
    public class StringColorDictionary : SerializableDictionary<string, Color>
    {
        public StringColorDictionary(IDictionary<string, Color> dict) : base(dict) {}
    }
    
    [DisallowMultipleComponent]
    public abstract class Entity: MonoBehaviour, IEntity
    {
        public Transform attributeHolder;
        public Transform effectHolder;
        public Transform carryingEffectHolder;
        
        [field: SerializeReference] public List<Attribute> Attributes { get; set; } = new ();

        [SerializeField]
        StringStringDictionary m_stringStringDictionary = null;
        public IDictionary<string, string> StringStringDictionary
        {
            get { return m_stringStringDictionary; }
            set { m_stringStringDictionary.CopyFrom (value); }
        }
        
        [SerializeField]
        AttributeDictionary xxx = null;
        public IDictionary<Type, string> XXX
        {
            get { return xxx; }
            set { xxx.CopyFrom (value); }
        }

        // public List<Attribute> Attributes => attributeHolder.GetComponents<Attribute>().ToList();
        public List<IEffect> Effects => effectHolder.GetComponents<IEffect>().ToList();
        public List<IEffect> CarryingEffects => carryingEffectHolder.GetComponents<IEffect>().ToList();
        
        public TAttribute GetAttribute<TAttribute>() where TAttribute : IAttribute
        {
            return (TAttribute)(object)Attributes.Find(v => v.GetType() == typeof(TAttribute));
        }
        
        public void SetAttribute<TAttribute>(TAttribute attribute) where TAttribute : MonoBehaviour, IAttribute
        {
            attributeHolder.gameObject.AddComponent(attribute);
        }
        
        public TEffect AddEffect<TEffect>() where TEffect: Effect
        {
            return effectHolder.gameObject.AddComponent<TEffect>();
        }
        
        public TEffect AddCarryingEffect<TEffect>() where TEffect: Effect
        {
            return carryingEffectHolder.gameObject.AddComponent<TEffect>();
        }

        private void Awake()
        {
            if (attributeHolder != null)
            {
                foreach (var attribute in Attributes)
                {
                    attribute.OnChanged += (sender, args) =>
                    {
                        
                    };
                }
            }
        }
    }
}
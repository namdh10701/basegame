using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [DisallowMultipleComponent]
    public abstract class Entity: MonoBehaviour, IEntity
    {
        public Transform attributeHolder;
        public Transform effectHolder;
        [field: SerializeReference] public List<Attribute> Attributes { get; set; } = new ();
        
        public TAttribute GetAttribute<TAttribute>() where TAttribute : IAttribute
        {
            return (TAttribute)(object)Attributes.Find(v => v.GetType() == typeof(TAttribute));
        }

        [field: SerializeReference]
        public List<IEffect> Effects => effectHolder.GetComponents<IEffect>().ToList();

        private void Awake()
        {
            if (attributeHolder != null)
            {
                Attributes = attributeHolder.GetComponents<Attribute>().ToList();
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
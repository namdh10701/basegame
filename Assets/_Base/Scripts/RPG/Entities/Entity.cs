using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using UnityEngine;
using Attribute = _Base.Scripts.RPG.Attributes.Attribute;

namespace _Base.Scripts.RPG.Entities
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
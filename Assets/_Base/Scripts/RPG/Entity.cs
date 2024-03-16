using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [DisallowMultipleComponent]
    public abstract class Entity: MonoBehaviour, IEntity
    {
        [field: SerializeReference] public List<Attribute> Attributes { get; set; } = new ();
        
        public TAttribute GetAttribute<TAttribute>() where TAttribute : IAttribute
        {
            return (TAttribute)(object)Attributes.Find(v => v.GetType() == typeof(TAttribute));
        }
    }
}
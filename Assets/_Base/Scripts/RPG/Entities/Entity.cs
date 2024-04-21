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
    public class AttributeDictionary : SerializableDictionary<Type, string> {}
    
    [Serializable]
    public class StringColorDictionary : SerializableDictionary<string, Color>
    {
        public StringColorDictionary(IDictionary<string, Color> dict) : base(dict) {}
    }
    
    [DisallowMultipleComponent]
    public abstract class Entity: MonoBehaviour//, IEntity
    {
        // public Transform attributeHolder;
        // public Transform effectHolder;
        // public Transform carryingEffectHolder;

        public EffectHandler EffectHandler;
        public EntityCollisionDetector EntityCollisionDetector;
        public ICollisionHandler CollisionHandler = new DefaultCollisionHandler();
        
        public abstract _Game.Scripts.Stats Stats { get; }
        
        // [field: SerializeReference] public List<Attribute> Attributes { get; set; } = new ();

        // public List<IEffect> Effects => effectHolder.GetComponents<IEffect>().ToList();
        public List<Effect> OutgoingEffects = new List<Effect>();

        public void SetCollisionObjectChecker(Func<Entity, bool> checker)
        {
            if (EntityCollisionDetector != null)
            {
                EntityCollisionDetector.CollisionObjectChecker = checker;
            }
        }
        
        // public List<IEffect> IncomingEffects => carryingEffectHolder.GetComponents<IEffect>().ToList();
        
        // public TAttribute GetAttribute<TAttribute>() where TAttribute : IAttribute
        // {
        //     return (TAttribute)(object)Attributes.Find(v => v.GetType() == typeof(TAttribute));
        // }
        
        // public void SetAttribute<TAttribute>(TAttribute attribute) where TAttribute : Attribute
        // {
        //     var targetAttr = Attributes.Find(v => v.GetType() == typeof(TAttribute));
        //     if (targetAttr == null)
        //     {
        //         targetAttr = attribute;
        //     }
        //
        //     targetAttr.Value = attribute.Value;
        //     targetAttr.MaxValue = attribute.MaxValue;
        //     targetAttr.MinValue = attribute.MinValue;
        //     // attributeHolder.gameObject.AddComponent(attribute);
        // }
        
        // public TEffect AddEffect<TEffect>() where TEffect: Effect
        // {
        //     return effectHolder.gameObject.AddComponent<TEffect>();
        // }
        
        // public void AddCarryingEffect(IEffect effect)
        // {
        //     // IncomingEffects.Add(effect);
        //     return carryingEffectHolder.gameObject.AddComponent<TEffect>();
        // }
        
        // public TEffect AddEffectedEffect<TEffect>() where TEffect: Effect
        // {
        //     return carryingEffectHolder.gameObject.AddComponent<TEffect>();
        // }

        protected virtual void Awake()
        {
            // if (attributeHolder != null)
            // {
            //     foreach (var attribute in Attributes)
            //     {
            //         attribute.OnChanged += (sender, args) =>
            //         {
            //             
            //         };
            //     }
            // }

            if (EntityCollisionDetector != null)
            {
                EntityCollisionDetector.OnEntityCollisionEnter += OnEntityCollisionEnter;
            }
        }

        private void OnDestroy()
        {
            if (EntityCollisionDetector != null)
            {
                EntityCollisionDetector.OnEntityCollisionEnter -= OnEntityCollisionEnter;
            }
        }

        private void OnEntityCollisionEnter(Entity entity)
        {
            if (CollisionHandler == null)
            {
                return;
            }
            CollisionHandler.Process(this, entity);
        }
    }
}
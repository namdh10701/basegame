using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG.Entities
{
    [DisallowMultipleComponent]
    public abstract class Entity : MonoBehaviour
    {
        public string Id;
        [Header("Entity")]
        public Rigidbody2D body;
        public abstract _Game.Scripts.Stats Stats { get; }

        protected virtual void Awake()
        {
            InitStats();
        }
        protected abstract void LoadStats();
        protected abstract void LoadModifiers();
        protected abstract void ApplyStats();

        public void InitStats()
        {
            LoadStats();
            LoadModifiers();
            ApplyStats();
        }
    }
}
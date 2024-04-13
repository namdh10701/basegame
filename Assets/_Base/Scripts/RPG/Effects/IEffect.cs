using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public interface IEffect
    {
        event EventHandler<EffectEventArgs> OnStart;
        event EventHandler<EffectEventArgs> OnEnd;

        void Process();

        void OnBeforeApply();
        void Apply();
        void ApplyTo(Entity entity);
        void OnAfterApply();

        void NotifyStarted();
        void NotifyEnded();
    }
}
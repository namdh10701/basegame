using System;

namespace _Base.Scripts.RPG.Effects
{
    public interface IEffect
    {
        event EventHandler<EffectEventArgs> OnStart;
        event EventHandler<EffectEventArgs> OnEnd;

        void Process();

        void OnBeforeApply();
        void Apply();
        void OnAfterApply();

        void NotifyStarted();
        void NotifyEnded();
    }
}
using System;

namespace _Base.Scripts.RPG
{
    public interface IEffect
    {
        event EventHandler<EffectEventArgs> OnStarted;
        event EventHandler<EffectEventArgs> OnEnd;
        void Apply();

        void DoStart();
        void DoEnd();

        void NotifyStarted();
        void NotifyEnded();
    }
}
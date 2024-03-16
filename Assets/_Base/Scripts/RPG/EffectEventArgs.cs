using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public class EffectEventArgs: EventArgs
    {
        private object[] _targets;

        public object[] Targets => _targets;
    }
}
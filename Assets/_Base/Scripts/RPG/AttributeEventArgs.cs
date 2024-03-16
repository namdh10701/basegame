using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public class AttributeEventArgs: EventArgs
    {
        private object[] _targets;

        public object[] Targets => _targets;
    }
}
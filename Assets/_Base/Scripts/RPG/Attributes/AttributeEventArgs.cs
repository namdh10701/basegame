using System;

namespace _Base.Scripts.RPG.Attributes
{
    public class AttributeEventArgs: EventArgs
    {
        private object[] _targets;

        public object[] Targets => _targets;
    }
}
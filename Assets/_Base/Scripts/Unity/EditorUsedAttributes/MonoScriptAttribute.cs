using System;
using UnityEngine;

namespace _Base.Scripts.Unity.EditorUsedAttributes
{
    public class MonoScriptAttribute : PropertyAttribute
    {
        public MonoScriptAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }
    }
}
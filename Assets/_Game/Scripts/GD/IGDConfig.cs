using System;
using System.Reflection;
using UnityEngine;

namespace _Game.Scripts.GD
{
    public class GDConfigAttribute : Attribute
    {
        public GDConfigAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
    public interface IGDConfig//<TStats> where TStats: Stats
    {
        void ApplyGDConfig(object stats);

        string GetId();
    }

    public interface IOperationConfig {
        string OperationType { get; set; }
    }
    
    public abstract class GDConfig: ScriptableObject, IGDConfig
    {
        public abstract void ApplyGDConfig(object stats);

        public abstract string GetId();

        // public void Load(GDConfigLoader loader, string configId)
        // {
        //     var gdConfig = loader.CannonMap[configId];
        //     foreach (var fieldInfo in typeof(CannonConfig).GetFields(BindingFlags.Public | BindingFlags.Instance))
        //     {
        //         object value = gdConfig[fieldInfo.Name];
        //         if (fieldInfo.FieldType == typeof(float))
        //         {
        //             value ??= 0f;
        //         }
        //         fieldInfo.SetValue(this, value);
        //     }
        // }

        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GD.DataManager
{
    public class StatAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsPercentage { get; set; }

        public StatAttribute(string name, bool isPercentage = false)
        {
            Name = name;
            IsPercentage = isPercentage;
        }
    }
}

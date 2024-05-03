using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public enum Rarity
    {
        Common, Uncommon, Rare, Epic, Legend, LegendPlus
    }
    public interface ICraftItem
    {
        public Rarity Rarity { get; set; }
    }
}

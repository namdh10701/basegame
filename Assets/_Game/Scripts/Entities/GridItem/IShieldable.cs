using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public interface IShieldable
    {
        public List<RangedStat> Shields { get; set; }
        public List<RangedStat> Blocks { get; set; }
    }
}
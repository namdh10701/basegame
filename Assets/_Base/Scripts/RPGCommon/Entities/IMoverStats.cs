using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG.Stats
{
    public interface IMoverStats
    {
        public Stat MoveSpeed { get; set; }
    }
}
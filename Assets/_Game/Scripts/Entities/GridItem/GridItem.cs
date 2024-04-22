using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class GridItem : Entity
    {

        GridItemStats gridItemStats;
        public override Stats Stats => gridItemStats;
    }
}

using System;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;

namespace _Game.Scripts.CrewSystem
{
    [Serializable]
    public class CrewData
    {
        public int Id;
        public int Level;
        public List<GearData> EquippingGear;
        // Stats & Skill
    }
}

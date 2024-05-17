namespace _Game.Scripts.InventorySystem
{
    [System.Serializable]
    public class GearData : IInventoryData, IUpgradeable
    {
        public int id;
        public InventoryType inventoryType;
        public GearType GearType;
        public Rarity rarity;

        public GearData(int id, GearType gearType, Rarity rarity)
        {
            this.id = id;
            this.inventoryType = InventoryType.Gear;
            this.GearType = gearType;
            this.rarity = rarity;
        }

        public int Id { get => id; set => id = value; }
        public InventoryType Type { get => inventoryType; }
        public Rarity Rarity { get => rarity; set => rarity = value; }
    }
}
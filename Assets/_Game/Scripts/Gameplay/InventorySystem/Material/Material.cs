namespace _Game.Scripts.InventorySystem
{
    public class Material : IInventoryItem, IUpgradeable, IStackable
    {
        public Rarity Rarity { get; set; }
        public int Count { get; set; }

        public int Id => throw new System.NotImplementedException();

        public GearType GearType => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();
    }
}

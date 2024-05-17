namespace _Game.Scripts.InventorySystem
{
    public interface IInventoryData
    {
        public int Id { get; set; }
        public InventoryType Type { get; }
    }
}

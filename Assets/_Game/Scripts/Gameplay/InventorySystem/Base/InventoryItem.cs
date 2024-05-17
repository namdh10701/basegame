
namespace _Game.Scripts.InventorySystem
{
    public enum InventoryType
    {
        Potion, Gear, Crew
    }
    public interface IInventoryItem
    {
        public int Id { get;}
        public GearType GearType { get;}
        public string Name { get;}
        public string Description { get;}
    }
}

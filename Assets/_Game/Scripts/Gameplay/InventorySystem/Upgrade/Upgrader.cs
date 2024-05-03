using _Game.Scripts.SaveLoad;

namespace _Game.Scripts.InventorySystem
{
    public class Upgrader
    {
        public bool IsValidUpgrade(IUpgradeable mainItem, IUpgradeable[] consumeItems)
        {
            foreach (IUpgradeable upgradeable in consumeItems)
            {
                if (upgradeable.Rarity != mainItem.Rarity)
                {
                    return false;
                }
            }
            return true;
        }
        public void Upgrade(IUpgradeable mainItem, IUpgradeable[] consumeItems)
        {
            InventoryCollection<InventoryItem> ownedItems = SaveSystem.GameSave.OwnedInventoryItems;
            mainItem.Rarity++;
            foreach (IUpgradeable upgradeable in consumeItems)
            {
                ownedItems.Items.Remove(upgradeable as InventoryItem);
            }
            SaveSystem.SaveGame();
        }
    }
}

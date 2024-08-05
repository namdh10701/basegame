using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;
namespace Online
{
	public static class ItemInstanceExtension
	{
		public static ItemData GetItemData(this ItemInstance itemInstance)
		{
			string[] idParts = itemInstance.ItemId.Split('_');
			var itemType = idParts[0].GetItemType();
			if (itemType == ItemType.None) return null;

			var itemId = idParts[1];

			int level = 1;
			if (itemInstance.CustomData != null && itemInstance.CustomData.TryGetValue(C.NameConfigs.Level, out var levelData))
			{
				level = System.Convert.ToInt32(levelData);
			}

			int rarityLevel = 0;
			switch (itemType)
			{
				case ItemType.CANNON:
					rarityLevel = GameData.CannonTable.FindById(itemId)?.RarityLevel ?? 0;
					break;

				case ItemType.AMMO:
					rarityLevel = GameData.AmmoTable.FindById(itemId)?.RarityLevel ?? 0;
					break;
				// case ItemType.SHIP:
				// 	rarityLevel = GameData.ShipTable.FindById(itemId)?.RarityLevel ?? 0;
				// 	break;
			}

			return new ItemData()
			{
				ItemType = itemType,
				ItemId = itemId,
				OwnItemId = itemInstance.ItemInstanceId,
				Level = level,
				RarityLevel = rarityLevel
			};
		}
		
		public static ItemData[] ParseToItemDatas(this ItemInstance[] itemInstances)
		{
			var itemDatas = new ItemData[itemInstances.Length];
			for (int i = 0; i < itemInstances.Length; i++)
			{
				itemDatas[i] = itemInstances[i].GetItemData();
			}
			return itemDatas;
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.SaveSystem;
using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using Online;

namespace _Game.Scripts.SaveLoad
{
	public static class SaveSystem
	{
		public static SaveData GameSave;

		public static void LoadSave()
		{
			// FIXME: Delete
			// SaveLoadManager.DeleteSave(1);

			// GameSave = SaveLoadManager.ReadSave(1);
			// if (GameSave == null)
			// {
			//     SaveLoadManager.WriteDefaultSave(SaveData.DefaultSave);
			//     GameSave = SaveLoadManager.ReadSave(1);
			// }

			// Load backend data
			// GameSave.MapStatus = PlayfabManager.Instance.MapStatus;
			GameSave = new SaveData();
			foreach (var item in PlayfabManager.Instance.ItemMaps)
			{
				item.Value.Level = item.Value.Level + 1;
				GameSave.OwnedItems.Add(item.Value);
			}
			
			// test
			// foreach (var rec in GameData.ShipTable.Records)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = rec.Id,
			// 		ItemType = ItemType.SHIP,
			// 		Level = 1,
			// 	});
			// }
			//
			// foreach (var rec in GameData.AmmoTable.Records)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = rec.Id,
			// 		ItemType = ItemType.AMMO,
			// 		Level = 1,
			// 	});
			// }
			//
			// foreach (var rec in GameData.CrewTable.Records)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = rec.Id,
			// 		ItemType = ItemType.CREW,
			// 		Level = 1,
			// 	});
			// }
			//
			// foreach (var rec in GameData.CannonTable.Records)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = rec.Id,
			// 		ItemType = ItemType.CANNON,
			// 		Level = 1,
			// 	});
			// }
			//
			// for (int i = 0; i < 5; i++)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = MiscItemId.blueprint_cannon,
			// 		ItemType = ItemType.MISC,
			// 	});
			// }
			//
			// for (int i = 0; i < 5; i++)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = MiscItemId.blueprint_ammo,
			// 		ItemType = ItemType.MISC,
			// 	});
			// }
			//
			// for (int i = 0; i < 5; i++)
			// {
			// 	GameSave.OwnedItems.Add(new ItemData()
			// 	{
			// 		ItemId = MiscItemId.blueprint_ship,
			// 		ItemType = ItemType.MISC,
			// 	});
			// }
			//
			// GameSave.OwnedItems.Add(new ItemData()
			// {
			// 	ItemId = "2011",
			// 	ItemType = ItemType.CREW,
			// });
			// end test
			
			GameSave.ShipSetupSaveData = PlayfabManager.Instance.Equipment.EquipmentShips;
			if (GameSave.ShipSetupSaveData == null)
			{
				GameSave.ShipSetupSaveData = new ShipSetupSaveData();
				GameSave.ShipSetupSaveData.Init();
			}
		}
		public static void SaveGame()
		{
			DebounceUtility.Debounce(() => SaveLoadManager.WriteSave(GameSave), 500);
		}
	}
}
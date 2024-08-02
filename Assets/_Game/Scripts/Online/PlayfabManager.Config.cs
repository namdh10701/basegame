using System.Collections.Generic;
using System.IO;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model.Config;
using PlayFab;
using UnityEngine;
using Newtonsoft.Json;
using PlayFab.Internal;

namespace Online
{
	public partial class PlayfabManager : IConfigService
	{
		private const string GAME_CONFIG_NAME = "game_config";

		private const string SHIPS_DB_NAME = "ShipsDB.json";
		private const string MONSTERS_DB_NAME = "MonstersDB.json";
		private const string CREWS_DB_NAME = "CrewsDB.json";
		private const string CANNONS_DB_NAME = "CannonsDB.json";
		private const string CANNONS_FEVERS_DB_NAME = "CannonFeversDB.json";
		private const string AMMOS_DB_NAME = "AmmosDB.json";
		
		private const string NORMAL_LEVELS_DB_NAME = "NormalLevelsDB.json";
		private const string PRE_LEVEL_DB_NAME = "PreLevelsDB.json";
		private const string ITEM_LEVEL_DB_NAME = "StatItemsDB.json";

		private const string SHIP_UPGRADE_DB_NAME = "ShipUpgradeDB.json";
		private const string AMMO_UPGRADE_DB_NAME = "AmmoUpgradeDB.json";
		private const string CANNON_UPGRADE_DB_NAME = "CannonUpgradeDB.json";

		#region Properties

		public ShopItemTable ShopItemTable { get; private set; }
		public ShopListingTable ShopListingTable { get; private set; }
		public ShopRarityTable ShopRarityTable { get; private set; }

		public ShipTable ShipTable { get; private set; }
		public MonsterTable MonsterTable { get; private set; }
		public CrewTable CrewTable { get; private set; }
		public CannonTable CannonTable { get; private set; }
		public CannonTable CannonFeverTable { get; private set; }
		public AmmoTable AmmoTable { get; private set; }

		public LevelWaveTable LevelWaveTable { get; private set; }
		public TalentTreeTable TalentTreeNormalTable { get; private set; }
		public TalentTreeTable TalentTreePremiumTable { get; private set; }
		public TalentTreeItemTable TalentTreeItemTable { get; private set; }

		public InventoryItemUpgradeTable CannonUpgradeTable { get; private set; }
		public InventoryItemUpgradeTable AmmoUpgradeTable { get; private set; }
		public InventoryItemUpgradeTable ShipUpgradeTable { get; private set; }

		#endregion

		private GameConfigModel _configModel = null;

		public async UniTask LoadDatabase()
		{
			await RequestNewDatabase();

			ShipTable = new ShipTable(SHIPS_DB_NAME);
			MonsterTable = new MonsterTable(MONSTERS_DB_NAME);
			CrewTable = new CrewTable(CREWS_DB_NAME);
			CannonTable = new CannonTable(CANNONS_DB_NAME);
			CannonFeverTable = new CannonTable(CANNONS_FEVERS_DB_NAME);
			AmmoTable = new AmmoTable(AMMOS_DB_NAME);

			TalentTreeNormalTable = new TalentTreeTable(NORMAL_LEVELS_DB_NAME);
			TalentTreePremiumTable = new TalentTreeTable(PRE_LEVEL_DB_NAME);
			TalentTreeItemTable = new TalentTreeItemTable(NORMAL_LEVELS_DB_NAME);

			CannonUpgradeTable = new InventoryItemUpgradeTable(CANNON_UPGRADE_DB_NAME);
			AmmoUpgradeTable = new InventoryItemUpgradeTable(AMMO_UPGRADE_DB_NAME);
			ShipUpgradeTable = new InventoryItemUpgradeTable(SHIP_UPGRADE_DB_NAME);
		}

		public async UniTask<bool> RequestNewDatabase()
		{
			var signalGameConfig = new UniTaskCompletionSource<bool>();
			PlayFabClientAPI.GetTitleData(new()
			{
				Keys = new()
				{
					GAME_CONFIG_NAME
				}
			}, result =>
			{
				_configModel = JsonConvert.DeserializeObject<GameConfigModel>(result.Data[GAME_CONFIG_NAME]);
				signalGameConfig.TrySetResult(true);
			}, error =>
			{
				Debug.LogError(error.ErrorMessage);
				signalGameConfig.TrySetResult(false);
			});

			await signalGameConfig.Task;

			var signalDBs = new UniTaskCompletionSource<bool>();
			var reloadKeys = GetDatabases();
			if (reloadKeys.Count > 0)
			{
				PlayFabClientAPI.GetTitleData(new()
				{
					Keys = reloadKeys
				}, result =>
				{
					foreach (var db in result.Data)
					{
						SaveJsonFile(db.Key, db.Value);
					}
					
					SaveJsonFile(GAME_CONFIG_NAME, JsonConvert.SerializeObject(_configModel));
					signalDBs.TrySetResult(true);
				}, error =>
				{
					signalDBs.TrySetResult(false);
				});
			}
			else
			{
				signalDBs.TrySetResult(true);
			}
			return await signalDBs.Task;
		}

		List<string> GetDatabases()
		{
			var cacheConfig = JsonConvert.DeserializeObject<GameConfigModel>(LoadJsonFile(GAME_CONFIG_NAME));

			List<string> reloadKeys = new List<string>();
			foreach (var db in cacheConfig.GameDBs)
			{
				var onlineDb = _configModel.GameDBs.Find(val => val.Name == db.Name);
				if (onlineDb != null && onlineDb.Version != db.Version)
				{
					reloadKeys.Add(db.Name);
				}
			}

			foreach (var onlineDB in _configModel.GameDBs)
			{
				if (cacheConfig.GameDBs.Find(val => val.Name == onlineDB.Name) == null)
				{
					reloadKeys.Add(onlineDB.Name);
				}
			}
			return reloadKeys;
		}

		string LoadJsonFile(string fileName)
		{
			if (File.Exists(Path.Combine(Application.persistentDataPath, fileName + ".json")))
				return File.ReadAllText(Path.Combine(Application.persistentDataPath, fileName + ".json"));
			return "{}";
		}

		void SaveJsonFile(string fileName, string content)
		{
			File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName + ".json"), content);
		}
	}
}
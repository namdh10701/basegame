using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Interface;
using Online.Service;
using UnityEngine;

namespace Online
{
	public partial class PlayfabManager : UnityEngine.MonoBehaviour, IPlayfabManager
	{
		public static PlayfabManager Instance;

		private AuthService _authService = null;
		private ProfileService _profileService = null;
		private InventoryService _inventoryService = null;
		private EquipmentService _equipmentService = null;
		private ShopService _shopService = null;
		private RankingService _rankingService = null;

		#region Services

		public AuthService Auth => _authService;
		public ProfileService Profile => _profileService;
		public InventoryService Inventory => _inventoryService;
		public EquipmentService Equipment => _equipmentService;
		public RankingService Ranking => _rankingService;

		#endregion

		private void Awake()
		{
			Instance = this;
			Initialize();
		}

		private void Initialize()
		{
			_authService = new AuthService();
			_profileService = new ProfileService();
			_inventoryService = new InventoryService();
			_equipmentService = new EquipmentService();
			_shopService = new ShopService();
			_rankingService = new RankingService();

			_authService.Initialize(this);
			_profileService.Initialize(this);
			_inventoryService.Initialize(this);
			_equipmentService.Initialize(this);
			_shopService.Initialize(this);
			_rankingService.Initialize(this);
		}

		public async Task LoginAsync()
		{
			var loginResponse = await Auth.LoginAsync();
			if (loginResponse.Status == ELoginStatus.Failed)
			{
				Debug.LogError("Login failed");
				return;
			}
			
			await LoadDatabase();
			
			if (loginResponse.Status == ELoginStatus.Newly)
			{
				await Profile.RequestDisplayNameAsync();
				await Profile.RequestUserProfileAsync();
				await Inventory.RequestInventoryAsync();
			}
			else
			{
				var infoPayload = loginResponse.ResultPayload;
				Profile.LoadProfile(infoPayload.PlayerProfile);
				Profile.LoadUserReadOnlyData(infoPayload.UserReadOnlyData);
				Equipment.LoadEquipmentShip(infoPayload.UserData);
				Inventory.LoadVirtualCurrency(infoPayload.UserVirtualCurrency);
				Inventory.LoadItems(infoPayload.UserInventory);
			}

			LoadUserRankInfo();
			
			await Ranking.LoadRewardBundleInfo();
			
			// UpdateEquipShip(SaveSystem.GameSave.ShipSetupSaveData);

			LoadShop();
		}

		public void LinkFacebook()
		{
			Auth.LinkFacebook();
		}

		public async UniTask UpgradeItem(string itemInstanceId)
		{
			var resUpgrade = await Inventory.UpgradeItem(itemInstanceId);
			Inventory.LoadVirtualCurrency(resUpgrade.VirtualCurrency);
			Inventory.RevokeBlueprints(resUpgrade.RevokeBlueprintIDs);
		}

		public async UniTask CombineItems(List<string> itemInstanceIds)
		{
			var resUpgrade = await Inventory.CombineItems(itemInstanceIds);
			Inventory.RefundBlueprints(resUpgrade.RefundBlueprints);
		}

		public void RunCoroutine(IEnumerator coroutine)
		{
			StartCoroutine(coroutine);
		}
	}
}
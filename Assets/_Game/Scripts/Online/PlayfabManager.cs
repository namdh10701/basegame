using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Ump.Api;
using Newtonsoft.Json;
using Online.Enum;
using Online.Interface;
using Online.Model.RequestAPI.Profile;
using Online.Model.ResponseAPI.Inventory;
using Online.Model.ResponseAPI.Profile;
using Online.Service;
using PlayFab;
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
		private AdsService _adsService = null;

		#region Services

		public AuthService Auth => _authService;
		public ProfileService Profile => _profileService;
		public InventoryService Inventory => _inventoryService;
		public EquipmentService Equipment => _equipmentService;
		public RankingService Ranking => _rankingService;
		public AdsService Ads => _adsService;

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
			_adsService = new AdsService();

			_authService.Initialize(this);
			_profileService.Initialize(this);
			_inventoryService.Initialize(this);
			_equipmentService.Initialize(this);
			_shopService.Initialize(this);
			_rankingService.Initialize(this);
			_adsService.Initialize(this);
		}

		public async UniTask<bool> LoginAsync()
		{
			var loginResponse = await Auth.LoginAsync();
			
			if (loginResponse.Result)
			{
				await LoadDatabase();
				await GameData.Load();

				var profileResponse = await Profile.RequestProfileAsync(loginResponse.PlayfabID);

				if (string.IsNullOrEmpty(profileResponse.PlayerProfileModel.DisplayName))
				{
					var newName = await Profile.RequestDisplayNameAsync("User" + UnityEngine.Random.Range(1, 99999).ToString("D5"));
					profileResponse.PlayerProfileModel.DisplayName = newName;
				}

				Profile.LoadProfile(profileResponse.PlayerProfileModel);
				Profile.LoadUserReadOnlyData(profileResponse.UserReadOnlyData);
				Equipment.LoadEquipmentShip(profileResponse.UserData);
				Inventory.LoadVirtualCurrency(profileResponse.UserVirtualCurrency);
				Inventory.LoadItems(profileResponse.UserInventory);
				Auth.LoadAccountInfo(profileResponse.UserAccountInfo);

				await LoadUserRankInfoAsync();

				await Ranking.LoadRewardBundleInfo();
				await LoadShopAsync();
				await RequestInventoryAsync();
				await RequestEquipmentShipAsync();

				await Ads.LoadAdsAsync();
				// UpdateEquipShip(SaveSystem.GameSave.ShipSetupSaveData);
				return true;
			}
			return false;
		}

		public async Task LinkFacebook()
		{
			await Auth.LinkFacebook();
		}

		public async Task UnlinkFacebook()
		{
			await Auth.UnlinkFacebook();
		}

		public async UniTask<EnhanceItemResponse> EnhanceItem(string itemInstanceId)
		{
			var resUpgrade = await Inventory.EnhanceItem(itemInstanceId);
			Inventory.UpdateItemData(resUpgrade.ItemUpgrade);
			Inventory.LoadVirtualCurrency(resUpgrade.VirtualCurrency);
			Inventory.RemoveItems(resUpgrade.RevokeBlueprintIDs);
			return resUpgrade;
		}

		public async UniTask<CombineItemRespons> CombineItems(List<string> itemInstanceIds)
		{
			var resUpgrade = await Inventory.CombineItems(itemInstanceIds);
			Inventory.RemoveItems(itemInstanceIds);
			Inventory.AddItems(new () { resUpgrade.Item });
			Inventory.RefundBlueprints(resUpgrade.RefundBlueprints);
			return resUpgrade;
		}

		public void RunCoroutine(IEnumerator coroutine)
		{
			StartCoroutine(coroutine);
		}

		public async UniTask<DateTime> GetTimeAsync()
		{
			var signal = new UniTaskCompletionSource<DateTime>();
			PlayFabClientAPI.GetTime(new(), result =>
			{
				signal.TrySetResult(result.Time);
			}, error =>
			{
				signal.TrySetResult(DateTime.MinValue);
			});
			return await signal.Task;
		}

		public async UniTask ReportLimitPackage(string adUnitId)
		{
			var signal = new UniTaskCompletionSource<bool>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.ReportLimitPackage,
				FunctionParameter = new ReportLimitPackageRequest()
				{
					AdUnitId = adUnitId
				}
			}, result =>
			{
				var readOnlyData = JsonConvert.DeserializeObject<ReportLimitPackageResponse>(result.FunctionResult.ToString());
				Profile.SetLimitPackage(readOnlyData.LimitPackages);
				signal.TrySetResult(true);
			}, error =>
			{
				signal.TrySetResult(false);
			});
			await signal.Task;
		}
	}
}
using _Game.Scripts.SaveLoad;
using Online.Enum;
using Online.Interface;
using Online.Service.Auth;
using Online.Service.Leaderboard;
using Online.Service.Profile;

namespace Online
{
	public partial class PlayfabManager : UnityEngine.MonoBehaviour, IPlayfabManager
	{
		public static PlayfabManager Instance;

		private AuthService _authService = null;
		private ProfileService _profileService = null;
		private InventoryService _inventoryService = null;
		private EquipmentService _equipmentService = null;

		#region Services

		public AuthService Auth => _authService;
		public ProfileService Profile => _profileService;
		public InventoryService Inventory => _inventoryService;
		public EquipmentService Equipment => _equipmentService;

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

			_authService.Initialize(this);
			_profileService.Initialize(this);
			_inventoryService.Initialize(this);
			_equipmentService.Initialize(this);
		}

		public void Login()
		{
			Auth.Login((result, infoPayload) =>
			{
				switch (result)
				{
					case ELoginStatus.Failed:
						break;

					default:
						if (result == ELoginStatus.Newly)
						{
							Profile.RequestNewProfile(result =>
							{
								if (result)
								{
									RequestInventory();
								}
							});
						}
						else
						{
							Profile.LoadProfile(infoPayload.PlayerProfile, infoPayload.UserReadOnlyData); 
							Equipment.LoadEquipmentShip(infoPayload.UserData);
							Inventory.LoadVirtualCurrency(infoPayload.UserVirtualCurrency);
							Inventory.LoadItems(infoPayload.UserInventory);
							PlayfabManager.Instance.UpdateEquipShip(SaveSystem.GameSave.ShipSetupSaveData);
						}
						break;
				}
			});
		}

		public void LinkFacebook()
		{
			Auth.LinkFacebook();
		}

		public void UpgradeItem(string itemInstanceId, System.Action<bool> cb = null)
		{
			Inventory.UpgradeItem(itemInstanceId, (result) =>
			{
				cb?.Invoke(true);
			});
		}
	}
}
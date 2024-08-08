using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Model.ResponseAPI.Inventory;
using Online.Service;

namespace Online.Interface
{
	public interface IPlayfabManager
	{
		public AuthService Auth { get; }
		public ProfileService Profile { get; }
		public InventoryService Inventory { get; }
		public EquipmentService Equipment { get; }
		public RankingService Ranking { get; }
		
		UniTask<EnhanceItemResponse> EnhanceItem(string itemInstanceId);
		UniTask<CombineItemRespons> CombineItems(List<string> itemInstanceIds);
		UniTask ReportLimitPackage(string storeId);

		UniTask<DateTime> GetTimeAsync();
		
		void RunCoroutine(IEnumerator coroutine);
	}
}
using System.Collections.Generic;
using _Game.Features.Ads;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Model.ApiRequest;
using Online.Model.GooglePurchase;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service
{
	public class AdsService : BaseOnlineService
	{
		const string AppID = "ca-app-pub-4412764125039323~1816855687";
		public Dictionary<string, AdPlacementDetails> VideoAds { get; private set; } = new();

		public async UniTask<bool> LoadAdsAsync()
		{
			var signal = new UniTaskCompletionSource<bool>();
			PlayFabClientAPI.GetAdPlacements(new()
			{
				AppId = AppID
			}, result =>
			{
				VideoAds.Clear();
				foreach (var adPlacement in result.AdPlacements)
				{
					VideoAds.Add(adPlacement.RewardAssetUrl, adPlacement);
				}
				signal.TrySetResult(true);
			}, error =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(false);
			});
			return await signal.Task;
		}

		public async UniTask<bool> CanWatchAd(string adUnitId)
		{
			if (VideoAds.TryGetValue(adUnitId, out var videoAd))
			{
				var adCustomData = JsonConvert.DeserializeObject<StoreCustomData>(videoAd.RewardDescription);
				switch (adCustomData.Limit)
				{
					case EItemLimit.Daily:
					case EItemLimit.Weekly:
						var curTime = await Manager.GetTimeAsync();
						var limitPackage = Manager.Profile.LimitPackages.Find(x => x.Id == adUnitId);
						if (limitPackage != null)
						{
							var latestDate = limitPackage.LastTime.GetDateTime();
							return curTime.IsNewDate(latestDate) || (limitPackage.Count < adCustomData.Count && (curTime - latestDate).TotalSeconds > adCustomData.Countdown);
						}
						return true;
				}
			}
			return false;
		}

		public async UniTask<bool> ShowVideoAd(string adUnitId)
		{
			if (VideoAds.TryGetValue(adUnitId, out var videoAd))
			{
				var signal = new UniTaskCompletionSource<bool>();
				AdsManager.Instance.LoadRewardedAd(adUnitId, async () =>
				{
					await ClaimAdReward(videoAd);

					var adCustomData = JsonConvert.DeserializeObject<StoreCustomData>(videoAd.RewardDescription);
					if (adCustomData.Limit != EItemLimit.None)
					{
						await Manager.ReportLimitPackage(adUnitId);
						signal.TrySetResult(true);
					}
				});
				return await signal.Task;
			}
			return false;
		}

		public async UniTask<bool> ClaimAdReward(AdPlacementDetails adPlacementDetail)
		{
			var signal = new UniTaskCompletionSource<bool>();
			PlayFabClientAPI.RewardAdActivity(new()
			{
				PlacementId = adPlacementDetail.PlacementId,
				RewardId = adPlacementDetail.RewardId
			}, result =>
			{
				signal.TrySetResult(true);
			}, error =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(false);
			});
			return await signal.Task;
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Ads");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Ads");
		}
	}
}
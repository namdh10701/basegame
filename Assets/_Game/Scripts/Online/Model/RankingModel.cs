using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using Newtonsoft.Json;
using Online.Converters;
using Online.Enum;
using PlayFab.ClientModels;

namespace Online.Model
{
	public class RankReward
	{
		public string ItemId;
		public ItemType ItemType;
		public int Amount;
	}

	public class SeasonInfo
	{
		[JsonProperty("No")]
		public int SeasonNo;

		[JsonProperty("Name")]
		public string SeasonName;

		[JsonProperty("Start")]
		public ulong StartTimestamp;

		[JsonProperty("End")]
		public ulong EndTimestamp;
	}

	public class PlayerRankInfo
	{
		[JsonProperty("Id")]
		public string Id;

		[JsonProperty("Name")]
		public string DisplayName;

		[JsonProperty("Score")]
		public int Score;
	}

	public class RankInfo
	{
		[JsonProperty("Id")]
		public string RankID { get; set; }

		[JsonProperty("Count")]
		public int Count { get; set; }

		[JsonProperty("Players")]
		public List<PlayerRankInfo> Players { get; set; }
	}

	public class RewardData
	{
		[JsonProperty("Exp")]
		public string Exp;

		[JsonProperty("Gold")]
		public int Gold;

		[JsonProperty("Key")]
		public int Key;

		[JsonProperty("Blueprint")]
		public string[] Blueprint;
	}

	public class CompleteSeasonInfo
	{
		[JsonProperty("NewRank")]
		[JsonConverter(typeof(ERankConverter))]
		public ERank NewRank { get; set; }

		[JsonProperty("TimeExpired")]
		public ulong TimeExpired { get; set; }

		[JsonProperty("SeasonReward")]
		public SeasonReward SeasonReward { get; set; }
	}

	public class SeasonReward
	{
		[JsonProperty("Items")]
		public List<ItemInstance> Items { get; set; }
		
		[JsonProperty("VirtualCurrency")]
		public Dictionary<string, int> VirtualCurrency { get; set; }
	}

	public class ClaimRewardBundle
	{
		public ERank Rank;
		public bool IsCurrentRank;
		public List<RankReward> Rewards = new();
		public bool IsClaimed;
	}

	public class RewardBundleInfo
	{
		public List<ClaimRewardBundle> Bundles = new();
	}
}
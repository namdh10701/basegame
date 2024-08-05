using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using Newtonsoft.Json;
using Online.Enum;

namespace Online.Model
{
	public class RankReward
	{
		public string ItemId;
		public ItemType ItemType;
		public int Amount;
	}
		
	public class RankRecord
	{
		public int No;
		public string PlayfabID; // dùng để track xem record hiện tại có phải của người chơi hiện tại không
		public string Username;
		public int Score;
		public List<RankReward> Rewards = new();
	}

	public class RankInfo
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

	public class UserRankInfo
	{
		[JsonProperty("Id")]
		public string RankID;
		
		[JsonProperty("Count")]
		public int Count;

		[JsonProperty("Players")]
		public PlayerRankInfo[] Players;
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
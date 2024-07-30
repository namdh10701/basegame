using System.Collections.Generic;

namespace Online.Model
{
	public enum UserRank
	{
		UnRank,
		Rookie,
		Gunner,
		Hunter,
		Captain,
		Conqueror,
	}
	public class RankReward
	{
		public string ItemId;
		public int Amount;
	}
		
	public class RankRecord
	{
		public int No;
		public string Username;
		public int Score;
		public List<RankReward> Rewards = new();
	}

	public class UserRankInfo
	{
		public string SeasonNo;
		public string SeasonName;
		public UserRank Rank;
		public List<RankRecord> Records = new();
	}
	
	public class ClaimRewardBundle
	{
		public List<RankReward> Records = new();
		public bool IsClaimed;
	}

	public class RewardBundleInfo
	{
		public List<ClaimRewardBundle> Bundles = new();
	}
}
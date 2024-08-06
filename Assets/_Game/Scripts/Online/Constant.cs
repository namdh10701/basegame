using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityString;
using Online.Enum;
namespace Online
{
	public static class C
	{
		public static class NameConfigs
		{
			public const string Level = "Level";
			public const string Exp = "Exp";
			public const string Energy = "Energy";
			public const string Equipment = "Equipment";

			// Ranking
			public const string Rank = "Rank";
			public const string RankScore = "RankScore";
			public const string CurrentRankID = "CurrentRankID";
			public const string VideoAds = "VideoAds";
		}

		public static class RankConfigs
		{
			public static string GetLeaderboard(ERank rank) => $"{rank}_Rank_Score";
		}

		public static class CloudFunction
		{
			public const string UpgradeItem = "UpgradeItem";
			public const string CombineItems = "CombineItems";
			public const string GetRankInfo = "GetRankInfo";
			public const string CreateRankTicket = "CreateRankTicket";
			public const string SubmitRankingMatchAsync = "SubmitRankingMatchAsync";
			public const string BonusGold = "BonusGold";
			public const string ReportLimitPackage = "ReportLimitPackage";
			public const string RequestGacha = "RequestGacha";
			
			// Ranking Season
			public const string RequestSeasonInfo = "RequestSeasonInfo";
		}
	}
}
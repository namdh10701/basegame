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
			public const string Equipment = "Equipment";
			public const string CompleteSeasonInfo = "CompleteSeasonInfo";
			public const string LimitPackages = "LimitPackages";
			public const string GachasPackages = "Gachas";

			// Ranking
			public const string Rank = "Rank";
			public const string CurrentRankID = "CurrentRankID";
		}

		public static class RankConfigs
		{
			public static string GetLeaderboard(ERank rank) => $"{rank}_Rank_Score";
		}

		public static class CloudFunction
		{
			public const string EnhanceItem = "EnhanceItem";
			public const string CombineItems = "CombineItems";
			public const string BonusGold = "BonusGold";
			public const string ReportLimitPackage = "ReportLimitPackage";
			public const string RequestGacha = "RequestGacha";
			
			// Ranking
			public const string RequestSeasonInfo = "RequestSeasonInfo";
			public const string CreateRankTicket = "CreateRankTicket";
			public const string FinishRankBattle = "FinishRankBattle";
			public const string ClaimSeasonReward = "ClaimSeasonReward";
		}
	}
}
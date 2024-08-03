using System.Collections.Generic;
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
			public const string EquipmentShips = "EquipmentShips";
			
			// Ranking
			public const string Rank = "Rank";
			public const string RankScore = "RankScore";
			public const string CurrentRankID = "CurrentRankID";
		}
		
		public static class CloudFunction
		{
			public const string UpgradeItem = "UpgradeItem";
			public const string CombineItems = "CombineItems";
			public const string GetRankInfo = "GetRankInfo";
			public const string CreateRankTicket = "CreateRankTicket";
			public const string SubmitRankingMatch = "SubmitRankingMatch";
		}
	}
}
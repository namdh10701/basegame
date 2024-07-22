using System.Collections.Generic;
using Online.Enum;
namespace Online
{
	public static class C
	{
		public static class NameConfigs
		{
			public const string CurrencyCoin = "Coin";
			public static Dictionary<EVirtualCurrency, string> Currencies = new()
			{
				{ EVirtualCurrency.Coin, "Coin" },
				{ EVirtualCurrency.Gem, "Gem" }
			};
		}
		
		public static class CloudFunction
		{
			public const string CreateProfile = "CreateProfile";
		}
	}
}
using Online.Enum;

namespace Online
{
	public static class OnlineUtils
	{
		public static string GetCode(this EVirtualCurrency currency)
		{
			return currency switch
			{
				EVirtualCurrency.Coin => "CO",
				EVirtualCurrency.Gem => "GE",
				_ => string.Empty
			};
		}
	}
}
using _Game.Features.Inventory;
using Online.Enum;

namespace Online
{
	public static class OnlineUtils
	{
		public static string GetCode(this EVirtualCurrency currency)
		{
			return currency switch
			{
				EVirtualCurrency.Gold => "GO",
				EVirtualCurrency.Gem => "GE",
				EVirtualCurrency.Energy => "EN",
				EVirtualCurrency.Ticket => "TI",
				EVirtualCurrency.Diamond => "DI",
				EVirtualCurrency.RealMoney => "RM",
				EVirtualCurrency.Key => "VK",
				EVirtualCurrency.FreeTicket => "FT",
				_ => string.Empty
			};
		}

		public static ItemType GetItemType(this string item)
		{
			return item switch
			{
				"ship" => ItemType.SHIP,
				"crew" => ItemType.CREW,
				"cannon" => ItemType.CANNON,
				"ammo" => ItemType.AMMO,
				_ => ItemType.None
			};
		}
	}
}
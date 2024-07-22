using System.Collections.Generic;
using Online.Enum;
namespace Online.Interface
{
	public interface IProfile
	{
		public string DisplayName { get; }
		public Dictionary<EVirtualCurrency, int> Currencies { get; }
	}
}
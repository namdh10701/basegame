using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
	/// <summary>
	/// 
	/// </summary>
	public class GachaTable : LocalDataTable<GachaTableRecord>
	{
		public GachaTable(string fileName = null) : base(fileName)
		{
		}

		public List<GachaTableRecord> AllItems()
		{
			return Records;
		}
	}

	public class GachaTableRecord : DataTableRecord
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("virtual_currency")]
		public string VirtualCurrencyCode { get; set; }

		[JsonProperty("price")]
		public int Price { get; set; }

		[JsonProperty("amount")]
		public int Amount { get; set; }

		public override object GetId()
		{
			return Id;
		}
	}
}
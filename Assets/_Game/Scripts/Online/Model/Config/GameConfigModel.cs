using System.Collections.Generic;
namespace Online.Model.Config
{
	[System.Serializable]
	public class GameConfigModel
	{
		public List<DatabaseInfo> GameDBs = new();
	}
	
	[System.Serializable]
	public class DatabaseModel
	{
		public DatabaseInfo ShipsDB;
		public DatabaseInfo MonstersDB;
		public DatabaseInfo CrewsDB;
		public DatabaseInfo CannonsDB;
		public DatabaseInfo CannonFeversDB;
		public DatabaseInfo AmmosDB;
		public DatabaseInfo NormalLevelsDB;
		public DatabaseInfo PreLevelsDB;
		public DatabaseInfo StatItemIDsDB;
		public DatabaseInfo LevelBuffCalsDB;
	}
}
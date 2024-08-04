using System;
namespace Online
{
	public static class DateTimeUtils
	{
		public static DateTime GetDateTime(this long unixTime)
		{
			try
			{
				return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
			}
			catch (Exception ex)
			{
				return DateTime.UtcNow;
			}
		}
		
		public static bool IsNewDate(this DateTime date, DateTime dateCompare)
		{
			return date.Date != dateCompare.Date;
		}
	}
}
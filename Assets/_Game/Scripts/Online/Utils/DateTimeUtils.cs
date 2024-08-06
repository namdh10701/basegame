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
		
		public static bool IsNewWeek(this DateTime date, DateTime dateCompare)
		{
			var calendar = System.Globalization.CultureInfo.InvariantCulture.Calendar;
			var dateWeek = calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
			var dateCompareWeek = calendar.GetWeekOfYear(dateCompare, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
			return dateWeek != dateCompareWeek;
		}
	}
}
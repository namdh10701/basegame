using System;

namespace _Base.Scripts.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetRemainingTime(this DateTime futureDateTime,
            string day = "day", string days = "days",
            string hour = "hour", string hours = "hours",
            string minute = "minute", string minutes = "minutes",
            string second = "second", string seconds = "seconds",
            string timeHasPassed = "00:00:00")
        {
            DateTime currentDateTime = DateTime.UtcNow;
            TimeSpan remainingTime = futureDateTime - currentDateTime;

            if (remainingTime.TotalSeconds <= 0)
            {
                return timeHasPassed;
            }

            if (remainingTime.Days > 0)
            {
                return remainingTime.Days == 1 ? $"{remainingTime.Days} {day}" : $"{remainingTime.Days} {days}";
            }
            else
            {
                return remainingTime.ToString(@"hh\:mm\:ss");
            }

            if (remainingTime.Hours > 0)
            {
                return remainingTime.Hours == 1 ? $"{remainingTime.Hours} {hour}" : $"{remainingTime.Hours} {hours}";
            }

            if (remainingTime.Minutes > 0)
            {
                return remainingTime.Minutes == 1 ? $"{remainingTime.Minutes} {minute}" : $"{remainingTime.Minutes} {minutes}";
            }

            if (remainingTime.Seconds > 0)
            {
                return remainingTime.Seconds == 1 ? $"{remainingTime.Seconds} {second}" : $"{remainingTime.Seconds} {seconds}";
            }

            return timeHasPassed;
        }
    }
}
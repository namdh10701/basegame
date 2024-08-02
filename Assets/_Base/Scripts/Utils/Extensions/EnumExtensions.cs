using System;

namespace _Base.Scripts.Utils.Extensions
{
    /// <summary>
    /// Extension methods for enumerations to get the next and previous values.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the next value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="enumValue">The current enumeration value.</param>
        /// <param name="roundRobin">If true, enables round-robin behavior; otherwise, returns null when the next value is not found.</param>
        /// <returns>The next enumeration value, or null if the next value is not found and round-robin is disabled.</returns>
        public static T? GetNext<T>(this T enumValue, bool roundRobin = false) where T : struct, Enum
        {
            T[] values = (T[])Enum.GetValues(enumValue.GetType());
            int index = Array.IndexOf(values, enumValue) + 1;

            if (index >= values.Length)
            {
                return roundRobin ? values[0] : (T?)null;
            }
            return values[index];
        }

        /// <summary>
        /// Gets the previous value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="enumValue">The current enumeration value.</param>
        /// <param name="roundRobin">If true, enables round-robin behavior; otherwise, returns null when the previous value is not found.</param>
        /// <returns>The previous enumeration value, or null if the previous value is not found and round-robin is disabled.</returns>
        public static T? GetPrevious<T>(this T enumValue, bool roundRobin = false) where T : struct, Enum
        {
            T[] values = (T[])Enum.GetValues(enumValue.GetType());
            int index = Array.IndexOf(values, enumValue) - 1;

            if (index < 0)
            {
                return roundRobin ? values[^1] : (T?)null;
            }
            return values[index];
        }
    }
}
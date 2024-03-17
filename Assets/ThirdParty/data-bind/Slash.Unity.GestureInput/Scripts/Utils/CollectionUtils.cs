using System.Collections.Generic;
using System.Linq;

namespace Slash.Unity.GestureInput.Utils
{
    public static class CollectionUtils
    {
        public static string Implode<T>(this IEnumerable<T> collection, string separator)
        {
            return collection != null
                ? string.Join(separator, collection.Select(item => item.ToString()).ToArray())
                : string.Empty;
        }
    }
}
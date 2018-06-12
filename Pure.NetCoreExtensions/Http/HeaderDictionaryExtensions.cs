using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Pure.NetCoreExtensions
{
    public static class HeaderDictionaryExtensions
    {
        public static string FirstOrDefault(this IHeaderDictionary headers, string key)
        {
            if (headers.TryGetValue(key, out var values) && !string.IsNullOrWhiteSpace(values))
            {
                return values.First().Split(',').First();
            }

            return null;
        }
    }
}

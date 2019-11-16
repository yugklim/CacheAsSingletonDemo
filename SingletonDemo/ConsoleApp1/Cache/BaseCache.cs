using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache
{
    public class BaseCache<TValue>
    {
        private readonly ConcurrentDictionary<string, TValue> cache = new ConcurrentDictionary<string, TValue>();

        /// <summary>
        /// gets the cache key parts as inputs: f.e. "keyPart1", "keyPart2", "keyPart3"
        /// and turns them into the cache key in the form of "keyPart1_keyPart2_keyPart3"
        /// </summary>
        /// <param name="keyParts"></param>
        /// <returns></returns>
        private string CacheKey(IEnumerable<string> keyParts)
        {
            var keyPartsEnumerated = keyParts as string[] ?? keyParts.ToArray();
            if (!keyPartsEnumerated.Any())
            {
                return String.Empty;
            }

            string retVal = keyPartsEnumerated.Aggregate(string.Empty, (acc, key) => string.IsNullOrEmpty(acc) ? $"{key}" : $"{acc}_{key}");

            return retVal;
        }

        private string[] GetCacheKeys_WhichStartWith(string startWith)
        {
            return cache.Where(p => p.Key.ToLower().StartsWith(startWith.ToLower())).Select(p => p.Key).ToArray();
        }

        public TValue GetOrAdd(IEnumerable<string> keyParts, Func<IEnumerable<string>, TValue> valueFactory)
        {
            var keyPartsEnumerated = keyParts.ToArray();
            var cacheKey = CacheKey(keyPartsEnumerated);
            return cache.GetOrAdd(cacheKey, (key) => valueFactory(keyPartsEnumerated));
        }

        public TValue GetOrAdd(string cacheKey, Func<string, TValue> valueFactory)
        {
            return cache.GetOrAdd(cacheKey, valueFactory);
        }

        public void ClearCache(string keyStartsWith)
        {
            if (string.IsNullOrEmpty(keyStartsWith))
            {
                return;
            }

            switch (keyStartsWith.ToLower())
            {
                case "all":
                    cache.Clear();
                    return;
                default:
                    var keys = GetCacheKeys_WhichStartWith(keyStartsWith);
                    TValue value;
                    foreach (var key in keys) cache.TryRemove(key, out value);
                    return;
            }
        }

    }
}

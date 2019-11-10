using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache
{
    public class BaseCache<TValue>
    {
        private readonly IDictionary<string, TValue> hash = new Dictionary<string, TValue>();

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

        public void SetCacheValue(IEnumerable<string> keyParts, TValue value)
        {
            hash[CacheKey(keyParts)] = value;
        }

        public void SetCacheValue(string key, TValue value)
        {
            SetCacheValue(new[] { key }, value);
        }

        public bool TryGetCacheValue(IEnumerable<string> keyParts, out TValue value)
        {
            var cacheKey = CacheKey(keyParts);
            return hash.TryGetValue(cacheKey, out value);
        }

        public bool TryGetCacheValue(string key, out TValue value)
        {
            return TryGetCacheValue(new[] { key }, out value);
        }

        private string[] GetCacheKeys_WhichStartWith(string startWith)
        {
            return hash.Where(p => p.Key.ToLower().StartsWith(startWith.ToLower())).Select(p => p.Key).ToArray();
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
                    hash.Clear();
                    return;
                default:
                    var envKeys = GetCacheKeys_WhichStartWith(keyStartsWith);
                    foreach (var envKey in envKeys) hash.Remove(envKey);
                    break;
            }
        }
    }
}

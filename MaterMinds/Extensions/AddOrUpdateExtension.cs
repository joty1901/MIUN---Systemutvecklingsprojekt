using System.Collections.Generic;

namespace MaterMinds
{
    public static class AddOrUpdateExtension
    {
        public static void AddOrUpdate<TKey, TValue>
            (this IDictionary<TKey, TValue> map, TKey key, TValue value)
        {
            if (map.ContainsKey(key))
            {
                map[key] = value;
            }
            else
            {
                map.Add(key, value);
            }
        }
    }
}

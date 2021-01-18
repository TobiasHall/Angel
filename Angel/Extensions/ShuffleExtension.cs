using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Angel
{
    public static class ShuffleExtension
    {
        private static readonly Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(
        this Dictionary<TKey, TValue> source)
        {            
            return source.OrderBy(x => random.Next())
               .ToDictionary(item => item.Key, item => item.Value);
        }
    }
}

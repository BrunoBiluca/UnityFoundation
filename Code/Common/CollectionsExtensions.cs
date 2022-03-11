using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityFoundation.Code
{
    public static class CollectionsExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void AddIfNot<T>(
            this IList<T> list, Func<T, bool> predicate, T value
        )
        {
            if(list.Any(predicate))
                return;

            list.Add(value);
        }

        public static void AddIfNotExits<T>(this IList<T> list, T value)
        {
            if(list.Contains(value))
                return;

            list.Add(value);
        }
    }
}

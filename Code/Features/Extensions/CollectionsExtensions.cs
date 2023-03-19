using System;
using System.Collections.Generic;
using System.Linq;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public static class CollectionsExtensions
    {
        public static void Shuffle<T>(this IList<T> list, IRandomGenerator random)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
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

        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.Count == 0;
        }
    }
}

using System;
using System.Collections.Generic;

namespace AsepStudios.Utils
{
    public static class ShuffleService
    {
        private static readonly Random Random = new Random();
        public static void Shuffle<T>(this IList<T> list)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = Random.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.Collections;
using UnityEngine;
using Random = System.Random;

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
        
        public static FixedString512Bytes SerializeArray(this int[][] array)
        {
            return JsonConvert.SerializeObject(array);
        }

        public static int[][] DeserializeArray(this FixedString512Bytes array)
        {
            return JsonConvert.DeserializeObject<int[][]>(array.ToString());
        }
    }
}
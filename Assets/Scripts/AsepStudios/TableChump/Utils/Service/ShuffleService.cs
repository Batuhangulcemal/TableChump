using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Collections;
using Random = System.Random;

namespace AsepStudios.TableChump.Utils.Service
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
        
        public static FixedString512Bytes SerializeArray(this int[] array)
        {
            return JsonConvert.SerializeObject(array);
        }

        public static T DeserializeArray<T>(this FixedString512Bytes array)
        {
            return JsonConvert.DeserializeObject<T>(array.ToString());
        }
        
        public static void Sort(this int[][] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                
            {
                for (int j = i; j < array[0].Length; j++)
                {
                    if (array[i][0] > array[j][0]) // sort by ascending by first index of each row
                    {
                        for (int k = 0; k < array[0].Length; k++)
                        {
                            (array[i][k], array[j][k]) = (array[j][k], array[i][k]);
                        }
                    }
                }
            }
        }
    }
}
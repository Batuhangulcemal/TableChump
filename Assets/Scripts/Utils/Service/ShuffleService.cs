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
        
        public static void Sort(this int[][] array)
        {
            for (int i = 0;
                 i < array.GetLength(0); i++)
            {
 
                // loop for column of matrix
                for (int j = 0;
                     j < array.GetLength(1); j++)
                {
 
                    // loop for comparison and swapping
                    for (int k = 0;
                         k < array.GetLength(1) - j - 1; k++)
                    {
                        if (array[i][k] > array[i][k + 1])
                        {
 
                            // swapping of elements
                            (array[i][k], array[i][k + 1]) = (array[i][k + 1], array[i][k]);
                        }
                    }
                }
            }
        }
        
        
    }
}
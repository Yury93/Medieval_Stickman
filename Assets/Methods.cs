using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class Methods
    {
        /// <summary>
        /// метод возвращающий рандом
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="previousNumber"></param>
        /// <returns></returns>
        public static int RandomRange(int min, int max, int previousNumber = -1)
        {
            int nextNumber = previousNumber;
            Func<int> randomGenerator = () => new System.Random().Next(min, max + 1);

            while (nextNumber == previousNumber)
            {
                nextNumber = randomGenerator();
            }

            return nextNumber;
        }
    }
}
using System.Collections.Generic;

namespace Reevo.Unbroken.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns an IEnumerable{int} containing values ranged from lower boundary up to the upper boundary provided. 
        /// </summary>
        /// <param name="lowerBoundary">Lower boundary of the integer</param>
        /// <param name="upperBoundary">Upper boundary of the integer</param>
        /// <returns></returns>
        public static IEnumerable<int> UpTo(this int lowerBoundary, int upperBoundary)
        {
            var boundaries = new List<int>();

            for (var i = lowerBoundary; i <= upperBoundary; i++)
            {
                boundaries.Add(i);
            }

            return boundaries;
        }
    }
}

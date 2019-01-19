using System;
using System.Text.RegularExpressions;

namespace Reevo.Unbroken.Extensions
{
    public static class StringExtensions
    {
        public static MatchCollection Matches(this string input, string pattern)
        {
            return input.Matches(pattern, RegexOptions.None);
        }

        public static MatchCollection Matches(this string input, string pattern, RegexOptions options)
        {
            var regex = new Regex(pattern, options);
            return regex.Matches(input);
        }

        public static MatchCollection Matches(this string input, string pattern, RegexOptions options, TimeSpan timeout)
        {
            var regex = new Regex(pattern, options, timeout);
            return regex.Matches(input);
        }
    }
}

using System;
using System.Text;

namespace Reevo.Unbroken.Extensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Prepends a string at the beginning of string builder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="content"></param>
        public static StringBuilder Prepend(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content);
        }

        /// <summary>
        /// Prepends a string and a newline at the beginning of string builder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="content"></param>
        public static StringBuilder PrependLine(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content + Environment.NewLine);
        }
    }
}

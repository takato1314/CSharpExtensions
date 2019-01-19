using System.Threading;

namespace Reevo.Unbroken.Extensions
{
    /// <summary>
    ///     Extended Interlocked methods.
    ///     <see cref="http://msdn.microsoft.com/de-de/magazine/cc163726(en-us).aspx" />
    /// </summary>
    public static class InterlockedExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="startValue"></param>
        /// <param name="argument"></param>
        /// <param name="morphResult"></param>
        /// <returns></returns>
        public delegate int Morpher<TResult, TArgument>(
            int startValue,
            TArgument argument, 
            out TResult morphResult);

        /// <summary>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="target"></param>
        /// <param name="argument"></param>
        /// <param name="morpher"></param>
        /// <returns></returns>
        public static TResult Morph<TResult, TArgument>(
            ref int target,
            TArgument argument, 
            Morpher<TResult, TArgument> morpher)
        {
            TResult morphResult;
            int i, j = target;
            do
            {
                i = j;
                j = Interlocked.CompareExchange(ref target,
                    morpher(i, argument, out morphResult), i);
            } while (i != j);
            return morphResult;
        }
    }
}
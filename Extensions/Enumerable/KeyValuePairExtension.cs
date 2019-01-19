
namespace Reevo.Unbroken.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static bool IsDefaultValue<T>(this T value) where T : struct
        {
            bool isDefault = value.Equals(default(T));

            return isDefault;
        }
    }
}
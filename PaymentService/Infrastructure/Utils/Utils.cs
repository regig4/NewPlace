using System.Text.RegularExpressions;

namespace PaymentService.Infrastructure.Utils
{
    internal static class Utils
    {
        public static string CreateColumnName(string name)
        {
            return string.Join("_", name.SplittedByCamelCase()).ToLower();
        }

        private static string[] SplittedByCamelCase(this string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
    }
}

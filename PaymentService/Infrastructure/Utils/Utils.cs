using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PaymentService.Infrastructure.Utils
{
    static class Utils
    {
        public static string CreateColumnName(string name)
        {
            return string.Join("_", name.SplittedByCamelCase()).ToLower();
        }

        static string[] SplittedByCamelCase(this string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }
    }
}

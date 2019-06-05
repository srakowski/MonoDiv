// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Linq;
using System.Text;

namespace MonoDiv
{
    internal static class Extensions
    {
        public static string ToKebabCase(this string value)
        {
            var str = char.ToLower(value[0]) + value.Substring(1);
            return str
                .ToCharArray()
                .Aggregate(
                    new StringBuilder(),
                    (acc, curr) => acc.Append(char.IsUpper(curr) ? $"-{curr}" : $"{curr}")
                )
                .ToString()
                .ToLower();
        }
    }
}

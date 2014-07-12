using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisnelShared
{
    public static class StringExtensions
    {
        public static bool NullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool NullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string AsNullIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public static string AsNullIfWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        public static IEnumerable<T> AsNullIfEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any() ? null : list;
        }

        public static string CoalesceOnNullOrEmpty(this string str, params string[] otherStrings)
        {
            if (otherStrings == null || !otherStrings.Any())
            {
                return AsNullIfEmpty(str);
            }
            return (new[] { str })
                .Concat(otherStrings)
                .FirstOrDefault(s => !string.IsNullOrEmpty(s));
        }

        public static string CoalesceOnNullOrWhiteSpace(this string str, params string[] otherStrings)
        {
            if( otherStrings == null || !otherStrings.Any() )
            {
                return AsNullIfWhiteSpace(str);
            }
            return (new[] { str })
                .Concat(otherStrings)
                .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Extensions
{
    public static class CheckNullExtensions
    {

        public static void ThrowIfNull<T>(this T self, string exceptionText = "Argument can't be null!")
        {
            if (self == null)
                throw new ArgumentNullException(exceptionText);
        }

        public static void ThrowIfNullOrEmpty(this string self, string exceptionText = "Argument can't be null or empty!")
        {
            if (string.IsNullOrEmpty(self))
                throw new ArgumentException(exceptionText);
        }

        public static void ThrowIfNullOrWhiteSpace(this string self, string exceptionText = "Argument can't be null or empty!")
        {
            if (string.IsNullOrWhiteSpace(self))
                throw new ArgumentException(exceptionText);
        }
    }
}

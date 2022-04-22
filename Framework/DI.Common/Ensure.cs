using System;
using System.Collections.Generic;
using System.Linq;

namespace DI
{
    public static class Ensure
    {
        public static void ThrowIfEmpty(this string input, string argumentName = "input string")
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(argumentName);
        }

        public static void ThrowIfNull(this object value, string argumentName = "input value")
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void ThrowIfNullOrEmpty(this string[] value, string argumentName = "input value")
        {
            if (value == null || value.Length <= 0)
                throw new ArgumentNullException(argumentName);
        }

        public static void ThrowIfNotEqual(this object value, string argumentName, object expectedValue)
        {
            if (!value.Equals(expectedValue))
                throw new ArgumentException(argumentName, $"Argument must not equal {expectedValue}");
        }

        public static void ThrowIfEqual(this object value, string argumentName, object expectedValue)
        {
            if (value.Equals(expectedValue))
                throw new ArgumentException(argumentName, $"Argument must equal {expectedValue}");
        }

        public static void ThrowIfNotEqual<T>(this T value, Func<T> expected)
        {
            if (value == null || !value.Equals(expected()))
                throw new ArgumentException();
        }

        public static void ThrowIfEmpty<T>(this IEnumerable<T> collection,
            string message = "Collection cannot be empty")
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            That<ArgumentException>(() => !collection.Any(), message);
        }

        public static void ThrowIfNotEqual<T>(this T param1, T param2, string message = "Comparision check failed")
        {
            var comparer = EqualityComparer<T>.Default;
            That<ArgumentException>(() => !comparer.Equals(param1, param2), message);
        }

        public static void That(Func<bool> predicate, string message = "Check failed")
        {
            That(predicate, () => CreateException<ArgumentException>(message));
        }

        public static void That<T>(Func<bool> predicate, string message = "Check failed") where T : Exception, new()
        {
            That(predicate, () => CreateException<T>(message));
        }

        public static void That<T>(Func<bool> predicate, Func<T> exception) where T : Exception, new()
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            if (!predicate.Invoke())
                throw exception();
        }

        private static T CreateException<T>(string message = null) where T : Exception, new()
        {
            if (string.IsNullOrWhiteSpace(message))
                return new T();
            try
            {
                return (T) Activator.CreateInstance(typeof(T), message);
            }
            catch (MissingMethodException)
            {
                return new T();
            }
        }
    }
}
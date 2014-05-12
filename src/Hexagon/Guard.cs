// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Guard clauses for arguments validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Guard clauses for arguments validation.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Ensures the given <paramref name="value"/> is not null.
        /// Throws <see cref="ArgumentNullException"/> otherwise.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the argument.
        /// </typeparam>
        /// <param name="reference">
        /// The reference function for argument name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void AgainstNull<T>(Expression<Func<T>> reference, T value)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(GetParameterName(reference), "Parameter cannot be null.");
            }
        }

        /// <summary>
        /// Ensures the given <paramref name="value"/> is not null or empty.
        /// Throws <see cref="ArgumentException"/> otherwise.
        /// </summary>
        /// <param name="reference">
        /// The reference function for argument name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void AgainstNullOrEmpty(Expression<Func<string>> reference, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Parameter cannot be null or empty.", GetParameterName(reference));
            }
        }

        /// <summary>
        /// Ensures the given <paramref name="value"/> is a <typeparamref name="T"/>.
        /// Throws <see cref="ArgumentException"/> otherwise.
        /// </summary>
        /// <param name="reference">
        /// The reference function for argument name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// The type to check for compatibility.
        /// </typeparam>
        public static void AgainstTypeIncompatibility<T>(Expression<Func<object>> reference, object value)
        {
            if (!(value is T))
            {
                throw new ArgumentException(string.Format("Parameter must be a {0}. Actual: {1}", typeof(T), value.GetType()), GetParameterName(reference));
            }
        }

        /// <summary>
        /// Ensures the given <paramref name="value"/> is more than <paramref name="minValue"/>.
        /// Throws <see cref="ArgumentException"/> otherwise.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the argument.
        /// </typeparam>
        /// <param name="reference">
        /// The reference function for argument name.
        /// </param>
        /// <param name="value">
        /// The value to compare.
        /// </param>
        /// <param name="minValue">
        /// The minimal value to compare to.
        /// </param>
        public static void AgainstMinimalValue<T>(Expression<Func<T>> reference, T value, T minValue)
            where T : IComparable<T>
        {
            if (value.CompareTo(minValue) < 0)
            {
                throw new ArgumentException(string.Format("Parameter must be more than {0}. Actual: {1}.", minValue, value), GetParameterName(reference));
            }
        }

        /// <summary>
        /// Gets the parameter name from the <paramref name="reference"/>.
        /// </summary>
        /// <param name="reference">
        /// The reference.
        /// </param>
        /// <returns>
        /// The argument name, or Unknown if cannot be determined.
        /// </returns>
        private static string GetParameterName(Expression reference)
        {
            var lambda = reference as LambdaExpression;
            if (lambda == null)
            {
                return "Unknown";
            }

            var member = lambda.Body as MemberExpression;
            if (member == null)
            {
                return "Unknown";
            }

            return member.Member.Name;
        }
    }
}

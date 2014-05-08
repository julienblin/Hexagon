// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexagonException.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Hexagon <see cref="Exception" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Hexagon <see cref="Exception"/>.
    /// </summary>
    [Serializable]
    public class HexagonException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HexagonException"/> class.
        /// </summary>
        public HexagonException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexagonException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public HexagonException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexagonException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner exception.
        /// </param>
        public HexagonException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <inheritdoc />
        protected HexagonException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}

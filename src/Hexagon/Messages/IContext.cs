// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContext.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents the context of a message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the context of a message.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Gets the id of the message.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the correlation id.
        /// Messages that are related can share the same correlation id.
        /// </summary>
        Guid CorrelationId { get; set; }

        /// <summary>
        /// Gets the local timestamp at creation.
        /// </summary>
        DateTimeOffset Timestamp { get; }

        /// <summary>
        /// Gets the headers.
        /// Headers is a list of key/value pairs representing additional
        /// information about the context of a message.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Initializes correct values from <paramref name="context"/>.
        /// </summary>
        /// <param name="context">
        /// The context to initializes from.
        /// </param>
        void InitializeFrom(IContext context);
    }
}

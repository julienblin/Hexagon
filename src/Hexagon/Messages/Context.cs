// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Context.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IContext" /> standard implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// <see cref="IContext"/> standard implementation.
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
        {
            this.Id = Guid.NewGuid();
            this.CorrelationId = Guid.NewGuid();
            this.Timestamp = DateTimeOffset.Now;
            this.Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// Copy the <see cref="CorrelationId"/> and the <see cref="Headers"/>.
        /// </summary>
        /// <param name="context">
        /// The context to copy from.
        /// </param>
        public Context(IContext context)
        {
            Guard.AgainstNull(() => context, context);

            this.Id = Guid.NewGuid();
            this.CorrelationId = context.CorrelationId;
            this.Timestamp = DateTimeOffset.Now;
            this.Headers = context.Headers.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <inheritdoc />
        public Guid Id { get; private set; }

        /// <inheritdoc />
        public Guid CorrelationId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset Timestamp { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, string> Headers { get; private set; }
    }
}

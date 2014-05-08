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

        /// <inheritdoc />
        public Guid Id { get; private set; }

        /// <inheritdoc />
        public Guid CorrelationId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset Timestamp { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, string> Headers { get; private set; }

        /// <inheritdoc />
        public void InitializeFrom(IContext context)
        {
            Guard.AgainstNull(() => context, context);

            this.CorrelationId = context.CorrelationId;
            this.Headers = context.Headers.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("{0:N} / {1:N} / {2:O} / {3}", this.Id, this.CorrelationId, this.Timestamp, string.Join(",", this.Headers.Select(x => string.Format("{0}={1}", x.Key, x.Value))));
        }
    }
}

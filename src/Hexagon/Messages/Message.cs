// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   <see cref="IMessage" /> standard abstract implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// <see cref="IMessage"/> standard abstract implementation.
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
        {
            this.Context = new Context();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// Copy the context from the <paramref name="message"/>.
        /// </summary>
        /// <param name="message">
        /// The message to copy the context from.
        /// </param>
        protected Message(IMessage message)
        {
            Guard.AgainstNull(() => message, message);

            this.Context = new Context(message.Context);
        }

        /// <inheritdoc />
        public IContext Context { get; private set; }
    }
}

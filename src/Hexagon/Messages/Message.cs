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

        /// <inheritdoc />
        public IContext Context { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("{0}({1})", this.GetType().FullName, this.Context);
        }
    }
}

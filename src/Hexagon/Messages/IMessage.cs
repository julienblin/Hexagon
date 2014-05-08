// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents a base unit of message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// Represents a base unit of message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        IContext Context { get; }
    }
}

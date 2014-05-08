// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalHeaderKeys.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Holds constants for key values that various components can use to insert values into the <see cref="IContext.Headers" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Messages
{
    /// <summary>
    /// Holds constants for key values that various components can use to insert values into the <see cref="IContext.Headers"/>
    /// </summary>
    public static class InternalHeaderKeys
    {
        /// <summary>
        /// Prefix used by all the internal keys.
        /// </summary>
        public const string Prefix = @"_";

        /// <summary>
        /// The local processing time in milliseconds.
        /// </summary>
        public const string LocalProcessingTime = Prefix + @"localProcessingTime";

        /// <summary>
        /// <see cref="System.Environment.MachineName"/> of the handler.
        /// </summary>
        public const string HandlerMachineName = Prefix + @"handlerMachineName";
    }
}

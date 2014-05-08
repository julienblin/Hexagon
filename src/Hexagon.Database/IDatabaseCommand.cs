// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseCommand.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Represents command parameters for a database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database
{
    /// <summary>
    /// Represents command parameters for a database. 
    /// </summary>
    public interface IDatabaseCommand
    {
    }

    /// <summary>
    /// Represents command parameters for a database. 
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of result returned when executing the command.
    /// </typeparam>
    public interface IDatabaseCommand<TResult> : IDatabaseCommand
    {
    }
}

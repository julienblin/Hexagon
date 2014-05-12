// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandNoResultHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the CommandNoResultHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.CommandHandlers
{
    using System.Data.Entity;

    using Hexagon.Database.EF.Tests.Commands;

    public class CommandNoResultHandler : DbContextDatabaseCommandHandler<CommandNoResult>
    {
        private const string NoOpCommand = @"BEGIN WAITFOR DELAY '00:00:00' END";

        protected override void Handle(CommandNoResult command, DbContext context)
        {
            context.Database.ExecuteSqlCommand(NoOpCommand);
        }
    }
}

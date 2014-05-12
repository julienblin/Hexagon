// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandWithResultHandler.cs" company="Julien Blin">
//   Copyright (c) 2014 Julien Blin
// </copyright>
// <summary>
//   Defines the CommandWithResultHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Hexagon.Database.EF.Tests.CommandHandlers
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;

    using Hexagon.Database.EF.Tests.Commands;

    public class CommandWithResultHandler : DbContextDatabaseCommandHandler<CommandWithResult, DateTime>
    {
        private const string GetDateTSql = @"SELECT CAST(GETDATE() AS DATE)";

        protected override DateTime Handle(CommandWithResult command, DbContext context)
        {
            return context.Database.SqlQuery<DateTime>(GetDateTSql).First();
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Entity.Users;
using WebApp.Domain.Interface;

//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System.Data.Common;
//using Microsoft.Extensions.Logging;


namespace WebApp.Persistence.Data
{
  public class ApplicationDbContext
    :
       IdentityDbContext<
        AmfUser,
        AmfRole,
        long,
        AmfUserClaim,
        AmfUserRole,
        AmfUserLogin,
        AmfRoleClaim,
        AmfUserToken>, IDBContext
  //: IdentityDbContext, IDBContext
  {
    private readonly CustomDbCommandInterceptor _commandInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, CustomDbCommandInterceptor commandInterceptor)
    : base(options)
    {
      _commandInterceptor = commandInterceptor;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.AddInterceptors(_commandInterceptor);

      base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Product { get; set; }
  }



  public class CustomDbCommandInterceptor : DbCommandInterceptor
  {
    private readonly ILogger<CustomDbCommandInterceptor> _logger;

    public CustomDbCommandInterceptor(ILogger<CustomDbCommandInterceptor> logger)
    {
      _logger = logger;
    }

    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result)
    {
      _logger.LogInformation("Executing NonQuery: {CommandText}", command.CommandText);
      return base.NonQueryExecuting(command, eventData, result);
    }

    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result)
    {
      _logger.LogInformation("Executing Scalar: {CommandText}", command.CommandText);
      return base.ScalarExecuting(command, eventData, result);
    }

    public override DbCommand CommandInitialized(CommandEndEventData eventData, DbCommand result)
    {
      return base.CommandInitialized(eventData, result);
    }

    public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
      return base.CommandCreated(eventData, result);
    }

    //public override InterceptionResult<DbCommand> ReaderExecuting(
    //    DbCommand command,
    //    CommandEventData eventData,
    //    InterceptionResult<DbCommand> result)
    //{
    //  if (!command.CommandText.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
    //  {
    //    _logger.LogInformation("Executing Command: {CommandText}", command.CommandText);
    //  }
    //  return base.ReaderExecuting(command, eventData, result);
    //}
  }

}

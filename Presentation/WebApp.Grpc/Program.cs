using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services.Get;
using WebApp.Domain.Interface;
using WebApp.Grpc.Services;
using WebApp.Persistence.Data;
using WebApp.Application.Services;
using Microsoft.AspNetCore.Identity;
using WebApp.Domain.Entity.Users;
using WebApp.Infrastructure.Comon;

namespace WebApp.Grpc
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      var services=builder.Services;

      var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
      //builder.Services.AddDbContext<ApplicationDbContext>(options =>
      //    options.UseSqlServer(connectionString));

      services
       .AddDbContext<ApplicationDbContext>(
           op =>
           {
             op.UseSqlServer(connectionString);
             op.EnableSensitiveDataLogging();
           });

      services.AddIdentity<AmfUser, AmfRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddErrorDescriber<CustomIdentityError>()
        .AddUserManager<AmfUserManager>()
        .AddRoles<AmfRole>()
        .AddRoleManager<AmfRoleManager>()
        .AddDefaultTokenProviders()
        .Services
        .ConfigureApplicationCookie(options =>
        {
          options.SlidingExpiration = true;
          options.ExpireTimeSpan = TimeSpan.FromDays(180);
        });


      services.AddScoped<CustomDbCommandInterceptor>();

      builder.Services.AddEndpointsApiExplorer();

      builder.Services.AddProductService();
      builder.Services.AddScoped<IDBContext, ApplicationDbContext>();

      // Add services to the container.
      builder.Services.AddGrpc();
      builder.Services.AddGrpcReflection();


      var app = builder.Build();

      app.MapGrpcReflectionService();
      app.MapGrpcService<ProductsService>();

      // Configure the HTTP request pipeline.
      app.MapGrpcService<GreeterService>();
      app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

      app.Run();
    }
  }
}
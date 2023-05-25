using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((hostContext, services) =>
                {
                    services.AddControllers(options => {
                        options.Filters.Add<CustomExceptionHandler>(); // Register the global exception handler
                    });

                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();

                    services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(
                            policy  =>
                            {
                                policy
                                    .WithOrigins((Environment.GetEnvironmentVariable("ASPNETCORE_ALLOWED_ORIGINS") ?? "").Split(","))
                                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                    .WithExposedHeaders("access-control-allow-headers", "access-control-allow-origin", "access-control-allow-methods")
                                    .WithHeaders("Accept", "Origin", "X-Requested-With", "Content-Type", "Referer", "User-Agent", "Access-Control-Allow-Origin");
                            });
                    });

                    string server = Environment.GetEnvironmentVariable("MSSQL_IP") ?? "localhost";
                    string port = Environment.GetEnvironmentVariable("MSSQL_PORT") ?? "";
                    string database = Environment.GetEnvironmentVariable("MSSQL_DB") ?? "";
                    string user = Environment.GetEnvironmentVariable("MSSQL_USER") ?? "";
                    string password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD") ?? "";

                    string connectionString = $"Server={server},{port};Database={database};User Id={user};Password={password};TrustServerCertificate=true;";

                    // Configure the DbContext with the retrieved connection string
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString)
                    );

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    var requiredNamespaces = new List<string>{
                        "Com.Dotnet.Cric.Repositories",
                        "Com.Dotnet.Cric.Services"
                    };
                    var types = assembly.GetTypes()
                        .Where(type => requiredNamespaces.Contains(type.Namespace));
                    foreach (var type in types)
                    {
                        services.AddScoped(type);
                    }
                });
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseCors();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers(); // Map controllers
                    });
                });
                // webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:" + GetPortFromEnvironment());
            });

    private static string GetPortFromEnvironment()
    {
        return Environment.GetEnvironmentVariable("PORT") ?? "";
    }
}
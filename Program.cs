using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Exceptions;

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
                        options.Filters.Add<CustomExceptionHandler>();
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    }); ;

                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet-cric", Version = "1.0" });
                    });

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

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}
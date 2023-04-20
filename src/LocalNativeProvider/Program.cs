using Serilog;
using Npgsql;
using LocalNativeProvider.BusinessObjects;
using LocalNativeProvider.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, logger) => logger
    .ReadFrom.Configuration(hostBuilderContext.Configuration)
    .WriteTo.Async(config => config.Console())
);

builder.Services.AddLogging();
builder.Services.AddControllers();

builder.Services.AddSingleton(sp =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>()
        .GetConnectionString("Default");

    var result = new NpgsqlDataSourceBuilder(connectionString)
        .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
        .Build();

    return result;
});

builder.Services.AddTransient<BusinessObjectFilterConfigService>();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseRouting();
app.MapControllers();

app.InitializeBusinessObjects();

app.Run();
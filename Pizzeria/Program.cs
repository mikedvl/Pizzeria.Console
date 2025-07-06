using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Pizzeria.Application.Interfaces;
using Pizzeria.Application.Services;
using Pizzeria.Infrastructure.Configuration;
using Serilog;
using Pizzeria.Infrastructure.Parsers;
using Pizzeria.Infrastructure.Parsers.Csv;
using Pizzeria.Infrastructure.Parsers.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/bootstrap-log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        shared: true)
    .CreateLogger();

try
{
    Log.Information("Starting Pizzeria service");

    var builder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .UseSerilog((ctx, config) => 
            config.ReadFrom.Configuration(ctx.Configuration)) 
        .ConfigureServices((context, services) =>
        {
            services.Configure<DataFileSettings>(context.Configuration.GetSection("DataFiles"));

            services.AddScoped<CsvParser>();
            services.AddScoped<JsonParser>();
            services.AddScoped<IParserFactory, ParserFactory>();
            services.AddTransient<IOrderValidator, OrderValidator>();
            services.AddTransient<IOrderCalculator, OrderCalculator>();
            services.AddTransient<ISummaryPrinter, SummaryPrinter>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ISummaryResultFactory, SummaryResultFactory>();
        });

    var host = builder.Build();
    var app = host.Services.GetRequiredService<IOrderService>();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;
using SMW.ServiceOrder.Api.Shared.HealthChecks;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

_ = builder.Services.AddEndpointsApiExplorer();

_ = builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "SMW.ServiceOrder.Api"));
_ = builder.Logging.ClearProviders();

_ = builder.Services.AddOpenApi();
_ = builder.Services.AddHealthChecks()
    .AddCheck<DetailedHealthCheck>("detailed");
_ = builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
_ = builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();
_ = app.MapOpenApi();
_ = app.MapScalarApiReference(options =>
{
    options.WithTitle( "Smart Mechanical Workshop - Service Order API" )
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

_ = app.UseHttpsRedirection();
_ = app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

_ = app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .AddOpenApiOperationTransformer((operation, context, ct) =>
    {
        // Per-endpoint tweaks
        operation.Summary     = "Gets the current weather report.";
        operation.Description = "Returns a short description and emoji.";
        return Task.CompletedTask;
    });

await app.RunAsync();

public partial class Program
{
    protected Program() { }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}

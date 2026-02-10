using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;
using SMW.ServiceOrder.Api.Endpoints;
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
    options.WithTitle("Smart Mechanical Workshop - Service Order API")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});
_ = app.UseHttpsRedirection();
_ = app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
_ = app.MapServiceOrderEndpoints();

await app.RunAsync();

namespace SMW.ServiceOrder.Api
{
    public partial class Program
    {
        protected Program() { }
    }
}

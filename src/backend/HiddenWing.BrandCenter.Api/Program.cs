using System.Text.Json;
using System.Text.Json.Serialization;
using HiddenWing.BrandCenter.Api.Extensions;
using HiddenWing.BrandCenter.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationActionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<ValidationActionFilter>();
builder.Services.AddBrandCenterServices(builder.Configuration);

var app = builder.Build();
await app.ApplyMigrationsAsync();
app.UseBrandCenterPipeline();
app.Run();

public partial class Program;

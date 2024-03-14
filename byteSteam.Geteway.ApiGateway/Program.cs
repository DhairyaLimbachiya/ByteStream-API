using byteStream.Gateway.ApiGateway.Extension;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Jwt Authentication Configurations
builder.AddAppAuthentication();

// CORS Policy
builder.Services.AddCors();

// ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseOcelot().GetAwaiter().GetResult();
app.MapGet("/", () => "Hello World!");

app.Run();

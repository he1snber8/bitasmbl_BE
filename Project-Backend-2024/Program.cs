using Project_Backend_2024;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddSwaggerBearer();

// Add controllers and API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context,config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Clear default logging providers
builder.Logging.ClearProviders();

// Add Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.AddSwagger();
app.MapControllers();

app.Run();
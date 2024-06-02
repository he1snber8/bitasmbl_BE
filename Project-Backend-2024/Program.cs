
using Project_Backend_2024;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.IncludeAppSettingsConfiguration(builder.Configuration);

var app = builder.Build();

app.AddAppMiddleware();
app.Run();
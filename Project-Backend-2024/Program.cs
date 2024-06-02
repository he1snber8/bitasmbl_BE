
using Project_Backend_2024;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations(builder.Configuration);

var app = builder.Build();
app.AddAppMiddleware();
app.Run();
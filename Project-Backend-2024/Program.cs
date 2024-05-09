using Project_Backend_2024;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddSwaggerBearer();

// Add controllers and API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.AddSwagger();
app.MapControllers();

app.Run();
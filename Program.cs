var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
});

// Add services to the container
builder.Services.AddControllers();

var app = builder.Build();

// Serve static files from wwwroot
app.UseStaticFiles();

// Map the API routes
app.MapControllers();

// Configure the default file to serve index.html when accessing the root
app.UseDefaultFiles(); // Looks for default files like index.html automatically
app.UseStaticFiles();

app.UseHttpsRedirection();

// Bind to the port provided by Render (or default to 5000)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");



app.Run();

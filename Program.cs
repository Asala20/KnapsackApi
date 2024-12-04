var builder = WebApplication.CreateBuilder(args);

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

app.Run();

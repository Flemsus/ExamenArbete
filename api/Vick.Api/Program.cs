using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vick.Api.Endpoints.Internal;
using Vick.Core.Interfaces;
using Vick.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with the provided connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add authorization and authentication services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// Register UserService as a singleton service
builder.Services.AddSingleton<IUserService, UserService>();

// Configure CORS policy
ConfigureCors(builder);

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAllOrigins");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Define root endpoint response
app.MapGet("/", () => "I'm alive!");

// Define endpoints for internal API
app.UseEndpoints<EndpointMarker>();

// Start the application
app.Run();

// Method to configure CORS policy
static void ConfigureCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: "AllowAllOrigins",
            policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
    });
}

// DbContext class representing the application's database
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

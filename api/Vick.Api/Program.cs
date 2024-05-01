// Starting point of the application

using Microsoft.EntityFrameworkCore;
using Vick.Api.Endpoints;
using Vick.Api.Endpoints.Internal;
using Vick.Core.Interfaces;
using Vick.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Should add identity??
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add authorization
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// TODO: Should not be Singleton when we have a persistant storage/database 
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITimeSlotService, TimeSlotService>();

ConfigureCors(builder);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

// Just a simple health check
app.MapGet("/", () => "I'm alive!");

// Define endpoints for all classes that implement IEndpoints
app.UseEndpoints<EndpointMarker>();

app.Run();


static void ConfigureCors(WebApplicationBuilder builder)
{
    // TODO: Use a better way to configure CORS
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

// This part is definitely better somewhere else
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

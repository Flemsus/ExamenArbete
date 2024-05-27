using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vick.Api.Endpoints.Internal;
using Vick.Core.Interfaces;
using Vick.Core.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication();


builder.Services.AddScoped<IUserService, UserService>();


ConfigureCors(builder);

var app = builder.Build();


app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "If you see this the backend is probably on!");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    UserEndpoints.DefineEndpoints(endpoints);
});

app.Run();

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

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
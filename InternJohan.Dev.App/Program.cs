using InternJohan.Dev.API.Services;
using InternJohan.Dev.Infrastructure;
using InternJohan.Dev.Infrastructure.Configuration;
using InternJohan.Dev.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using InternJohan.Dev.App;
using InternJohan.Dev.Infrastructure.Models;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);

// Konfigurera anslutningssträngar och andra inställningar
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Lägg till Entity Framework Core-tjänsten och använd din ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Använd den anslutningssträng du definierade i appsettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Lägg till dina andra tjänster (repositories, services, etc.)
builder.Services.AddTransient<SportEventRepository>();
builder.Services.AddTransient<SportEventService>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddTransient<RoleService>();

// Lägg till controllers
builder.Services.AddControllers();
builder.Services.AddScoped<PasswordService>();

// Lägg till Swagger/OpenAPI om du använder det
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lägg till CORS-policy om du har en
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsRule", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Lägg till `IHttpContextAccessor`-tjänsten
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Lägg till autentisering och auktoriseringstjänster
builder.Services.AddAuthentication(); // Lägg till autentiseringstjänster
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SportEventAccess", policy =>
        policy.RequireAssertion(context =>
            // Kontrollera om användaren är SportEventOwner eller har Admin/Moderator roll
            context.User.IsInRole("Admin") || context.User.IsInRole("Moderator") ||
            (context.User.Identity.IsAuthenticated &&
             context.Resource is HttpContext httpContext &&
             httpContext.RequestServices.GetService<SportEventService>() is SportEventService sportEventService &&
             int.TryParse(httpContext.Request.RouteValues["id"]?.ToString(), out int sportEventId) &&
             sportEventService.FindById(sportEventId).Result is SportEvent sportEvent &&
             sportEvent.UserHost?.Id == int.Parse(context.User.Identity.Name)))
        );
});

var app = builder.Build();

// Använd CORS-policy om du har definierat en
app.UseCors("CorsRule");

// Om applikationen är i utvecklingsläge, använd Swagger och SwaggerUI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Använd HTTPS om det behövs
app.UseHttpsRedirection();

// Använd autentisering och auktorisering
app.UseAuthentication(); // Lägg till autentiseringstjänster
app.UseAuthorization();

// Mappa controllers
app.MapControllers();

// Konfigurera SPA om du har en client app
app.MapWhen(x => !x.Request.Path.StartsWithSegments("/api"), x =>
{
    app.UseSpa(spa =>
    {
        if (!builder.Environment.IsDevelopment())
            spa.Options.SourcePath = "wwwroot/clientapp";
        else
            spa.Options.SourcePath = @"C:\Data\Repos\KEYnet\InternJohan.Dev.App\wwwroot\clientapp";
    });
});

// Kör applikationen
app.Run();

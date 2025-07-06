using Domain.Interface;
using Domain.Repositories;
using Domain.Service;
using Entity.Context;
using Entity.Repositories;
using Entity.SeedData;
using Entity.WorkContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.SQLite(
        sqliteDbPath: Environment.CurrentDirectory + @"/Logs.db", // or path to your DB file
        tableName: "Logs"
    ).CreateLogger();


builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

Log.Logger.Error("Test");

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Web") // ???? ?? ??? ????? ??? Web
    ));


#region "Authentication"
//JWT API authentication service
var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            SaveSigninToken = true,
            ValidateLifetime = true,
        };
    });
#endregion

// Dependency Injection
builder.Services.AddScoped<SessionProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IItemService, ItemsService>();
builder.Services.AddScoped<ILogsService, LogsService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


#region "CORS"
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy-public",
        builder => builder.SetIsOriginAllowed(origin => true)  //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
            .AllowAnyMethod()
            .AllowAnyHeader()
    .AllowCredentials()
    .Build());
});
#endregion

// Swagger
builder.Services.AddEndpointsApiExplorer();

#region "Swagger API"

builder.Services.AddSwaggerGen(c =>
{
    //c.CustomSchemaIds(type => type.ToString());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API's", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference{
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },new List<string>()
                        }
                    });

});
#endregion

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "client/dist/client/browser";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Run seeders
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // زرع البيانات فقط إذا كانت الجداول فارغة
        if (!dbContext.Roles.Any())
            SeedRole.Seed(dbContext);

        if (!dbContext.Users.Any())
            SeedUser.Seed(dbContext);

        if (!dbContext.Countries.Any())
            SeedCountries.Seed(dbContext);

        if (!dbContext.Warehouses.Any())
            SeedWarehouse.Seed(dbContext);

        if (!dbContext.WarehouseItems.Any())
            SeedItems.Seed(dbContext);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"??? ????? ????? ????????: {ex.Message}");
        throw;
    }
}


// Configure middleware
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseSpaStaticFiles();

app.UseCors("CorsPolicy-public");  //apply to every request


app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client/dist/client/browser";

    //if (app.Environment.IsDevelopment())
    //{
    //    // استخدم البروكسي إلى خادم Angular بدل UseAngularCliServer
    //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    //}
});

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "client";
//    //spa.UseAngularCliServer(npmScript: "ng serve");
//});

app.Run();

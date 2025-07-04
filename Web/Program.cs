using Domain.Interface;
using Domain.Service;
using Entity.Context;
using Entity.SeedData;
using Entity.WorkContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Web") // ???? ?? ??? ????? ??? Web
    ));

// Dependency Injection
builder.Services.AddScoped<SessionProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();


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
builder.Services.AddSwaggerGen();

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
        SeedRole.Seed(dbContext);
        SeedUser.Seed(dbContext);
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
app.UseAuthorization();

app.MapControllers();



app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client/dist/client/browser";

    //if (app.Environment.IsDevelopment())
    //{
    //    // ?????? ???????? ??? ???? Angular ??? UseAngularCliServer
    //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    //}
});

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "client";
//    //spa.UseAngularCliServer(npmScript: "ng serve");
//});

app.Run();

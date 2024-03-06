using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentaion();
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.

app.UseSwaggerDocumentation();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Applying Migration and creating the database at app startup without using CLI commands 
//if the migration is applied it will not be applied again and it will pass throw this method and run the app

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();   
try
{
    await context.Database.MigrateAsync();
    await IdentityContext.Database.MigrateAsync();
    //adding seed contents json file to the darabse 
    await StoreContentSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedusersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex,"Error occured during migration");
}

app.Run();

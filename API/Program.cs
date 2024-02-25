using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAppServices(builder.Configuration);
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

//Applying Migration and creating the database at app startup without using CLI commands 
//if the migration is applied it will not be applied again and it will pass throw this method and run the app

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();   
try
{
    await context.Database.MigrateAsync();
    //adding seed contents json file to the darabse 
    await StoreContentSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex,"Error occured during migration");
}

app.Run();

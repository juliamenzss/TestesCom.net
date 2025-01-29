using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaNaLista.Data;
using TaNaLista.Interfaces;
using TaNaLista.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var services = builder.Services;

var connectionString = configuration.GetConnectionString("TaNaListaContext")
    ?? throw new InvalidOperationException("Connection string 'TaNaListaContext' not found.");

services.AddDbContext<TaNaListaContext>(options =>
{
    options.UseSqlite(connectionString, p =>
    {
        p.MigrationsHistoryTable("__EFMigrationsHistory", "tanalista");
    });
});

builder.Services.AddScoped<IUserService, UserServiceDB>(); 
builder.Services.AddScoped<IProductService, ProductService>();
// configuração de interface quando utilizamos ela como herança
builder.Services.AddScoped<IProductContext, TaNaListaContext>();
builder.Services.AddScoped<IUserContext, TaNaListaContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaNaListaContext>();
    context.Database.Migrate();
}

app.Run();

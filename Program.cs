global using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using InventoryManagementBackend.Data;
using InventoryManagementBackend.Endpoints;
using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryDbContext>();

builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IRepository<Sale>, Repository<Sale>>();
builder.Services.AddScoped<IRepository<Storage>, Repository<Storage>>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithDarkModeToggle(false)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithTitle("Inventory Management Backend");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapCategoriesEndpoints();
app.MapProductEndpoints();
app.MapSaleEndpoints();
app.MapStoragesEndpoints();

app.Run();
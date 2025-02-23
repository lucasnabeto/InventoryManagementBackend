global using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using InventoryManagement.Data;
using InventoryManagement.Endpoints;
using InventoryManagement.Entities;
using InventoryManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryDbContext>();

builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();

builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();

builder.Services.AddScoped<IRepository<Sale>, Repository<Sale>>();

builder.Services.AddScoped<IRepository<Storage>, Repository<Storage>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithDarkModeToggle(false)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithTitle("Inventory Management");
    });
}

app.UseHttpsRedirection();

app.MapCategoriesEndpoints();

app.MapProductEndpoints();

app.MapSaleEndpoints();

app.MapStoragesEndpoints();

app.Run();

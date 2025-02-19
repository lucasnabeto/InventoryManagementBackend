global using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Endpoints;
using InventoryManagement.Entities;
using InventoryManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryDbContext>();

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app
    .MapSaleEndpoints()
    .MapProductEndpoints();

app.Run();

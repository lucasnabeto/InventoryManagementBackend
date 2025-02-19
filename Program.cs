global using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGroup("/api/sales").MapSalesEndpoints();

app.MapGroup("/api/products").MapProductsEndpoints();

app.Run();

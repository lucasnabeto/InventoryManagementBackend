using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Endpoints;

public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProductsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (InventoryDbContext context) => Results.Ok(await context.Products.ToListAsync()));

        group.MapGet("/{id:int}", async (InventoryDbContext context, int id) =>
        {
            Product? product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(product);
        });

        group.MapPost("/", async (InventoryDbContext context, Product newProduct) =>
        {
            await context.AddAsync(newProduct);

            await context.SaveChangesAsync();

            return Results.Created($"/api/products/{newProduct.Id}", newProduct);
        });

        group.MapPut("/{id:int}", async (InventoryDbContext context, int id, Product updatedProduct) =>
        {
            Product? product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.Description = updatedProduct.Description;
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.StockQuantity = updatedProduct.StockQuantity;

            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (InventoryDbContext context, int id) =>
        {
            Product? product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
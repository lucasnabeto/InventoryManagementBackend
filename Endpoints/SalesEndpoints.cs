using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Endpoints;

public static class SalesEndpoints
{
    public static RouteGroupBuilder MapSalesEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (InventoryDbContext context) =>
        {
            List<Sale> allSales = await context.Sales.ToListAsync();
            return Results.Ok(allSales);
        });

        group.MapGet("/{id:int}", async (InventoryDbContext context, int id) =>
        {

            Sale? sale = await context.Sales.FindAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(sale);
        });

        group.MapPost("/", async (InventoryDbContext context, Sale newSale) =>
        {
            Product? product = await context.Products.FindAsync(newSale.ProductId);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.StockQuantity -= newSale.QuantitySold;
            await context.Sales.AddAsync(newSale);
            await context.SaveChangesAsync();

            return Results.Created($"/sales/{newSale.Id}", newSale);
        });

        group.MapPut("/{id:int}", async (InventoryDbContext context, int id, Sale updatedSale) =>
        {
            Sale? sale = await context.Sales.FindAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            sale.Price = updatedSale.Price;
            sale.Product = updatedSale.Product;
            sale.ProductId = updatedSale.ProductId;
            sale.QuantitySold = updatedSale.QuantitySold;
            sale.SaleDate = updatedSale.SaleDate;

            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (InventoryDbContext context, int id) =>
        {
            Sale? sale = await context.Sales.FindAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            context.Sales.Remove(sale);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        return group;
    }
}
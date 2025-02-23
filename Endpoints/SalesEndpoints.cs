using InventoryManagementBackend.Data;
using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Repositories;

namespace InventoryManagementBackend.Endpoints;

public static class SaleEndpoints
{
    public static IEndpointRouteBuilder MapSaleEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var saleGroup = routeBuilder.MapGroup("api/sales");

        saleGroup.MapGet("/", async (IRepository<Sale> repository) =>
        {
            ICollection<Sale> allSales = await repository.GetAllAsync();
            return Results.Ok(allSales);
        });

        saleGroup.MapGet("/{id:int}", async (IRepository<Sale> repository, int id) =>
        {

            Sale? sale = await repository.GetByIdAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(sale);
        }).WithName("GetSaleById");

        saleGroup.MapPost("/", async (IRepository<Product> productRepository, IRepository<Sale> saleRepository, Sale newSale) =>
        {
            Product? product = await productRepository.GetByIdAsync(newSale.ProductId);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.StockQuantity -= newSale.QuantitySold;

            await productRepository.UpdateAsync(product);
            await saleRepository.CreateAsync(newSale);

            return Results.Created("GetSaleById", newSale);
        });

        saleGroup.MapPut("/{id:int}", async (IRepository<Sale> repository, int id, Sale updatedSale) =>
        {
            Sale? sale = await repository.GetByIdAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            sale.FullPrice = updatedSale.FullPrice;
            sale.Product = updatedSale.Product;
            sale.ProductId = updatedSale.ProductId;
            sale.QuantitySold = updatedSale.QuantitySold;
            sale.Date = updatedSale.Date;

            await repository.UpdateAsync(sale);

            return Results.NoContent();
        });

        saleGroup.MapDelete("/{id:int}", async (IRepository<Sale> repository, int id) =>
        {
            Sale? sale = await repository.GetByIdAsync(id);
            if (sale is null)
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(sale);

            return Results.NoContent();
        });

        return routeBuilder;
    }
}
using InventoryManagement.Entities;
using InventoryManagement.Repositories;

namespace InventoryManagement.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var productGroup = routeBuilder.MapGroup("/api/products");

        productGroup.MapGet("/", async (IRepository<Product> repository) =>
        {
            ICollection<Product> allProducts = await repository.GetAllAsync();
            return Results.Ok(allProducts);
        });

        productGroup.MapGet("/{id:int}", async (IRepository<Product> repository, int id) =>
        {
            Product? product = await repository.GetByIdAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(product);
        }).WithName("GetProductById");

        productGroup.MapPost("/", async (IRepository<Product> repository, Product newProduct) =>
        {
            await repository.CreateAsync(newProduct);
            return Results.Created("GetProductById", newProduct);
        });

        productGroup.MapPut("/{id:int}", async (IRepository<Product> repository, int id, Product updatedProduct) =>
        {
            Product? product = await repository.GetByIdAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.Description = updatedProduct.Description;
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.StockQuantity = updatedProduct.StockQuantity;
            product.ExpirationDate = updatedProduct.ExpirationDate;
            product.CategoryId = updatedProduct.CategoryId;
            product.StorageId = updatedProduct.StorageId;

            await repository.UpdateAsync(product);

            return Results.NoContent();
        });

        productGroup.MapDelete("/{id:int}", async (IRepository<Product> repository, int id) =>
        {
            Product? product = await repository.GetByIdAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(product);

            return Results.NoContent();
        });

        return routeBuilder;
    }
}
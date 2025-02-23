using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Repositories;

namespace InventoryManagementBackend.Endpoints;

public static class StoragesEndpoints
{
    public static IEndpointRouteBuilder MapStoragesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var storageGroup = routeBuilder.MapGroup("/api/storages");

        storageGroup.MapGet("/", async (IRepository<Storage> repository) =>
        {
            ICollection<Storage> allStorages = await repository.GetAllAsync();
            return Results.Ok(allStorages);
        });

        storageGroup.MapGet("/{id:int}", async (IRepository<Storage> repository, int id) =>
        {
            Storage? storage = await repository.GetByIdAsync(id);
            if (storage is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(storage);
        }).WithName("GetStorageById");

        storageGroup.MapPost("/", async (IRepository<Storage> repository, Storage newStorage) =>
        {
            await repository.CreateAsync(newStorage);
            return Results.Created("GetStorageById", newStorage);
        });

        storageGroup.MapPut("/{id:int}", async (IRepository<Storage> repository, int id, Storage updatedStorage) =>
        {
            Storage? storage = await repository.GetByIdAsync(id);
            if (storage is null)
            {
                return Results.NotFound();
            }

            storage.Description = updatedStorage.Description;
            storage.Location = updatedStorage.Location;
            storage.Products = updatedStorage.Products;

            await repository.UpdateAsync(storage);

            return Results.NoContent();
        });

        storageGroup.MapDelete("/{id:int}", async (IRepository<Storage> repository, int id) =>
        {
            Storage? storage = await repository.GetByIdAsync(id);
            if (storage is null)
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(storage);
            return Results.NoContent();
        });

        return storageGroup;
    }
}
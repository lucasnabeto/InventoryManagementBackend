using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Repositories;

namespace InventoryManagementBackend.Endpoints;

public static class CategoriesEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var categoryGroup = routeBuilder.MapGroup("/api/categories");

        categoryGroup.MapGet("/", async (IRepository<Category> repository) =>
        {
            ICollection<Category> allCategories = await repository.GetAllAsync();
            return Results.Ok(allCategories);
        });

        categoryGroup.MapGet("/{id:int}", async (IRepository<Category> repository, int id) =>
        {
            Category? category = await repository.GetByIdAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(category);
        }).WithName("GetCategoryById");

        categoryGroup.MapPost("/", async (IRepository<Category> repository, Category newCategory) =>
        {
            await repository.CreateAsync(newCategory);
            return Results.Created("GetCategoryById", newCategory);
        });

        categoryGroup.MapPut("/{id:int}", async (IRepository<Category> repository, int id, Category updatedCategory) =>
        {
            Category? category = await repository.GetByIdAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            category.Name = updatedCategory.Name;
            category.Products = updatedCategory.Products;

            await repository.UpdateAsync(category);

            return Results.NoContent();
        });

        categoryGroup.MapDelete("/{id:int}", async (IRepository<Category> repository, int id) =>
        {
            Category? category = await repository.GetByIdAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(category);

            return Results.NoContent();
        });

        return categoryGroup;
    }
}
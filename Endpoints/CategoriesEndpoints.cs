using InventoryManagementBackend.Dtos;
using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Repositories;
using Mapster;

namespace InventoryManagementBackend.Endpoints;

public static class CategoriesEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var categoryGroup = routeBuilder.MapGroup("/api/categories");

        categoryGroup.MapGet("/", async (IRepository<Category> repository) =>
        {
            ICollection<Category> allCategories = await repository.GetAllAsync();

            ICollection<CategoryDTO> allCategoriesDTO = allCategories.Adapt<ICollection<CategoryDTO>>();

            return Results.Ok(allCategoriesDTO);
        });

        categoryGroup.MapGet("/{id:int}", async (IRepository<Category> repository, int id) =>
        {
            Category? category = await repository.GetByIdAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            CategoryDTO categoryDTO = category.Adapt<CategoryDTO>();

            return Results.Ok(categoryDTO);
        }).WithName("GetCategoryById");

        categoryGroup.MapPost("/", async (IRepository<Category> repository, CategoryDTO newCategoryDTO) =>
        {
            Category newCategory = newCategoryDTO.Adapt<Category>();

            await repository.CreateAsync(newCategory);
            return Results.Created("GetCategoryById", newCategory);
        });

        categoryGroup.MapPut("/{id:int}", async (IRepository<Category> repository, int id, CategoryDTO updatedCategoryDTO) =>
        {
            Category? category = await repository.GetByIdAsync(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            TypeAdapterConfig<CategoryDTO, Category>
                .NewConfig()
                .Ignore(dest => dest.Id);

            updatedCategoryDTO.Adapt(category);

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
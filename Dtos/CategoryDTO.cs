using System.ComponentModel.DataAnnotations;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Dtos;

public record CategoryDTO(
    int Id,
    string Name,
    ICollection<Product> Products
);

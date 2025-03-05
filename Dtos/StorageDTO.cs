using System.ComponentModel.DataAnnotations;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Dtos;

public record StorageDTO(
    int Id,
    string Description,
    string Location,
    ICollection<Product> Products
);
using System.ComponentModel.DataAnnotations;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Dtos;

public record ProductDTO(
    int Id,
    string Name,
    string? Description,
    decimal AcquisitionPrice,
    decimal SellingPrice,
    int StockQuantity,
    DateOnly ExpirationDate,
    Category Category,
    Storage Storage
);
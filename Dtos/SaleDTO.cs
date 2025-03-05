using System.ComponentModel.DataAnnotations;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Dtos;

public record SaleDTO(
    int Id,
    DateOnly Date,
    decimal Discount,
    decimal EffectivePrice,
    decimal FullPrice,
    int QuantitySold,
    Product Product
);
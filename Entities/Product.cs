using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementBackend.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(24, ErrorMessage = "The Name field is mandatory and must have a maximum of 24 characters.")]
    public string? Name { get; set; }

    [StringLength(128, ErrorMessage = "The Name field is mandatory and must have a maximum of 128 characters.")]
    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [Required]
    public DateOnly ExpirationDate { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    [Required]
    [ForeignKey("Storage")]
    public int StorageId { get; set; }

    public Storage Storage { get; set; }
}
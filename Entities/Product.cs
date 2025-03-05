using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementBackend.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(24)]
    public string Name { get; set; } = string.Empty;

    [StringLength(128)]
    public string? Description { get; set; }

    [Required]
    public decimal AcquisitionPrice { get; set; }

    [Required]
    public decimal SellingPrice { get; set; }

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
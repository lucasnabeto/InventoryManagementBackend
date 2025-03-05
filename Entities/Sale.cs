using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementBackend.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public decimal Discount { get; set; }

    [Required]
    public decimal EffectivePrice { get; set; }

    [Required]
    public decimal FullPrice { get; set; }

    [Required]
    public int QuantitySold { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public Product Product { get; set; }
}
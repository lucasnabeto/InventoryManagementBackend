using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementBackend.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int QuantitySold { get; set; }

    public decimal FullPrice { get; set; }

    public decimal EffectivePrice { get; set; }

    public DateOnly Date { get; set; }

    public decimal Discount { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public Product Product { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementBackend.Entities;

public class Category()
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(24)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}
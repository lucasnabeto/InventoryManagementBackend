using System.ComponentModel.DataAnnotations;

namespace InventoryManagementBackend.Entities;

public class Category()
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(24, ErrorMessage = "The Name field is mandatory and must have a maximum of 24 characters.")]
    public string? Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
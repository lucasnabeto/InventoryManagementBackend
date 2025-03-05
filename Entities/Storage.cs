using System.ComponentModel.DataAnnotations;

namespace InventoryManagementBackend.Entities;

public class Storage()
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(24)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(24)]
    public string Location { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}
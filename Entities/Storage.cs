namespace InventoryManagement.Entities;

public class Storage()
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}
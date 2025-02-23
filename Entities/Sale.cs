namespace InventoryManagement.Entities;

public class Sale
{
    public int Id { get; set; }

    public int QuantitySold { get; set; }

    public decimal FullPrice { get; set; }

    public decimal EffectivePrice { get; set; }

    public DateOnly Date { get; set; }

    public decimal Discount { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }
}
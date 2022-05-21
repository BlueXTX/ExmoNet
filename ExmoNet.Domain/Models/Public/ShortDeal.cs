namespace ExmoNet.Domain.Models;

public record ShortDeal
{
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
}

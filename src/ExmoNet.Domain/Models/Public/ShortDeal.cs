namespace ExmoNet.Domain.Models.Public;

public record ShortDeal
{
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
}

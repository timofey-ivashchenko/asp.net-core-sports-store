namespace SportsStore.Models;

public class CartLine
{
	// ReSharper disable once UnusedMember.Global
	public int CartLineID { get; set; }

	public Product Product { get; set; } = new();

	public int Quantity { get; set; }

	public decimal ComputeSubtotalValue() =>
		Quantity * Product.Price;
}
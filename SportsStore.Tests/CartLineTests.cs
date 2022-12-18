using SportsStore.Models;

namespace SportsStore.Tests;

public class CartLineTests
{
	[Fact]
	public void CanComputeSubtotalValue()
	{
		// Arrange.

		var product = new Product
		{
			ProductID = 1,
			Name = "Квартира на ж/м Коммунар",
			Price = 150000,
			Category = "Недвижимость"
		};

		var line = new CartLine
		{
			Product = product,
			Quantity = 2
		};

		// Act.

		var subtotal = line.ComputeSubtotalValue();

		// Assert.

		Assert.Equal(300000, subtotal);
	}
}
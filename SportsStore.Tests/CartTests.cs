using SportsStore.Models;

namespace SportsStore.Tests;

public class CartTests
{
	[Fact]
	public void CanAddNewLinesAndQuantityForExistingLines()
	{
		var product1 = new Product
		{
			ProductID = 1,
			Name = "Apple iPhone",
			Category = "Смартфоны"
		};

		var product2 = new Product
		{
			ProductID = 2,
			Name = "Porsche Cayenne",
			Category = "Автомобили"
		};

		var product3 = new Product
		{
			ProductID = 3,
			Name = "Квартира на ж/м Коммунар",
			Category = "Недвижимость"
		};

		var cart = new Cart();
		Assert.Empty(cart.Lines);
		
		cart.AddItem(product1, 1);
		Assert.Single(cart.Lines);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);

		cart.AddItem(product2, 1);
		Assert.Equal(2, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(1, cart.Lines[1].Quantity);

		cart.AddItem(product3, 1);
		Assert.Equal(3, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(1, cart.Lines[1].Quantity);
		Assert.Same(product3, cart.Lines[2].Product);
		Assert.Equal(1, cart.Lines[2].Quantity);

		cart.AddItem(product1, 5);
		Assert.Equal(3, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(6, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(1, cart.Lines[1].Quantity);
		Assert.Same(product3, cart.Lines[2].Product);
		Assert.Equal(1, cart.Lines[2].Quantity);

		cart.AddItem(product2, 1);
		Assert.Equal(3, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(6, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(2, cart.Lines[1].Quantity);
		Assert.Same(product3, cart.Lines[2].Product);
		Assert.Equal(1, cart.Lines[2].Quantity);

		cart.AddItem(product3, 1);
		Assert.Equal(3, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(6, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(2, cart.Lines[1].Quantity);
		Assert.Same(product3, cart.Lines[2].Product);
		Assert.Equal(2, cart.Lines[2].Quantity);
	}

	[Fact]
	public void CanRemoveLine()
	{
		// Arrange.
		
		var product1 = new Product
		{
			ProductID = 1,
			Name = "Nike Air Zoom Pegasus 39 By You",
			Category = "Спортивная обувь"
		};

		var product2 = new Product
		{
			ProductID = 2,
			Name = "Fender Stratocaster USA",
			Category = "Электрогитары"
		};

		var product3 = new Product
		{
			ProductID = 3,
			Name = "Beats Studio 3",
			Category = "Наушники"
		};

		var cart = new Cart();
		cart.AddItem(product1, 1);
		cart.AddItem(product2, 1);
		cart.AddItem(product3, 1);

		Assert.Equal(3, cart.Lines.Count);
		Assert.Same(product1, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);
		Assert.Same(product2, cart.Lines[1].Product);
		Assert.Equal(1, cart.Lines[1].Quantity);
		Assert.Same(product3, cart.Lines[2].Product);
		Assert.Equal(1, cart.Lines[2].Quantity);

		// Acts & asserts.

		cart.RemoveLine(product1);
		Assert.Equal(2, cart.Lines.Count);
		Assert.Same(product2, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);
		Assert.Same(product3, cart.Lines[1].Product);
		Assert.Equal(1, cart.Lines[1].Quantity);

		cart.RemoveLine(product3);
		Assert.Single(cart.Lines);
		Assert.Same(product2, cart.Lines[0].Product);
		Assert.Equal(1, cart.Lines[0].Quantity);

		cart.RemoveLine(product2);
		Assert.Empty(cart.Lines);
	}

	[Fact]
	public void CanCalculateCartTotal()
	{
		// Arrange.
		
		var product1 = new Product
		{
			ProductID = 1,
			Name = "Apple iPhone",
			Category = "Смартфоны",
			Price = 2500
		};

		var product2 = new Product
		{
			ProductID = 2,
			Name = "Porsche Cayenne",
			Category = "Автомобили",
			Price = 150000
		};

		var product3 = new Product
		{
			ProductID = 3,
			Name = "Квартира на ж/м Коммунар",
			Category = "Недвижимость",
			Price = 100000
		};

		var cart = new Cart();
		cart.AddItem(product1, 6);
		cart.AddItem(product2, 2);
		cart.AddItem(product3, 2);

		// Act.

		var totalValue = cart.ComputeTotalValue();

		// Assert.

		Assert.Equal(515000, totalValue);
	}
}
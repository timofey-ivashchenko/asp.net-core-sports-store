using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models;
using SportsStore.Pages;
using System.Text;
using System.Text.Json;

namespace SportsStore.Tests;

public class CartPageTests
{
	[Fact]
	public void CanLoadCart()
	{
		// Arrange.

		var product1 = new Product
		{
			ProductID = 1,
			Name = "MSI GS77 Stealth 12UHS",
			Category = "Ноутбуки",
			Price = 157000
		};

		var product2 = new Product
		{
			ProductID = 2,
			Name = "Apple AirPods Max",
			Category = "Наушники",
			Price = 22350
		};

		var repository = new Mock<IStoreRepository>();
		repository.Setup(x => x.Products).Returns(
			new[] { product1, product2 }.AsQueryable());

		var cart = new Cart();
		cart.AddItem(product1, 3);
		cart.AddItem(product2, 3);

		var session = new Mock<ISession>();
		var cartData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cart));
		session.Setup(x => x.TryGetValue(It.IsAny<string>(), out cartData));

		var context = new Mock<HttpContext>();
		context.Setup(x => x.Session).Returns(session.Object);

		// Act.

		var cartModel = new CartModel(repository.Object, cart)
		{
			PageContext = new(new()
			{
				ActionDescriptor = new PageActionDescriptor(),
				HttpContext = context.Object,
				RouteData = new()
			})
		};

		cartModel.OnGet("myUrl");

		// Assert.

		Assert.Equal(2, cartModel.Cart.Lines.Count);
		Assert.Equal("myUrl", cartModel.ReturnUrl);
	}

	[Fact]
	public void CanUpdateCart()
	{
		// Arrange.

		var product1 = new Product
		{
			ProductID = 1,
			Name = "Дом в Таромском",
			Description = "Мой будущий дом.",
			Category = "Недвижимость",
			Price = 300000
		};

		var repository = new Mock<IStoreRepository>();
		repository.Setup(x => x.Products).Returns(
			new[] { product1 }.AsQueryable());

		var cart = new Cart();

		var session = new Mock<ISession>();
		session.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
			.Callback<string, byte[]>((_, value) =>
			{
				cart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(value));
			});

		var context = new Mock<HttpContext>();
		context.Setup(x => x.Session).Returns(session.Object);

		// Act.

		var cartModel = new CartModel(repository.Object, cart)
		{
			PageContext = new(new()
			{
				ActionDescriptor = new PageActionDescriptor(),
				HttpContext = context.Object,
				RouteData = new()
			})
		};

		cartModel.OnPost(1, "myUrl");

		// Assert.

		Assert.Single(cart.Lines);
		var line = cart.Lines.First();
		var product2 = line.Product;
		Assert.Equal(product1.ProductID, product2.ProductID);
		Assert.Equal(product1.Name, product2.Name);
		Assert.Equal(product1.Description, product2.Description);
		Assert.Equal(product1.Category, product2.Category);
		Assert.Equal(product1.Price, product2.Price);
		Assert.Equal(1, line.Quantity);
	}
}
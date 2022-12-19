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

		var cartModel = new CartModel(repository.Object)
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
}
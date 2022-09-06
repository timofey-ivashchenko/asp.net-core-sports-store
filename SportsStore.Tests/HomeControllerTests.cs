using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class HomeControllerTests
{
	[Fact]
	public void CanUseRepository()
	{
		// Arrange.

		var mock = new Mock<IStoreRepository>();

		mock.Setup(x => x.Products).Returns(
			new Product[]
			{
				new () {Id = 1, Name = "Product 1"},
				new () {Id = 2, Name = "Product 2"}
			}.AsQueryable());

		var controller = new HomeController(mock.Object);

		// Act.

		var result = controller.Index() as ViewResult;
		var model = result?.ViewData.Model as IEnumerable<Product>;
		var products = model?.ToArray() ?? Array.Empty<Product>();

		// Assert.

		Assert.NotNull(products);
		Assert.Equal(2, products.Length);

		Assert.NotNull(products[0]);
		Assert.Equal(1, products[0].Id);
		Assert.Equal("Product 1", products[0].Name);

		Assert.NotNull(products[1]);
		Assert.Equal(2, products[1].Id);
		Assert.Equal("Product 2", products[1].Name);
	}
}
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class HomeControllerTests
{
	public static readonly TheoryData<int, Product[]> PaginationTestData = new ()
	{
		{0, new Product[]
		{
			new () {Id = 1, Name = "Product 1"},
			new () {Id = 2, Name = "Product 2"},
			new () {Id = 3, Name = "Product 3"}
		}},
		{1, new Product[]
		{
			new () {Id = 1, Name = "Product 1"},
			new () {Id = 2, Name = "Product 2"},
			new () {Id = 3, Name = "Product 3"}
		}},
		{2, new Product[]
		{
			new () {Id = 4, Name = "Product 4"},
			new () {Id = 5, Name = "Product 5"}
		}},
		{3, Array.Empty<Product>()}
	};

	[Theory]
	[MemberData(nameof(PaginationTestData))]
	public void CanPaginate(int page, Product[] expectedProcucts)
	{
		// Arrange.

		var mock = new Mock<IStoreRepository>();

		mock.Setup(x => x.Products).Returns(
			new Product[]
			{
				new () {Id = 1, Name = "Product 1"},
				new () {Id = 2, Name = "Product 2"},
				new () {Id = 3, Name = "Product 3"},
				new () {Id = 4, Name = "Product 4"},
				new () {Id = 5, Name = "Product 5"}
			}.AsQueryable()
		);

		var controller = new HomeController(mock.Object)
		{
			PageSize = 3
		};

		// Act.

		var result = controller.Index(page) as ViewResult;
		var model = result?.ViewData.Model as IEnumerable<Product>;
		var actualProducts = model?.ToArray() ?? Array.Empty<Product>();

		// Assert.

		Assert.Equal(expectedProcucts.Length, actualProducts.Length);

		for (var i = 0; i < expectedProcucts.Length; i++)
		{
			var expectedProduct = expectedProcucts[i];
			var actualProduct = actualProducts[i];

			Assert.NotNull(actualProduct);
			Assert.Equal(expectedProduct.Id, actualProduct.Id);
			Assert.Equal(expectedProduct.Name, actualProduct.Name);
			Assert.Equal(expectedProduct.Description, actualProduct.Description);
			Assert.Equal(expectedProduct.Category, actualProduct.Category);
			Assert.Equal(expectedProduct.Price, actualProduct.Price);
		}
	}

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
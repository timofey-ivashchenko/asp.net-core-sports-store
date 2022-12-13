using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests;

public class HomeControllerTests
{
	public static readonly TheoryData<int, Product[]> PaginationTestData = new()
	{
		{0, new Product[]
		{
			new() {Id = 1, Name = "Product 1"},
			new() {Id = 2, Name = "Product 2"},
			new() {Id = 3, Name = "Product 3"}
		}},
		{1, new Product[]
		{
			new() {Id = 1, Name = "Product 1"},
			new() {Id = 2, Name = "Product 2"},
			new() {Id = 3, Name = "Product 3"}
		}},
		{2, new Product[]
		{
			new() {Id = 4, Name = "Product 4"},
			new() {Id = 5, Name = "Product 5"}
		}},
		{3, Array.Empty<Product>()}
	};

	[Fact]
	public void CanFilterProducts()
	{
		// Arrange.

		var repository = new Mock<IStoreRepository>();

		repository.Setup(x => x.Products).Returns(
			new Product[]
			{
				new() { Name = "P1", Category = "C1" },
				new() { Name = "P2", Category = "C2" },
				new() { Name = "P3", Category = "C1" },
				new() { Name = "P4", Category = "C3" },
				new() { Name = "P5", Category = "C1" }
			}.AsQueryable());

		var controller = new HomeController(repository.Object);

		// Act.

		var result = controller.Index("C1");
		var viewModel = result.ViewData.Model as ProductsListViewModel;
		var products = viewModel?.Products.ToList();

		// Assert.

		Assert.NotNull(products);
		Assert.Equal(3, products.Count);

		Assert.NotNull(products[0]);
		Assert.Equal("P1", products[0].Name);
		Assert.Equal("C1", products[0].Category);

		Assert.NotNull(products[1]);
		Assert.Equal("P3", products[1].Name);
		Assert.Equal("C1", products[1].Category);

		Assert.NotNull(products[2]);
		Assert.Equal("P5", products[2].Name);
		Assert.Equal("C1", products[2].Category);
	}

	[Theory]
	[MemberData(nameof(PaginationTestData))]
	public void CanPaginate(int page, Product[] expectedProcucts)
	{
		// Arrange.

		var mock = new Mock<IStoreRepository>();

		mock.Setup(x => x.Products).Returns(
			new Product[]
			{
				new() {Id = 1, Name = "Product 1"},
				new() {Id = 2, Name = "Product 2"},
				new() {Id = 3, Name = "Product 3"},
				new() {Id = 4, Name = "Product 4"},
				new() {Id = 5, Name = "Product 5"}
			}.AsQueryable()
		);

		var controller = new HomeController(mock.Object)
		{
			PageSize = 3
		};

		// Act.

		var result = controller.Index(null, page);
		var model = result.ViewData.Model as ProductsListViewModel;
		var actualProducts = model?.Products.ToArray() ?? Array.Empty<Product>();

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
	public void CanSendPaginationViewModel()
	{
		// Arrange.

		var repository = new Mock<IStoreRepository>();

		repository.Setup(x => x.Products)
			.Returns(new Product[]
			{
				new() { Id = 1, Name = "Product 1" },
				new() { Id = 2, Name = "Product 2" },
				new() { Id = 3, Name = "Product 3" },
				new() { Id = 4, Name = "Product 4" },
				new() { Id = 5, Name = "Product 5" }
			}.AsQueryable());

		var controller = new HomeController(repository.Object)
		{
			PageSize = 3
		};

		// Act.

		var result = controller.Index(null, 2);
		var model = result.ViewData.Model as ProductsListViewModel;
		var pagingInfo = model?.PagingInfo;

		// Assert.

		Assert.NotNull(pagingInfo);
		Assert.Equal(2, pagingInfo.CurrentPage);
		Assert.Equal(3, pagingInfo.ItemsPerPage);
		Assert.Equal(5, pagingInfo.TotalItems);
		Assert.Equal(2, pagingInfo.TotalPages);
	}

	[Fact]
	public void CanUseRepository()
	{
		// Arrange.

		var mock = new Mock<IStoreRepository>();

		mock.Setup(x => x.Products).Returns(
			new Product[]
			{
				new() {Id = 1, Name = "Product 1"},
				new() {Id = 2, Name = "Product 2"}
			}.AsQueryable());

		var controller = new HomeController(mock.Object);

		// Act.

		var result = controller.Index(null);
		var model = result.ViewData.Model as ProductsListViewModel;
		var products = model?.Products.ToArray() ?? Array.Empty<Product>();

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

	[Fact]
	public void GeneratesCategorySpecificProductCount()
	{
		// Arrange.

		var repository = new Mock<IStoreRepository>();

		repository.Setup(x => x.Products).Returns(
			new Product[]
			{
				new() { Name = "P1", Category = "C3" },
				new() { Name = "P2", Category = "C1" },
				new() { Name = "P3", Category = "C5" },
				new() { Name = "P4", Category = "C2" },
				new() { Name = "P5", Category = "C1" },
				new() { Name = "P6", Category = "C4" },
				new() { Name = "P7", Category = "C1" },
				new() { Name = "P8", Category = "C2" },
				new() { Name = "P9", Category = "C1" }
			}.AsQueryable());

		var controller = new HomeController(repository.Object);

		// Act.

		static int? GetCount(HomeController target, string? category)
			=> (target.Index(category).ViewData.Model as ProductsListViewModel)?.PagingInfo.TotalItems;

		var countAll = GetCount(controller, null);
		var count1 = GetCount(controller, "C1");
		var count2 = GetCount(controller, "C2");
		var count3 = GetCount(controller, "C3");
		var count4 = GetCount(controller, "C4");
		var count5 = GetCount(controller, "C5");

		// Assert.

		Assert.Equal(9, countAll);
		Assert.Equal(4, count1);
		Assert.Equal(2, count2);
		Assert.Equal(1, count3);
		Assert.Equal(1, count4);
		Assert.Equal(1, count5);
	}
}
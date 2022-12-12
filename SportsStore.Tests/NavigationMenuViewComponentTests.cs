using Microsoft.AspNetCore.Mvc.ViewComponents;
using SportsStore.Components;
using SportsStore.Models;

namespace SportsStore.Tests;

public class NavigationMenuViewComponentTests
{
	[Fact]
	public void CanSelectCategories()
	{
		// Arrange.

		var repository = new Mock<IStoreRepository>();

		repository.Setup(x => x.Products).Returns(
			new Product[]
			{
				new() { Name = "P1", Category = "C3"},
				new() { Name = "P2", Category = "C2"},
				new() { Name = "P3", Category = "C1"},
				new() { Name = "P4", Category = "C3"},
				new() { Name = "P5", Category = "C4"},
				new() { Name = "P6", Category = "C5"},
				new() { Name = "P7", Category = "C1"},
				new() { Name = "P8", Category = "C2"},
				new() { Name = "P9", Category = "C5"}
			}.AsQueryable());

		var component = new NavigationMenuViewComponent(repository.Object);

		// Act.

		var result = component.Invoke() as ViewViewComponentResult;
		var categories = result?.ViewData?.Model is IEnumerable<string> model
			? model.ToList() : new();

		// Assert.

		Assert.True(categories.SequenceEqual(
			new[] { "C1", "C2", "C3", "C4", "C5" }));
	}

	[Fact]
	public void IndicatesSelectedCategory()
	{
		// Arrange.

		const string expectedCategory = "Electronics";

		var repository = new Mock<IStoreRepository>();

		var component = new NavigationMenuViewComponent(repository.Object)
		{
			ViewComponentContext = new() { ViewContext = new() { RouteData = new() }}
		};

		component.RouteData.Values["category"] = expectedCategory;

		// Act.

		var result = component.Invoke() as ViewViewComponentResult;
		var actualCategory = result?.ViewData?["SelectedCategory"];

		// Assert.

		Assert.Equal(expectedCategory, actualCategory);
	}
}
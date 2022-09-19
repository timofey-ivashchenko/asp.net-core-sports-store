using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
	readonly IStoreRepository _repository;

	public NavigationMenuViewComponent(IStoreRepository repository) =>
		_repository = repository;

	public IViewComponentResult Invoke()
	{
		// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
		ViewBag.SelectedCategory = RouteData?.Values["category"]!;

		return View(_repository.Products
			.Select(x => x.Category)
			.Distinct()
			.OrderBy(x => x));
	}
}
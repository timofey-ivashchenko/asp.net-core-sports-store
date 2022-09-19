using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
	readonly IStoreRepository _repository;

	public NavigationMenuViewComponent(IStoreRepository repository) =>
		_repository = repository;

	public IViewComponentResult Invoke() =>
		View(_repository.Products
			.Select(x => x.Category)
			.Distinct()
			.OrderBy(x => x));
}
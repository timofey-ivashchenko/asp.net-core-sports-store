using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
	const int PageSize = 4;

	readonly IStoreRepository _repository;

	public HomeController(IStoreRepository repository) =>
		_repository = repository;

	public IActionResult Index(int page = 1)
	{
		if (page < 1) page = 1;

		var products = _repository.Products
			.OrderBy(x => x.Id)
			.Skip((page - 1) * PageSize)
			.Take(PageSize);

		return View(products);
	}
}
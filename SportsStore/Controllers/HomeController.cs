using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
	readonly IStoreRepository _repository;

	public HomeController(IStoreRepository repository) =>
		_repository = repository;

	public int PageSize { get; set; } = 4;

	public IActionResult Index(string? category, int page = 1)
	{
		if (page < 1) page = 1;

		var products = _repository.Products
			.Where(x => category == null || x.Category == category)
			.OrderBy(x => x.Id)
			.Skip((page - 1) * PageSize)
			.Take(PageSize);

		var pagingInfo = new PagingInfo
		{
			CurrentPage = page,
			ItemsPerPage = PageSize,
			TotalItems = _repository.Products.Count(
				x => category == null || x.Category == category)
		};

		var model = new ProductsListViewModel
		{
			CurrentCategory = category,
			PagingInfo = pagingInfo,
			Products = products
		};

		return View(model);
	}
}
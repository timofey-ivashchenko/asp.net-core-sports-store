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

	public IActionResult Index(int page = 1)
	{
		if (page < 1) page = 1;

		var products = _repository.Products
			.OrderBy(x => x.Id)
			.Skip((page - 1) * PageSize)
			.Take(PageSize);

		var pagingInfo = new PagingInfo
		{
			CurrentPage = page,
			ItemsPerPage = PageSize,
			TotalItems = _repository.Products.Count()
		};

		var model = new ProductsListViewModel
		{
			PagingInfo = pagingInfo,
			Products = products
		};

		return View(model);
	}
}
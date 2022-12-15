using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
	readonly IStoreRepository _repository;

	public HomeController(IStoreRepository repository) =>
		_repository = repository;

	public int PageSize { get; set; } = 3;

	public ViewResult Index(string? category, int productPage = 1)
	{
		if (productPage < 1) productPage = 1;

		var products = _repository.Products
			.Where(x => category == null || x.Category == category)
			.OrderBy(x => x.ProductID)
			.Skip((productPage - 1) * PageSize)
			.Take(PageSize);

		var pagingInfo = new PagingInfo
		{
			CurrentPage = productPage,
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
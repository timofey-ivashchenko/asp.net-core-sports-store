using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages;

public class CartModel : PageModel
{
	readonly IStoreRepository _repository;

	public CartModel(IStoreRepository repository) => _repository = repository;

	public Cart Cart { get; private set; } = new();

	public string ReturnUrl { get; private set; } = "/";

	public void OnGet(string returnUrl)
	{
		ReturnUrl = returnUrl;
		Cart = HttpContext.Session.GetObject<Cart>("cart") ?? new();
	}

	public IActionResult OnPost(long productId, string returnUrl)
	{
		var product = _repository.Products.FirstOrDefault(
			x => x.ProductID == productId);

		if (product is not null)
		{
			Cart = HttpContext.Session.GetObject<Cart>("cart") ?? new();
			Cart.AddItem(product, 1);
			HttpContext.Session.SetObject("cart", Cart);
		}

		return RedirectToPage(new { returnUrl });
	}
}
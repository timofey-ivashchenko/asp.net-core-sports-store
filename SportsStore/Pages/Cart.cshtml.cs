using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models;

namespace SportsStore.Pages;

public class CartModel : PageModel
{
	readonly IStoreRepository _repository;

	public CartModel(IStoreRepository repository, Cart cart)
	{
		_repository = repository;
		Cart = cart;
	}

	public Cart Cart { get; private set; }

	public string ReturnUrl { get; private set; } = "/";

	public void OnGet(string returnUrl) => ReturnUrl = returnUrl;

	public IActionResult OnPost(long productId, string returnUrl)
	{
		var product = _repository.Products.FirstOrDefault(
			x => x.ProductID == productId);

		if (product is not null) Cart.AddItem(product, 1);

		return RedirectToPage(new { returnUrl });
	}

	public IActionResult OnPostRemove(long productId, string returnUrl)
	{
		Cart.RemoveLine(Cart.Lines.First(
			x => x.Product.ProductID == productId).Product);

		return RedirectToPage(new { returnUrl });
	}
}
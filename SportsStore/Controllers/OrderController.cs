using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class OrderController : Controller
{
	readonly Cart _cart;

	readonly IOrderRepository _repository;

	public OrderController(IOrderRepository repository, Cart cart)
	{
		_cart = cart;
		_repository = repository;
	}

	public ViewResult Checkout() => View(new Order());

	[HttpPost]
	public IActionResult Checkout(Order order)
	{
		if (_cart.Lines.Count == 0)
			ModelState.AddModelError(string.Empty, "Sorry, your cart is empty!");

		if (!ModelState.IsValid) return View();

		order.Lines = _cart.Lines;
		_repository.SaveOrder(order);
		_cart.Clear();

		return RedirectToPage("/completed", new { orderId = order.OrderID });
	}
}
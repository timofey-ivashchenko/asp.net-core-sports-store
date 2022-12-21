﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components;

public class CartSummaryViewComponent : ViewComponent
{
	readonly Cart _cart;

	public CartSummaryViewComponent(Cart cart) => _cart = cart;

	public IViewComponentResult Invoke() => View(_cart);
}